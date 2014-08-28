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
     * Version specific implementation of the WitsmlBhaRun type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlBhaRun : witsmllib.WitsmlBhaRun
    {

        /**
         * Create a new WITSML bha run instance.
         *
         * @param server    Server this instance lives within. Non-null.
         * @param id        ID of instance. May be null.
         * @param name      Name of instance. May be null.
         * @param parent    Parent of instance. May be null.
         * @param parentId  ID of parent instance. May be null.
         */
        private WitsmlBhaRun(WitsmlServer server, String id, String name,
                             WitsmlObject parent, String parentId)
        
            :base(server, id, name, parent, parentId)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. May be null.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML bha run instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent, XElement element)
        {
            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uidTubularAssy").Value ;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace + "nameTubularAssy").Value.Trim(); //, element.getNamespace());

            WitsmlBhaRun bhaRun = new WitsmlBhaRun(server, id, name, parent, parentId);
            bhaRun.update(element);

            return bhaRun;
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

            String query = "<bhaRuns xmlns=\"" + WitsmlVersion.VERSION_1_2_0.getNamespace() + "\">" +
                           "  <bhaRun uidWell = \"" + uidWell + "\"" +
                           "          uidWellbore = \"" + uidWellbore + "\"" +
                           "          uidTubularAssy = \"" + id + "\">" +
                           "    <nameTubularAssy/>" +
                           "    <dTimStart/>" +
                           "    <dTimStop/>" +
                           "    <dTimStartDrilling/>" +
                           "    <dTimStopDrilling/>" +
                           "    <planDogleg/>" +
                           "    <actDogleg/>" +
                           "    <actDoglegMx/>" +
                           "    <statusBha/>" +
                           "    <numBitRun/>" +
                           "    <numStringRun/>" +
                           "    <reasonTrip/>" +
                           "    <objectiveBha/>" +
                           "    <drillingParams>" +
                           "      <eTimOpBit uom=\"s\"/>" +
                           "      <mdHoleStart uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <mdHoleStop uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <uidTubularAssy/>" +
                           "      <hkldRot/>" +
                           "      <overPull/>" +
                           "      <slackOff/>" +
                           "      <hkldUp/>" +
                           "      <hkldDn/>" +
                           "      <tqOnBotAv/>" +
                           "      <tqOnBotMx/>" +
                           "      <tqOnBotMn/>" +
                           "      <tqOffBotAv/>" +
                           "      <tqDhAv/>" +
                           "      <wtAboveJar/>" +
                           "      <wtBelowJar/>" +
                           "      <wtMud/>" +
                           "      <flowratePump/>" +
                           "      <powBit/>" +
                           "      <velNozzleAv uom=\"m/s\"/>" +
                           "      <presDropBit uom=\"pa\"/>" +
                           "      <cTimHold/>" +
                           "      <cTimSteering/>" +
                           "      <cTimDrillRot/>" +
                           "      <cTimDrillSlid/>" +
                           "      <cTimCirc/>" +
                           "      <cTimReam/>" +
                           "      <distDrillRot/>" +
                           "      <distDrillSlid/>" +
                           "      <distReam/>" +
                           "      <distHold/>" +
                           "      <distSteering/>" +
                           "      <rpmAv/>" +
                           "      <rpmMx/>" +
                           "      <rpmMn/>" +
                           "      <rpmAvDh/>" +
                           "      <ropAv/>" +
                           "      <ropMx/>" +
                           "      <ropMn/>" +
                           "      <wobAv/>" +
                           "      <wobMx/>" +
                           "      <wobMn/>" +
                           "      <wobAvDh/>" +
                           "      <reasonTrip/>" +
                           "      <objectiveBha/>" +
                           "      <aziTop/>" +
                           "      <aziBottom/>" +
                           "      <inclStart/>" +
                           "      <inclMx/>" +
                           "      <inclMn/>" +
                           "      <inclStop/>" +
                           "      <tempMudDhMx uom=\"degC\"/>" +
                           "      <presPumpAv uom=\"psi\"/>" +
                           "      <flowrateBit/>" +
                           "      <comments/>" +
                           "    </drillingParams>" +
                           WitsmlCommonData.getQuery() +
                           "  </bhaRun>" +
                           "</bhaRuns>";

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

            startTime = XmlUtil.update(element, "dTimStart", startTime);
            endTime = XmlUtil.update(element, "dTimStop", endTime);
            drillingStartTime = XmlUtil.update(element, "dTimStartDrilling", drillingStartTime);
            drillingEndTime = XmlUtil.update(element, "dTimStopDrilling", drillingEndTime);
            plannedDls = XmlUtil.update(element, "planDogleg", plannedDls);
            actualDls = XmlUtil.update(element, "actDogleg", actualDls);
            maxActualDls = XmlUtil.update(element, "actDoglegMx", maxActualDls);
            status = XmlUtil.update(element, "statusBha", status);
            bitRunNumber = XmlUtil.update(element, "numBitRun", bitRunNumber);
            stringRunNumber = XmlUtil.update(element, "numStringRun", stringRunNumber);
            reason = XmlUtil.update(element, "reasonTrip", reason);
            objective = XmlUtil.update(element, "objectiveBha", objective);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}