using System;
using System.Xml.Linq;
using witsmllib.util;

namespace witsmllib.v131
{

    /**
     * Version specific implementation of the WitsmlRealtimeChannel type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */

    sealed class WitsmlRealtimeChannel : witsmllib.WitsmlRealtimeChannel
    {

        /**
         * Create a WITSML realtime channel instance from the given
         * XML element node.
         *
         * @param element  XML element to parse. Non-null.
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
        internal static String getQuery()
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
                           "</channel>";

            return query;
        }
    }
}