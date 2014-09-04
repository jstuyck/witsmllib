using System;
using witsmllib.util;
using System.Xml.Linq;

namespace witsmllib.v140
{

    /**
     * Version specific implementation of the WitsmlCommonData type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    
    public sealed class WitsmlCommonData : witsmllib.CommonData
    {

        /**
         * Create a WITSML common data instance from the given
         * XML element node.
         *
         * @param element  XML element to create instance from. Non-null.
         */
        public WitsmlCommonData(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            source = XmlUtil.update(element, "sourceName", source);
            dTimCreation = XmlUtil.update(element, "dTimCreation", dTimCreation);
            dTimLastChange = XmlUtil.update(element, "dTimLastChange", dTimLastChange);
            setState(XmlUtil.update(element, "itemState", state.ToString()));
            serviceCategory = XmlUtil.update(element, "serviceCategory", serviceCategory);
            comments = XmlUtil.update(element, "comments", comments);
            acqusitionTimeZone = XmlUtil.update(element, "acqusitionTimeZone", acqusitionTimeZone);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
        public static String getQuery()
        {
            String query = "<commonData>" +
                           "  <sourceName/>" +
                           "  <dTimCreation/>" +
                           "  <dTimLastChange/>" +
                           "  <itemState/>" +
                           "  <serviceCategory/>" +
                           "  <comments/>" +
                           "  <acqusitionTimeZone/>" +
                           "  <extensionAny/>" +
                           "  <extensionNameValue/>" +
                           "</commonData>";

            return query;
        }
    }
}