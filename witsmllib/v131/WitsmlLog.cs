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
namespace witsmllib.v131
{


    /**
     * Version specific implementation of the WitsmlLog type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlLog : witsmllib.WitsmlLog
    {

        /**
         * Create a new WITSML log instance.
         *
         * @param server  Server this instance lives within. Non-null.
         * @param id      ID of instance. May be null.
         * @param name    Name of instance. May be null.
         * @param parent  Parent of instance. May be null.
         * @param parent  ID of parent instance. May be null.
         */
        private WitsmlLog(WitsmlServer server, String id, String name,
                          WitsmlObject parent, String parentId)
        
            :base(server, id, name, parent, parentId)
            {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. May be null.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML log instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent,
                                        XElement element)
        { //throws WitsmlParseException {

            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uid").Value;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace +"name").Value.Trim(); //, element.getNamespace());

            WitsmlLog log = new WitsmlLog(server, id, name, parent, parentId);
            log.update(element);

            return log;
        }

        /**
         * Return complete XML query for this type.
         *
         * @param id        ID of instance to get. May be empty to indicate all.
         *                  Non-null.
         * @param parentId  Parent IDs. Closest first. May be empty if instances
         *                  are accessed from the root. Non-null.
         * @return          XML query. Never null.
         */
        static String getQuery(String id, params String[] parentId)
        {
            //Debug.Assert(id != null : "id cannot be null";
            //Debug.Assert(parentId != null : "parentId cannot be null";

            String uidWellbore = parentId.Length > 0 ? parentId[0] : "";
            String uidWell = parentId.Length > 1 ? parentId[1] : "";

            String query = "<logs xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <log uidWell = \"" + uidWell + "\"" +
                           "       uidWellbore = \"" + uidWellbore + "\"" +
                           "       uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <objectGrowing/>" +
                           "    <dataRowCount/>" +
                           "    <serviceCompany/>" +
                           "    <runNumber/>" +
                           "    <bhaRunNumber/>" +
                           "    <pass/>" +
                           "    <creationDate/>" +
                           "    <description/>" +
                           "    <indexType/>" +
                           "    <startIndex uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <endIndex uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <indexCurve/>" +
                           "    <startDateTimeIndex/>" +
                           "    <endDateTimeIndex/>" +
                           "    <stepIncrement/>" +
                           "    <direction/>" +
                           "    <indexCurve/>" +
                           "    <nullValue/>" +
                           "    <logParam/>" +
                           WitsmlLogCurve.getQuery() +
                           "    <logData>" +
                           "      <data/>" +
                           "    </logData>" +
                           WitsmlCommonData.getQuery() +
                           "  </log>" +
                           "</logs>";

            return query;
        }

        /**
         * Parse the specified DOM element and instantiate the properties
         * of this instance.
         *
         * @param element  XML element to parse. Non-null.
         */
        void update(XElement element)
        { //throws WitsmlParseException {
            //Debug.Assert(element != null : "element cannot be null";

            // Remove current bulk data
            foreach (witsmllib.WitsmlLogCurve curve in curves)
                ((WitsmlLogCurve)curve).clear();

            // Common data
            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);

            // Index
            indexType = XmlUtil.update(element, "indexType", indexType);
            if (indexType != null)
            {
                if (indexType.ToLower().Contains("time"))
                {
                    startIndex = getIndex(XmlUtil.update(element, "startDateTimeIndex", (String)null));
                    endIndex = getIndex(XmlUtil.update(element, "endDateTimeIndex", (String)null));
                }
                else
                {
                    startIndex = getIndex(XmlUtil.update(element, "startIndex", (String)null));
                    endIndex = getIndex(XmlUtil.update(element, "endIndex", (String)null));
                }
            }

            indexCurveName = XmlUtil.update(element, "indexCurve", indexCurveName);
            _isGrowing = XmlUtil.update(element, "objectGrowing", _isGrowing);
            nRows = XmlUtil.update(element, "dataRowCount", nRows);
            serviceCompany = XmlUtil.update(element, "serviceCompany", serviceCompany);
            runNumber = XmlUtil.update(element, "runNumber", runNumber);
            bhaRunNumber = XmlUtil.update(element, "bhaRunNumber", bhaRunNumber);
            pass = XmlUtil.update(element, "pass", pass);
            creationTime = XmlUtil.update(element, "creationDate", creationTime);
            description = XmlUtil.update(element, "description", description);
            direction = XmlUtil.update(element, "direction", direction);
            stepIncrement = XmlUtil.update(element, "stepIncrement", stepIncrement);
            indexUnit = XmlUtil.update(element, "indexUnits", indexUnit);
            noValue = XmlUtil.update(element, "nullValue", noValue);
            unitNamingSystem = XmlUtil.update(element, "uomNamingSystem", unitNamingSystem);
            comment = XmlUtil.update(element, "otherData", comment);

            // Curve data
            var logCurveInfoElements = element.Elements(element.Name.Namespace + "logCurveInfo");//, element.getNamespace());
            foreach (Object e in logCurveInfoElements)
            {
                XElement logCurveInfoElement = (XElement)e;

                String curveName = XmlUtil.update(logCurveInfoElement, "mnemonic", (String)null);
                Int32? curveNo = XmlUtil.update(logCurveInfoElement, "columnIndex", (Int32?)null);

                WitsmlLogCurve curve = (WitsmlLogCurve)findCurve(curveName);
                if (curve == null)
                {
                    curve = new WitsmlLogCurve(this, curveName, curveNo.Value );
                    curves.Add(curve);
                }

                curve.update(logCurveInfoElement);
            }

            // Bulk data
            XElement logDataElement = element.Element(element.Name.Namespace +"logData"); //, element.getNamespace());
            if (logDataElement != null)
            {
                var dataElements = logDataElement.Elements(element.Name.Namespace+"data") ;//, element.getNamespace());

                //for (var j = dataElements.iterator(); j.hasNext(); )
                foreach(var j in dataElements)
                {
                    XElement dataElement = (XElement)j;//.next();

                    String valueString = dataElement.Value.Trim(); //.getTextTrim();

                    String[] tokens = valueString.Split(dataDelimiter);//, -1);
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        String token = tokens[i];
                        if (token.Equals(noValue))
                            token = "";

                        WitsmlLogCurve curve = (witsmllib.v131.WitsmlLogCurve)findCurve(i + 1);
                        curve.addValue(token);
                    }

                    // Handle missing log values
                    for (int i = tokens.Length; i < getNCurves(); i++)
                    {
                        WitsmlLogCurve curve = (witsmllib.v131.WitsmlLogCurve)findCurve(i + 1);
                        curve.addValue(null);
                    }
                }
            }

            unitConvert();
        }
    }
}
