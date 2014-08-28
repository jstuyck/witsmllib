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
namespace witsmllib.util
{

    /**
     * Collection of XML utilities.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Schema;
    public sealed class XmlUtil
    {

        /**
         * Private constructor to prevent client instantiation.
         */
        private XmlUtil()
        {
            //Debug.Assert(false : "This constructor should never bve called";
        }

        /**
         * Create a DOM document from a specified XML string.
         *
         * @param xml  String to create DOM document from. Non-null.
         * @return     DOM document. Null if the parse operation failed somehow.
         */
        private static XDocument newDocument(String xml)
        {
            return XDocument.Load(new StringReader(xml)); 
        }
        //private static Document newDocument(String xml)
        //{ //throws SAXException {
        //    try
        //    {
        //        DocumentBuilderFactory documentBuilderFactory = DocumentBuilderFactory.newInstance();
        //        documentBuilderFactory.setNamespaceAware(true);

        //        DocumentBuilder parser = documentBuilderFactory.newDocumentBuilder();
        //        Reader reader = new StringReader(xml);
        //        Document document = parser.parse(new InputSource(reader));

        //        return document;
        //    }
        //    catch (ParserConfigurationException exception)
        //    {
        //        //Debug.Assert(false : exception.getMessage();
        //        return null;
        //    }
        //    catch (IOException exception)
        //    {
        //        //Debug.Assert(false : exception.getMessage();
        //        return null;
        //    }
        //}

        /**
         * Check if the specified XML is valid according to the specified schema.
         *
         * @param xml         XML to validate. Non-null.
         * @param schemaFile  Schema file. Non-null.
         * @return            True if the XML is valid, false otherwise.
         * @throws  ArgumentException  If xml is null or schemaFile is null or
         *                                    schemaFile doesnt exist.
         */
        public static void validate(String xml, string schemaFilePath)
        { //throws SAXException {

            if (xml == null)
                throw new ArgumentException("xml cannot be null");

            if (schemaFilePath == null)
                throw new ArgumentException("schemaFile cannot be null");

            if (!File.Exists(schemaFilePath))
                throw new ArgumentException("file doesn't exist: " + schemaFilePath);

            // Parse the XML string into a DOM tree.
            XDocument document = newDocument(xml);
            
            // Create a SchemaFactory capable of understanding WXS schemas.
            //SchemaFactory schemaFactory =
            //    SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);

            //// Load a WXS schema, represented by a Schema instance.
            //Schema schema = schemaFactory.newSchema(schemaFile);

            XmlSchema schema = XmlSchema.Read(new FileStream(schemaFilePath, FileMode.Open), null);   //TODO - add handler to this?
            var schemaset = new XmlSchemaSet();
            schemaset.Add(schema);
             // Create a Validator object, which can be used to validate
            // an instance document.
           // Validator validator = schema.newValidator();

            // Validate the DOM tree.
            try
            {
                //validator.validate(new DOMSource(document));
                document.Validate(schemaset, null); 
            }
            catch (IOException exception)
            {
                //Debug.Assert(false : exception.getMessage();
            }
        }

        /**
         * Check if the specified XML is well-formed.
         *
         * @param xml  XML to check. Non-null.
         * @return     True if the XML is well-formed, false otherwise.
         * @throws ArgumentException  If xml is null.
         */
        public static bool isWellFormed(String xml)
        {
            if (xml == null)
                throw new ArgumentException("xml cannot be null");

            try
            {
                newDocument(xml);
                return true;
            }
            catch (Exception exception)// (SAXException exception)
            {
                return false;
            }
        }

        /**
         * Pretty print the specified XML string.
         *
         * @param xml  XML string to pretty print. Non-null.
         * @return     A pretty printed version of the input, or the string
         *             itself if it doesn't form a well-formed XML.
         * @throws ArgumentException  If xml is null.
         */
        public static String prettyPrint(String xml)
        {
            if (xml == null)
                throw new ArgumentException("xml cannot be null");
            throw new NotImplementedException(); 

            //Format format = Format.getPrettyFormat();
            //XMLOutputter outputter = new XMLOutputter(format);

            //Writer writer = new StringWriter();

            //SAXBuilder builder = new SAXBuilder();

            //try
            //{
            //    org.jdom.Document document = XDocument.Load(new StringReader(xml));
            //    outputter.output(document, writer);

            //    return writer.ToString();
            //}
            //catch (IOException exception)
            //{
            //    Console.Write(exception.StackTrace);// exception.printStackTrace();
            //    return xml;
            //}
            //catch (JDOMException exception)
            //{
            //    exception.printStackTrace();
            //    return xml;
            //}
        }

        /**
         * Convenience method for parsing a standard WITSML double string
         * into a Java Double instance.
         *
         * @param valueString  WITSML double string. May be null.
         * @return             Corresponding double instance, or null if input
         *                     is null or incorrect format.
         */
        private static Double? getDouble(String valueString)
        {
            if (valueString == null || string.IsNullOrEmpty(valueString))// valueString.isEmpty())
                return null;

            try
            {
                return Double.Parse(valueString); // new Double(valueString);
            }
            catch (FormatException exception)
            {
                // TODO: Throw parse exception
                return null;
            }
        }

        /**
         * Convenience method for parsing a standard WITSML double string
         * into a Java Double instance.
         *
         * @param valueString  WITSML double string. May be null.
         * @return             Corresponding double instance, or null if input
         *                     is null or incorrect format.
         */
        private static Int32? getInteger(String valueString)
        {
            if (valueString == null || string.IsNullOrEmpty( valueString)) //.isEmpty())
                return null;

            try
            {
                return  Int32.Parse(valueString);
            }
            catch (FormatException exception)
            {
                // TODO: throw parse exception
                return null;
            }
        }

        /**
         * Convenience method for parsing a standard WITSML time string
         * into a Java Date instance.
         *
         * @param valueString  WITSML time string. May be null.
         * @return             Corresponding time instance, or null if input
         *                     is null or incorrect format.
         */
        public static DateTime?  getTime(String timeString)
        {
            if (timeString == null || String.IsNullOrEmpty(timeString))//.isEmpty())
                return null;

            try
            {
                return DateTime.Parse(timeString);
            }
            catch (FormatException exception)
            {
                // TODO: Throw parse exception
                return null;
            }
        }

        public static Boolean? getBoolean(String booleanString)
        {
            if (booleanString == null)
                return null;

            return booleanString.ToLower().Equals("true") ||
                   booleanString.Equals("1");
        }

        /**
         * Return the new value for the specfied element.
         *
         * @param element    Parent of element to get new value for. Non-null.
         * @param childName  Name of child element to extract value of. Non-null.
         * @param oldValue   The present value. This will be returned if the element
         *                   requested does not exist. May be null.
         * @return           The child element value, or oldValue if the requested
         *                   element doesn't exist. May be null.
         */

        //TODO - use IsEmpty instead of check for null?
        public static String update(XElement parentElement, string childName, string oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //Add namespace to this?
            return element == null ? oldValue : element.Value.Trim(); 
        }
        //public static String update(XElement parentElement, String childName, String oldValue)
        //{
        //    XElement element = parentElement.Element(childName, parentElement.getNamespace());
        //    return element == null ? oldValue : element.getTextTrim();
        //}

        public static DateTime?  update(XElement parentElement, String childName, DateTime?  oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); // getChild(childName, parentElement.getNamespace());
            return element == null ? oldValue : getTime(element.Value.Trim()); 
        }
        //public static DateTime? update(XElement parentElement, String childName, DateTime? oldValue)
        //{
        //    XElement element = parentElement.Element(childName, parentElement.getNamespace());
        //    return element == null ? oldValue : getTime(element.getTextTrim());
        //}

        public static Int32? update(XElement parentElement, String childName, Int32? oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //.Element(childName, parentElement.getNamespace());
            return element == null ? oldValue : getInteger(element.Value.Trim()); //.getTextTrim());
        }

        public static Boolean? update(XElement parentElement, String childName, Boolean? oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //.Element(childName, parentElement.getNamespace());
            return element == null ? oldValue : getBoolean(element.Value.Trim());//.getTextTrim());
        }

        public static Value update(XElement parentElement, String childName, Value oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName);//.Element(childName, parentElement.getNamespace());
            if (element == null)
                return oldValue;

            //BM - sample data was missing uom.. is this not required? 
            var att = element.Attribute("uom");
            
            String unit = att==null? null: att.Value;//.Attribute("uom");
            Double? value = getDouble(element.Value.Trim()); //.getTextTrim());

            return new Value(value.Value , unit);
        }

        public static String update(XElement parentElement, String childName, String attributeName, String oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace+ childName); //.Element(childName, parentElement.getNamespace());
            String newValue = element != null ? element.Attribute(attributeName).Value: null; //.Attribute(attributeName) : null;
            return newValue != null ? newValue : oldValue;
        }
    }

}