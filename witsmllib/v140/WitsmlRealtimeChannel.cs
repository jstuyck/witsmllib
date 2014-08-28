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
     * Version specific implementation of the WitsmlRealtimeChannel type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlRealtimeChannel : witsmllib.WitsmlRealtimeChannel
    {

        /**
         * Create a WITSML realtime channel instance from the given
         * XML element node.
         *
         * @param element  commonData node. Non-null.
         */
        internal WitsmlRealtimeChannel(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            groupId = XmlUtil.update(element, "id", groupId);
            mnemonic = XmlUtil.update(element, "mnemonic", mnemonic);
            time = XmlUtil.update(element, "dTim", time);
            md = XmlUtil.update(element, "md", md);
            value = XmlUtil.update(element, "value", value);
            dataDensity = XmlUtil.update(element, "densData", dataDensity);
            dataQuality = XmlUtil.update(element, "qualData", dataQuality);
            formationExposureTime = XmlUtil.update(element, "fet", formationExposureTime);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
        public static String getQuery()
        {
            String query = "<channel>" +
                           "  <id/>" +
                           "  <mnemonic/>" +
                           "  <dTim/>" +
                           "  <md/>" +
                           "  <value/>" +
                           "  <densData/>" +
                           "  <qualData/>" +
                           "  <fet/>" +
                           "  <extensionNameValue/>" +
                           "</channel>";

            return query;
        }
    }
}