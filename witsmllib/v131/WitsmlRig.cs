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
    sealed class WitsmlRig : witsmllib.WitsmlRig
    {

        /**
         * Create a new WITSML rig instance.
         *
         * @param server    Server this instance lives within. Non-null.
         * @param parent    Parent of instance. May be null.
         * @param parentId  ID of parent instance. May be null.
         */
        private WitsmlRig(WitsmlServer server, String id, String name,
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
            String name = element.Element(element.Name.Namespace + "name").Value.Trim(); //, element.getNamespace());

            WitsmlRig rig = new WitsmlRig(server, id, name, parent, parentId);
            rig.update(element);

            return rig;
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

            String query = "<rigs xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <rig uidWell =\"" + uidWell + "\"" +
                           "       uidWellbore =\"" + uidWellbore + "\"" +
                           "       uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <owner/>" +
                           "    <typeRig/>" +
                           "    <manufacturer/>" +
                           "    <yearEntService/>" +
                           "    <classRig/>" +
                           "    <approvals/>" +
                           "    <registration/>" +
                           "    <telNumber/>" +
                           "    <faxNumber/>" +
                           "    <emailAddress/>" +
                           "    <nameContact/>" +
                           "    <ratingDrillDepth uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <isOffshore/>" +
                           "    <dtmRefToDtmPerm uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <airGap uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <dtmReference/>" +
                           "    <dTimStartOp/>" +
                           "    <dTimEndOp/>" +
                           "    <bop>" +
                           "    </bop>" +
                           "    <pits>" +
                           "      <indexPit/>" +
                           "      <dTimInstall/>" +
                           "      <dTimRemove/>" +
                           "      <capMx uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "      <owner/>" +
                           "      <type/>" +
                           "      <isActive/>" +
                           "    </pits>" +
                           "    <pumps>" +
                           "      <indexPump/>" +
                           "      <manufacturer/>" +
                           "      <model/>" +
                           "      <dTimInstall/>" +
                           "      <dTimRemove/>" +
                           "      <owner/>" +
                           "      <typePump/>" +
                           "      <numCyl/>" +
                           "      <odRod uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <idLiner uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <pumpAction/>" +
                           "      <eff/>" +
                           "      <lenStroke uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "      <presMx/>" +
                           "      <powHydMx/>" +
                           "      <spmMx/>" +
                           "      <displacement uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "      <presDamp/>" +
                           "      <volDamp uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "      <powMechMx/>" +
                           "    </pumps>" +
                           "    <shakers>" + // TODO
                           "    </shakers>" +
                           "    <centrifuge>" + // TODO
                           "    </centrifuge>" +
                           "    <hydroclone>" + // TODO
                           "    </hydroclone>" +
                           "    <degasser>" + // TODO
                           "    </degasser>" +
                           "    <surfaceEquipment>" + // TODO
                           "    </surfaceEquipment>" +
                           "    <numDerricks/>" +
                           "    <typeDerrick/>" +
                           "    <ratingDerrick/>" +
                           "    <htDerrick uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <ratingHkld/>" +
                           "    <capWindDerrick/>" +
                           "    <wtBlock/>" +
                           "    <ratingBlock/>" +
                           "    <numBlockLines/>" +
                           "    <typeHook/>" +
                           "    <ratingHook/>" +
                           "    <sizeDrillLine uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <typeDrawWorks/>" +
                           "    <powerDrawWorks/>" +
                           "    <ratingDrawWorks/>" +
                           "    <motorDrawWorks/>" +
                           "    <descBrake/>" +
                           "    <typeSwivel/>" +
                           "    <ratingSwivel/>" +
                           "    <rotSystem/>" +
                           "    <descRotSystem/>" +
                           "    <ratingTqRotSys/>" +
                           "    <rotSizeOpening uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <ratingRotSystem/>" +
                           "    <scrSystem/>" +
                           "    <pipeHandlingSystem/>" +
                           "    <capBulkMud uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <capLiquidMud uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <capDrillWater uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <capPotableWater uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <capFuel uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <capBulkCement uom=\"" + WitsmlServer.volUom+"\"/>" +
                           "    <mainEngine/>" +
                           "    <generator/>" +
                           "    <cementUnit/>" +
                           "    <numBunks/>" +
                           "    <bunksPerRoom/>" +
                           "    <numCranes/>" +
                           "    <numAnch/>" +
                           "    <moorType/>" +
                           "    <numGuideTens/>" +
                           "    <numRiserTens/>" +
                           "    <varDeckLdMx uom=\"" + WitsmlServer.forceUom+"\"/>" +
                           "    <vdlStorm uom=\"" + WitsmlServer.forceUom+"\"/>" +
                           "    <numThrusters/>" +
                           "    <azimuthing/>" +
                           "    <motionCompensationMn uom=\"" + WitsmlServer.forceUom+"\"/>" +
                           "    <motionCompensationMx uom=\"" + WitsmlServer.forceUom+"\"/>" +
                           "    <strokeMotionCompensation uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <riserAngleLimit uom=\"rad\"/>" +
                           "    <heaveMx uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <gantry/>" +
                           "    <flares/>" +
                           "  </rig>" +
                           "</rigs>";

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

            owner = XmlUtil.update(element, "owner", owner);
            rigType = XmlUtil.update(element, "typeRig", rigType);
            manufacturer = XmlUtil.update(element, "manufacturer", manufacturer);
            startYear = XmlUtil.update(element, "yearEntService", startYear);
            rigClass = XmlUtil.update(element, "classRig", rigClass);
            approvals = XmlUtil.update(element, "approvals", approvals);
            registrationLocation = XmlUtil.update(element, "registration", registrationLocation);
            phoneNumber = XmlUtil.update(element, "telNumber", phoneNumber);
            faxNumber = XmlUtil.update(element, "faxNumber", faxNumber);
            emailAddress = XmlUtil.update(element, "emailAddress", emailAddress);
            contactName = XmlUtil.update(element, "nameContact", contactName);
            drillDepthRating = XmlUtil.update(element, "ratingDrillDepth", drillDepthRating);
            waterDepthRating = XmlUtil.update(element, "ratingWaterDepth", waterDepthRating);
            _isOffshore = XmlUtil.update(element, "isOffshore", _isOffshore);
            drillingDatumToPermanentDatumDistance = XmlUtil.update(element, "dtmRefToDtmPerm", drillingDatumToPermanentDatumDistance);
            airGap = XmlUtil.update(element, "airGap", airGap);
            datum = XmlUtil.update(element, "dtmReference", datum);
            operationStartTime = XmlUtil.update(element, "dTimStartOp", operationStartTime);
            operationEndTime = XmlUtil.update(element, "dTimEndOp", operationEndTime);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}