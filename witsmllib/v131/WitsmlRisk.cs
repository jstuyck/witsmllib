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
     * Version specific implementation of the WitsmlRig type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlRisk : witsmllib.WitsmlRisk
    {

        /**
         * Create a new WITSML risk instance.
         *
         * @param server    Server this instance lives within. Non-null.
         * @param parent    Parent of instance. May be null.
         * @param parentId  ID of parent instance. May be null.
         */
        private WitsmlRisk(WitsmlServer server, String id, String name,
                           WitsmlObject parent, String parentId)
        
            :base(server, id, name, parent, parentId)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. Always null on this level.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML rig instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent, XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uid").Value;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace + "name").Value.Trim();//, element.getNamespace());

            WitsmlRisk risk = new WitsmlRisk(server, id, name, parent, parentId);
            risk.update(element);

            return risk;
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

            String query = "<risks xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <risk uidWell =\"" + uidWell + "\"" +
                           "       uidWellbore =\"" + uidWellbore + "\"" +
                           "       uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <objectReference/>" +
                           "    <type/>" +
                           "    <category/>" +
                           "    <subCategory/>" +
                           "    <mdHoleStart uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <mdHoleEnd uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdHoleStart uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvdHoleEnd uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <diaHole/>" +
                           "    <severityLevel/>" +
                           "    <probabilityLevel/>" +
                           "    <summary/>" +
                           "    <details/>" +
                           "    <identification/>" +
                           "    <mitigation/>" +
                           "  </risk>" +
                           "</risks>";

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

            reference = XmlUtil.update(element, "objectReference", reference);
            referenceType = XmlUtil.update(element, "objectReference", "object", referenceType);
            referenceId = XmlUtil.update(element, "objectReference", "uidRef", referenceId);
            type = XmlUtil.update(element, "type", type);
            category = XmlUtil.update(element, "category", category);
            subCategory = XmlUtil.update(element, "subCategory", subCategory);
        }
    }
}