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
     * Version specific implementation of the WitsmlFluidsReport type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlFluidsReport : witsmllib.WitsmlFluidsReport
    {

        /**
         * Create a new WITSML fluids report instance.
         *
         * @param server    Server this instance lives within. Non-null.
         * @param id        ID of instance. May be null.
         * @param name      Name of instance. May be null.
         * @param parent    Parent of instance. May be null.
         * @param parentId  ID of parent instance. May be null.
         */
        private WitsmlFluidsReport(WitsmlServer server, String id, String name,
                                   WitsmlObject parent, String parentId)
        
            :base(server, id, name, parent, parentId)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. May be null.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML fluids report instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent, XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uidReport").Value ;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace +"nameReport").Value.Trim();//, element.getNamespace());

            WitsmlFluidsReport fluidsReport = new WitsmlFluidsReport(server, id, name, parent, parentId);
            fluidsReport.update(element);

            return fluidsReport;
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

            String query = "<fluidsReports xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <fluidsReport uidWell = \"" + uidWell + "\"" +
                           "                uidWellbore = \"" + uidWellbore + "\"" +
                           "                uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <dTim/>" +
                           "    <md uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <tvd uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <numReport/>" +
                           "    <fluids>" + // TODO
                           "    </fluids>" +
                           WitsmlCommonData.getQuery() +
                           "  </fluidsReport>" +
                           "</fluidsReports>";

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

            time = XmlUtil.update(element, "dTim", time);
            md = XmlUtil.update(element, "md", md);
            tvd = XmlUtil.update(element, "tvd", tvd);
            reportNumber = XmlUtil.update(element, "numReports", reportNumber);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}