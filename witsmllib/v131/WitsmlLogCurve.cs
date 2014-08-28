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
     * Version specific implementation of the WitsmlLogCurve type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    public sealed class WitsmlLogCurve : witsmllib.WitsmlLogCurve
    {

        /**
         * Create a log curve instance.
         *
         * @param log      Parent log. Non-null.
         * @param name     Name of curve. Non-null.
         * @param curveNo  Curve number.
         */
        internal WitsmlLogCurve(witsmllib.WitsmlLog log, String name, int curveNo)
       
            :base(log, name, curveNo)
         {}

        
        internal new void clear()
        {
            base.clear();
        }

        
        internal new void addValue(String token)
        { //throws WitsmlParseException {
            base.addValue(token);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
        internal static String getQuery()
        {
            String query = "<logCurveInfo uid = \"\">" +
                           "  <mnemonic/>" +
                           "  <classWitsml/>" +
                           "  <unit/>" +
                           "  <mnemAlias/>" +
                           "  <nullValue/>" +
                           "  <alternateIndex/>" +
                           "  <wellDatum/>" +
                           "  <minIndex/>" +
                           "  <maxIndex/>" +
                           "  <minDateTimeIndex/>" +
                           "  <maxDateTimeIndex/>" +
                           "  <columnIndex/>" +
                           "  <curveDescription/>" +
                           "  <sensorOffset/>" +
                           "  <dataSource/>" +
                           "  <densData/>" +
                           "  <traceState/>" +
                           "  <traceOrigin/>" +
                           "  <typeLogData/>" +
                           "  <axisDefinition/>" +
                           "</logCurveInfo>";

            return query;
        }

        /**
         * Parse the specified DOM element and instantiate the properties
         * of this instance.
         *
         * @param element  XML element to parse. Non-null.
         */
        internal void update(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            id = element.Attribute("uid").Value;
            quantity = XmlUtil.update(element, "classWitsml", quantity);
            unit = XmlUtil.update(element, "unit", unit);
            mnemonic = XmlUtil.update(element, "mnemAlias", mnemonic);
            noValue = XmlUtil.update(element, "nullValue", noValue);
            _isAlternateIndex = XmlUtil.update(element, "alternateIndex", _isAlternateIndex);

            if (log.getIndexType().ToLower().Contains("time"))
            {
                startIndex = log.getIndex(XmlUtil.update(element, "minDateTimeIndex", (String)null));
                endIndex = log.getIndex(XmlUtil.update(element, "maxDateTimeIndex", (String)null));
            }
            else
            {
                startIndex = log.getIndex(XmlUtil.update(element, "minIndex", (String)null));
                endIndex = log.getIndex(XmlUtil.update(element, "maxIndex", (String)null));
            }

            description = XmlUtil.update(element, "curveDescription", description);
            sensorOffset = XmlUtil.update(element, "sensorOffset", sensorOffset);
            dataSource = XmlUtil.update(element, "dataSource", dataSource);
            dataDensity = XmlUtil.update(element, "densData", dataDensity);
            traceState = XmlUtil.update(element, "traceState", traceState);
            traceOrigin = XmlUtil.update(element, "traceOrigin", traceOrigin);
            dataType = getDataType(XmlUtil.update(element, "typeLogData", (String)null));
        }
    }
}