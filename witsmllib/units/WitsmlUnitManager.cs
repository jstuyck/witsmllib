/*
nwitsml Copyright 2010 Setiri LLC
Derived from the jwitsml project, Copyright 2010 Statoil ASA
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
namespace witsmllib.units{

//import java.util.ArrayList;
//import java.util.List;
//import java.io.InputStream;
//import java.io.IOException;

////import org.jdom.Element; 
////import org.jdom.Namespace;
////import org.jdom.input.SAXBuilder;
////import org.jdom.JDOMException;

////import nwitsml.Value;

/**
 * Singleton class for handling WITSML quantities, units and
 * unit conversions.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
    using System.Text;
    using System.Xml.Linq;
public sealed class WitsmlUnitManager {

    /** The sole instance of this class. */
    private  static WitsmlUnitManager instance = new WitsmlUnitManager();

    /** All known quantities. */
    private  List<WitsmlQuantity> quantities = new List<WitsmlQuantity>();

    /**
     * Create a unit manager instance. Private to prevent
     * client instantiation.
     */
    private WitsmlUnitManager() {
        load();
    }

    /**
     * Return sole instance of this class.
     *
     * @return  Sole instance of this class. Never null.
     */
    public static WitsmlUnitManager getInstance() {
        return instance;
    }

    /**
     * Load all quantity and unit information from the local "units.txt" file.
     */
    private void load() {
        String fileName = "witsmlUnitDict.xml";
        //String packageName = Assembly.GetExecutingAssembly().GetName().Name;// getClass().getPackage().getName();
        //String packageLocation = packageName.Replace ('.', '/');
        //String filePath = "/" + packageLocation + "/" + fileName;

	   // Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName );	
        Assembly ass = this.GetType().Assembly;
        Stream stream = ass.GetManifestResourceStream(ass.GetName().Name + ".nwitsml.units." + fileName); 
        List<WitsmlQuantity> hasBaseUnit = new List<WitsmlQuantity>();


        //InputStream stream = WitsmlUnitManager.class.getResourceAsStream(filePath);

        //SAXBuilder builder = new SAXBuilder();

        try {
            XDocument document = XDocument.Load(stream); // builder.build(stream);
            XElement rootElement = document.Root;
            XNamespace @namespace = rootElement.Name.Namespace; //.getNamespace();

            XElement unitDefinitionsElement = rootElement.Element(@namespace + "UnitsDefinition"/*,@namespace*/);
            var children = unitDefinitionsElement.Elements(@namespace+"UnitOfMeasure"/*, @namespace*/);
            foreach (Object child in children) {
                XElement unitOfMeasureElement = (XElement) child;

                List<WitsmlQuantity> quantitiesForUnit = new List<WitsmlQuantity>();

                //
                // Extract the BaseUnit element with its Description memeber
                //
                XElement baseUnitElement = unitOfMeasureElement.Element(@namespace + "BaseUnit"/*, @namespace*/);
                bool isBaseUnit = baseUnitElement != null;

                String quantityDescription = baseUnitElement != null && baseUnitElement.Element(@namespace + "Description") != null ?  //bugfix 9-25-10
                                             baseUnitElement.Element(@namespace + "Description").Value.Trim()
                                                                               : null;

                //
                // Identify all the quantities this unit appears in
                //
                var quantityTypeElements = unitOfMeasureElement.Elements(@namespace + "QuantityType"/*,@namespace*/);
                foreach (Object child2 in quantityTypeElements) {
                    XElement quantityTypeElement = (XElement) child2;
                    String quantityName = quantityTypeElement.Value.Trim(); //.getTextTrim();
                    WitsmlQuantity quantity = findOrCreateQuantity(quantityName,
                                                                   quantityDescription);
                    quantitiesForUnit.Add(quantity);

                    // DEBUG
                    if (isBaseUnit && hasBaseUnit.Contains(quantity)) 
                        Console.WriteLine(
                        //System.out.println(
                            "ALREADY BASE: " + quantity.getName());

                    if (isBaseUnit)
                        hasBaseUnit.Add(quantity);
                }

                String unitName = unitOfMeasureElement.Element(@namespace + "Name"/*, @namespace*/).Value.Trim();

                String unitSymbol = unitOfMeasureElement.Element(@namespace + "CatalogSymbol"/*,@namespace*/).Value.Trim();

                XElement conversionElement = unitOfMeasureElement.Element("ConversionToBaseUnit"/*,@namespace*/);


                double a = 1.0;
                double b = 0.0;
                double c = 0.0;
                double d = 1.0;

                if (conversionElement != null) {
                    String factorText = conversionElement.Element(@namespace + "Factor"/*,@namespace*/).Value.Trim();
                    XElement fractionElement = conversionElement.Element(@namespace + "Fraction"/*,@namespace*/);
                    XElement formulaElement = conversionElement.Element(@namespace+"Formula"/*,@namespace*/);

                    if (factorText != null) {
                        try {
                            a = Double.Parse(factorText);
                        }
                        catch (FormatException exception) {
                            //Debug.Assert(false : "Invalid numeric value: " + factorText;
                        }
                    }
                    else if (fractionElement != null) {
                        String numeratorText = fractionElement.Element(@namespace+"Numerator"/*,@namespace*/).Value.Trim();
                        String denominatorText = fractionElement.Element(@namespace + "Denominator"/*,@namespace*/).Value.Trim();

                        try {
                            double numerator = Double.Parse(numeratorText);
                            double denominator = Double.Parse(denominatorText);

                            a = numerator / denominator;
                        }
                        catch (FormatException exception) {
                            //Debug.Assert(false : "Invalid numeric value: " + numeratorText + "/" + denominatorText;
                        }
                    }
                    else if (formulaElement != null) {
                        String aText = formulaElement.Element(@namespace+"A"/*, @namespace*/).Value.Trim();
                        String bText = formulaElement.Element(@namespace+"B"/*, @namespace*/).Value.Trim();
                        String cText = formulaElement.Element(@namespace+"C"/*, @namespace*/).Value.Trim();
                        String dText = formulaElement.Element(@namespace+"D"/*, @namespace*/).Value.Trim();

                        try {
                            a = Double.Parse(aText);
                            b = Double.Parse(bText);
                            c = Double.Parse(cText);
                            d = Double.Parse(dText);
                        }
                        catch (FormatException exception) {
                            //Debug.Assert(false : "Invalid numeric value: " + aText + "," + bText + "," + cText + "," + dText;
                        }
                    }
                }

                WitsmlUnit unit = new WitsmlUnit(unitName, unitSymbol,
                                                 a, b, c, d);

                foreach (WitsmlQuantity quantity in quantitiesForUnit) {
                    quantity.addUnit(unit, isBaseUnit);
                }
            }
        }
        catch (IOException exception) {
            //Debug.Assert(false : "Parse error: " + filePath;
        }
        catch (Exception /*JDOMException*/ exception) {
            //Debug.Assert(false : "Parse error: " + filePath;
        }


        foreach (WitsmlQuantity q in quantities) {
            if (!hasBaseUnit.Contains(q))
                Console.WriteLine(
                //System.out.println(
                "NO BASE UNIT: " + q.getName());
        }
    }

    /**
     * Return names of all known quantities.
     *
     * @return  List of all known quantities. Never null.
     */
    public List<String> getQuantities() {
        List<String> quantityNames = new List<String>();
        foreach (WitsmlQuantity quantity in quantities)
            quantityNames.Add(quantity.getName());

        return quantityNames;
    }

    /**
     * Return all units of the specified quantity.
     *
     * @param quantityName  Quantity to get units for. Non-null.
     * @return              All units for the specified quantity. Never null.
     * @throws ArgumentException  If quantityName is null or unknown.
     */
    public List<WitsmlUnit> getUnits(String quantityName) {
        if (quantityName == null)
            throw new ArgumentException("quantityName cannot be null");

        WitsmlQuantity quantity = findQuantity(quantityName);
        if (quantity == null)
            throw new ArgumentException("Unknown quantity: " + quantityName);

        return quantity.getUnits();
    }

    /**
     * Return the quantity instance of the specified name.
     *
     * @param quantityName  Name of quantity to find. Non-null.
     * @return              Requested quantity, or null if not found.
     */
    private WitsmlQuantity findQuantity(String quantityName) {
        //Debug.Assert(quantityName != null : "quantityName cannot be null";

        foreach (WitsmlQuantity quantity in quantities) {
            if (quantity.getName().Equals(quantityName))
                return quantity;
        }

        return  null;
    }

    /**
     * Find quantity of the specified name, or create it if it is not found.
     *
     * @param quantityName  Name of quantity to find or create. Non-null.
     * @return              Requested quantity. Never null.
     */
    private WitsmlQuantity findOrCreateQuantity(String quantityName,
                                                String description) {
        //Debug.Assert(quantityName != null : "quantityName cannot be null";

        WitsmlQuantity quantity = findQuantity(quantityName);
        if (quantity == null) {
            quantity = new WitsmlQuantity(quantityName, description);
            quantities.Add(quantity);
        }

        return quantity;
    }

    /**
     * Find unit instance based on the specified unit symbol.
     *
     * @param unitSymbol  Symbol of unit to find. Non-null.
     * @return            Requested unit, or null if not found.
     * @throws ArgumentException  If unitSymbol is null.
     */
    public WitsmlUnit findUnit(String unitSymbol) {
        if (unitSymbol == null)
            throw new ArgumentException("unitSymbol cannot be null");

        foreach (WitsmlQuantity quantity in quantities) {
            foreach (WitsmlUnit unit in quantity.getUnits()) {
                if (unit.getSymbol().Equals(unitSymbol))
                    return unit;
            }
        }

        // Not found
        return null;
    }

    /**
     * Find the base unit of the specified unit.
     *
     * @param unit  Unit to find base unit for. Non-null.
     * @return      Requested base unit. Never null.
     * @throws ArgumentException  If unit is null.
     */
    public WitsmlUnit findBaseUnit(WitsmlUnit unit) {
        if (unit == null)
            throw new ArgumentException("unit cannot be null");

        foreach (WitsmlQuantity quantity in quantities) {
            foreach (WitsmlUnit u in quantity.getUnits()) {
                if (u.Equals(unit))
                    return quantity.getBaseUnit();
            }
        }

        // Not found
        //Debug.Assert(false : "Impossible, as all units originates from the manager";
        return null;
    }

    /**
     * Convert a specified value between two units.
     *
     * @param fromUnit  Unit to convert from. Non-null.
     * @param toUnit    Unit to convert to. Non-null.
     * @param value     Value to convert.
     * @return          Converted value.
     * @throws ArgumentException  If fromUnit or toUnit is null.
     */
    public double convert(WitsmlUnit fromUnit, WitsmlUnit toUnit, double value) {
        if (fromUnit == null)
            throw new ArgumentException("fromUnit cannot be null");

        if (toUnit == null)
            throw new ArgumentException("toUnit cannot be null");

        double baseValue = fromUnit.toBase(value);
        return toUnit.fromBase(baseValue);
    }

    /**
     * Convert the specified value instance to base unit
     * if possible.
     *
     * @param value  Value to convert. Non-null.
     * @return       Converted value. If the value cannot be converted
     *               for some reason, a copy of the argument is returned.
     *               Never null.
     * @throws ArgumentException  If value is null.
     */
    public Value toBaseUnit(Value value) {
        if (value == null)
            throw new ArgumentException("value cannot be null");

        Double v = value.getValue().Value ;
        String unitSymbol = value.getUnit();

        if (unitSymbol != null) {
            WitsmlUnit unit = findUnit(unitSymbol);
            if (unit != null) {
                WitsmlUnit baseUnit = findBaseUnit(unit);
                v = convert(unit, baseUnit, v);
                unitSymbol = baseUnit.getSymbol();
            }
        }

        return new Value(v, unitSymbol);
    }

    /** {@inheritDoc} */
    
    public override String ToString() {
        StringBuilder s = new StringBuilder();

        foreach (WitsmlQuantity quantity in quantities) {
            s.Append(quantity.ToString());
            s.Append("\n");
        }

        return s.ToString();
    }
}
}