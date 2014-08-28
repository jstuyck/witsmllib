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
     * Version specific implementation of the WitsmlRealtime type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlRealtime : witsmllib.WitsmlRealtime
    {

        /**
         * Create a new WITSML realtime instance.
         *
         * @param server    Server this instance lives within. Non-null.
         * @param parent    Parent of instance. May be null.
         * @param parentId  ID of parent instance. May be null.
         */
        private WitsmlRealtime(WitsmlServer server, String id, WitsmlObject parent, String parentId)
        
            :base(server, id, parent, parentId)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. Always null on this level.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML realtime instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent, XElement element)
        {
            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("idSub").Value;
            String parentId = element.Attribute("uidWellbore").Value;

            WitsmlRealtime realtime = new WitsmlRealtime(server, id, parent, parentId);
            realtime.update(element);

            return realtime;
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

            String query = "<realtimes version=\"" + WitsmlVersion.VERSION_1_4_0.getVersion() + "\"" +
                           "           xmlns=\"" + WitsmlVersion.VERSION_1_4_0.getNamespace() + "\">" +
                           "  <realtime uidWell =\"" + uidWell + "\"" +
                           "            uidWellbore =\"" + uidWellbore + "\"" +
                           "            idSub = \"\">" +
                           "    <dTim/>" +
                           "    <md/>" +
                           "    <sequence/>" +
                           "    <activityCode/>" +
                           "    <realtimeHeader>" +
                           "      <nameWell/>" +
                           "      <nameWellbore/>" +
                           "      <serviceCompany/>" +
                           "      <runNumber/>" +
                           "      <creationDate/>" +
                           "      <description/>" +
                           "      <groupDefinition>" +
                           "        <id/>" +
                           "        <multiplexed/>" +
                           "        <channelDefinition>" +
                           "          <mnemonic/>" +
                           "          <dataType/>" +
                           "          <classWitsml/>" +
                           "          <description/>" +
                           "          <mnemAlias/>" +
                           "          <sensorOffset uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "          <dataSource/>" +
                           WitsmlInterval.getQuery() +
                           "        </channelDefinition>" +
                           "      </groupDefinition>" +
                           "    </realtimeHeader>" +
                           WitsmlRealtimeChannel.getQuery() +
                           WitsmlCommonData.getQuery() +
                           "  </realtime>" +
                           "</realtimes>";

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

            time = XmlUtil.update(element, "dTim", time);
            md = XmlUtil.update(element, "md", md);
            sequenceNumber = XmlUtil.update(element, "sequence", sequenceNumber);
            activityCode = XmlUtil.update(element, "activityCode", activityCode);

            XElement realtimeHeaderElement = element.Element(element.Name.Namespace + "realtimeHeader");//, element.getNamespace());
            if (realtimeHeaderElement != null)
            {
                wellName = XmlUtil.update(realtimeHeaderElement, "nameWell", wellName);
                wellboreName = XmlUtil.update(realtimeHeaderElement, "nameWellbore", wellboreName);
                serviceCompany = XmlUtil.update(realtimeHeaderElement, "serviceCompany", serviceCompany);
                runNumber = XmlUtil.update(realtimeHeaderElement, "runNumber", runNumber);
                creationDate = XmlUtil.update(realtimeHeaderElement, "creationDate", creationDate);
                description = XmlUtil.update(realtimeHeaderElement, "description", description);
            }

            var channelElements = element.Elements(element.Name.Namespace + "channel");//, element.getNamespace());
            foreach (Object subElement in channelElements)
            {
                XElement channelElement = (XElement)subElement;
                WitsmlRealtimeChannel channel = new WitsmlRealtimeChannel(channelElement);

                channels.Add(channel);
            }

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}