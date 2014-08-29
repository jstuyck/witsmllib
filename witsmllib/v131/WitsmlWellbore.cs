using System;
using System.Xml.Linq;
using witsmllib.util;

namespace witsmllib.v131
{
    /// <summary>
    /// Version specific implementation of the WitsmlWellbore type.
    /// </summary>
    sealed class WitsmlWellbore : witsmllib.WitsmlWellbore
    {
        /// <summary>
        /// Create a new WITSML wellbore instance.
        /// </summary>
        /// <param name="server">Server this instance lives within. Non-null.</param>
        /// <param name="id">ID of instance. May be null.</param>
        /// <param name="name">Name of instance. May be null.</param>
        /// <param name="parent">Parent of instance. May be null.</param>
        /// <param name="parentId">ID of parent instance. May be null.</param>
        private WitsmlWellbore(WitsmlServer server, String id, String name,
                               WitsmlObject parent, String parentId)
        
            :base(server, id, name, parent, parentId)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. May be null.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML wellbore instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent,
                                        XElement element)
        {
            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String parentId = element.Attribute("uidWell").Value ;
            String id = element.Attribute("uid").Value;
            String name = element.Element(element.Name.Namespace +"name").Value.Trim(); //, element.getNamespace());

            WitsmlWellbore wellbore = new WitsmlWellbore(server, id, name, parent, parentId);
            wellbore.update(element);

            return wellbore;
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

            String uidWell = parentId.Length > 0 ? parentId[0] : "";

            String query = "<wellbores xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <wellbore uidWell = \"" + uidWell + "\"" +
                           "            uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <nameWell/>" +
                           "    <parentWellbore/>" +
                           "    <number/>" +
                           "    <suffixAPI/>" +
                           "    <numGovt/>" +
                           "    <statusWellbore/>" +
                           "    <purposeWellbore/>" +
                           "    <typeWellbore/>" +
                           "    <shape/>" +
                           "    <dTimKickoff/>" +
                           "    <achievedTD/>" +
                           "    <mdCurrent uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdCurrent uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <mdKickoff uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdKickoff uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <mdPlanned uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdPlanned uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <mdSubSeaPlanned uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdSubSeaPlanned uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <dayTarget/>" +
                           WitsmlCommonData.getQuery() +
                           "    <customData/>" +
                           "  </wellbore>" +
                           "</wellbores>";

            return query;
        }

        /**
         * Parse the specified DOM element and instantiate the properties
         * of this instance.
         *
         * @param element  XML element to parse. Non-null.
         */
        void update(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            number = XmlUtil.update(element, "number", number);
            apiSuffix = XmlUtil.update(element, "suffixAPI", apiSuffix);
            wellboreNumber = XmlUtil.update(element, "numGovt", wellboreNumber);
            status = XmlUtil.update(element, "statusWellbore", status);
            purpose = XmlUtil.update(element, "purposeWellbore", purpose);
            type = XmlUtil.update(element, "typeWellbore", type);
            shape = XmlUtil.update(element, "shape", shape);
            kickoffTime = XmlUtil.update(element, "dTimKickoff", kickoffTime);
            _isTotalDepthReached = XmlUtil.update(element, "achievedTD", _isTotalDepthReached);
            mdCurrent = XmlUtil.update(element, "mdCurrent", mdCurrent);
            tvdCurrent = XmlUtil.update(element, "tvdCurrent", tvdCurrent);
            mdKickoff = XmlUtil.update(element, "mdKickoff", mdKickoff);
            tvdKickoff = XmlUtil.update(element, "tvdKickoff", tvdKickoff);
            mdPlanned = XmlUtil.update(element, "mdPlanned", mdPlanned);
            tvdPlanned = XmlUtil.update(element, "tvdPlanned", tvdPlanned);
            mdSubSeaPlanned = XmlUtil.update(element, "mdSubSeaPlanned", mdSubSeaPlanned);
            tvdSubSeaPlanned = XmlUtil.update(element, "tvdSubSeaPlanned", tvdSubSeaPlanned);
            nTargetDays = XmlUtil.update(element, "dayTarget", nTargetDays);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}