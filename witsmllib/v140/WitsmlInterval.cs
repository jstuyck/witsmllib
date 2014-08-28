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
namespace witsmllib.v140
{


    /**
     * Version specific implementation of the WitsmlLogCurve type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlInterval : witsmllib.WitsmlInterval
    {

        /**
         * Create a WITSML interval instance from the given
         * XML element node.
         *
         * @param element  XML element to create instance from. Non-null.
         */
        public WitsmlInterval(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            type = XmlUtil.update(element, "type", type);
            method = XmlUtil.update(element, "method", method);
            interval = XmlUtil.update(element, "timeInterval", interval);
            if (interval == null)
                interval = XmlUtil.update(element, "distanceInterval", interval);
        }

        /**
         * Return query for getting WITSML interval.
         *
         * @return  Query for getting WITSML interval. Never null.
         */
        public static String getQuery()
        {
            String query = "<interval>" +
                           "  <type/>" +
                           "  <method/>" +
                           "  <timeInterval uom=\"s\"/>" +
                           "  <distanceInterval uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "</interval>";

            return query;
        }
    }
}