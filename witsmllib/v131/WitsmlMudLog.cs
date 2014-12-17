using System;
using witsmllib.util;
using System.Xml.Linq;

namespace witsmllib.v131
{
    sealed class WitsmlMudLog : witsmllib.WitsmlMudLog
    {

        /// <summary>
        /// Create a new WITSML mud log instance.
        /// </summary>
        /// <param name="server">Server this instance lives within. Non-null.</param>
        /// <param name="id">ID of instance. May be null.</param>
        /// <param name="name">Name of instance. May be null.</param>
        /// <param name="parent">Parent of instance. May be null.</param>
        /// <param name="parentId">ID of parent instance. May be null.</param>
        private WitsmlMudLog(WitsmlServer server, String id, String name,
                            WitsmlObject parent, String parentId)
       
            :base(server, id, name, parent, parentId)
         {}

        /// <summary>
        /// Factory method for this type.
        /// </summary>
        /// <param name="server">Server the new instance lives within. Non-null.</param>
        /// <param name="parent">Parent instance. May be null.</param>
        /// <param name="element">XML element to create instance from. Non-null.</param>
        /// <returns>New WITSML wellbore instance. Never null.</returns>
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent, XElement element)
        {
            if (server == null)
                throw new ArgumentNullException("server cannot be null");
            if (element == null)
                throw new ArgumentNullException("element cannot be null");        

            String id = element.Attribute("uid").Value;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = XmlUtil.update(element, "name", (String)null);

            WitsmlMudLog mudLog = new WitsmlMudLog(server, id, name, parent, parentId);
            mudLog.update(element);

            return mudLog;
        }

        /// <summary>
        /// Return complete XML query for this type.
        /// </summary>
        /// <param name="id">ID of instance to get. May be empty to indicate all. Non-null.</param>
        /// <param name="parentId">Parent IDs. Closest first. May be empty if instances are accessed from the root. Non-null.</param>
        /// <returns>XML query. Never null.</returns>
        static String getQuery(String id, params String[] parentId)
        {
            if (id == null || id == "")
                throw new ArgumentNullException("id cannot be null");
            if (parentId == null)
                throw new ArgumentNullException("parentId cannot be null");          

            String uidWellbore = parentId.Length > 0 ? parentId[0] : "";
            String uidWell = parentId.Length > 1 ? parentId[1] : "";

            String query = "<mudLogs xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <mudLog uidWell = \"" + uidWell + "\"" +
                           "          uidWellbore = \"" + uidWellbore + "\"" +
                           "          uid = \"" + id + "\">" +
                           "      <name/>" +
                           "      <dTim/>" +
                           "      <mudLogCompany/>" +
                           "      <mudLogEngineers/>" +
                           "      <startMd uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <endMd uom=\"" + WitsmlServer.distUom+"\"/>" +
                           WitsmlCommonData.getQuery() +
                           "  </mudLog>" +
                           "</mudLogs>";

            return query;
        }

        /// <summary>
        /// Parse the specified DOM element and instantiate the properties of this instance.
        /// </summary>
        /// <param name="element">XML element to parse. Non-null.</param>
        void update(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element cannot be null");          

            time = XmlUtil.update(element, "dTim", time);
            mudLogCompany = XmlUtil.update(element, "mudLogCompany", mudLogCompany);
            mudLogEngineers = XmlUtil.update(element, "mudLogEngineers", mudLogEngineers);
            mdStart = XmlUtil.update(element, "startMd", mdStart);
            mdEnd = XmlUtil.update(element, "endMd", mdEnd);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}