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
namespace witsmllib.v120
{

    /**
     * Version specific implementation of the WitsmlLogCurve type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlLogCurve : witsmllib.WitsmlLogCurve
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
        public static String getQuery()
        {
            String query = "<logCurveInfo>" +
                           "  <mnemonic/>" +
                           "  <classPOSC/>" +
                           "  <unit/>" +
                           "  <mnemAlias/>" +
                           "  <nullValue/>" +
                           "  <startIndex/>" +
                           "  <endIndex/>" +
                           "  <columnIndex/>" +
                           "  <curveDescription/>" +
                           "  <sensorOffset/>" +
                           "  <traceState/>" +
                           "  <typeLogData/>" +
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

            quantity = XmlUtil.update(element, "classPOSC", quantity);
            unit = XmlUtil.update(element, "unit", unit);
            mnemonic = XmlUtil.update(element, "mnemAlias", mnemonic);
            noValue = XmlUtil.update(element, "nullValue", noValue);
            startIndex = log.getIndex(XmlUtil.update(element, "startIndex", (String)null));
            endIndex = log.getIndex(XmlUtil.update(element, "endIndex", (String)null));
            description = XmlUtil.update(element, "curveDescription", description);
            sensorOffset = XmlUtil.update(element, "sensorOffset", sensorOffset);
            traceState = XmlUtil.update(element, "traceState", traceState);
            dataType = getDataType(XmlUtil.update(element, "typeLogData", (String)null));
        }
    }
}