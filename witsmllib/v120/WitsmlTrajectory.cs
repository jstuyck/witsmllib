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
     * Version specific implementation of the WitsmlTrajectory type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlTrajectory : witsmllib.WitsmlTrajectory
    {

        /**
         * Create a new WITSML wellbore instance.
         *
         * @param server  Server this instance lives within. Non-null.
         * @param id      ID of instance. May be null.
         * @param name    Name of instance. May be null.
         * @param parent  Parent of instance. May be null.
         * @param parent  ID of parent instance. May be null.
         */
        private WitsmlTrajectory(WitsmlServer server, String id, String name,
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
        static WitsmlObject newInstance(WitsmlServer server,
                                        WitsmlObject parent, XElement element)
        {
            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uidTraj").Value;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace+"nameTraj").Value.Trim(); // element.getNamespace());

            WitsmlTrajectory witsmlTrajectory = new WitsmlTrajectory(server, id, name, parent, parentId);
            witsmlTrajectory.update(element);

            return witsmlTrajectory;
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

            String query = "<trajectorys xmlns=\"" + WitsmlVersion.VERSION_1_2_0.getNamespace() + "\">" +
                           "  <trajectory uidWell = \"" + uidWell + "\"" +
                           "              uidWellbore =\"" + uidWellbore + "\"" +
                           "              uidTraj = \"" + id + "\">" +
                           "    <nameTraj/>" +
                           "    <parentTrajectory>" +
                           "      <trajectoryReference/>" +
                           "    </parentTrajectory>" +
                           "    <dTimTrajStart/>" +
                           "    <dTimTrajEnd/>" +
                           "    <mdMn uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <mdMx uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <serviceCompany/>" +
                           "    <magDeclUsed uom=\"rad\"/>" +
                           "    <gridCorUsed uom=\"rad\"/>" +
                           "    <aziVertSect uom=\"rad\"/>" +
                           "    <dispNsVertSectOrig uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <dispEwVertSectOrig uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "    <definitive/>" +
                           "    <memory/>" +
                           "    <finalTraj/>" +
                           "    <aziRef/>" +
                           WitsmlTrajectoryStation.getQuery() +
                           WitsmlCommonData.getQuery() +
                           "  </trajectory>" +
                           "</trajectorys>";

            return query;
        }

        /**
         * Parse the specified DOM element and instantiate the properties
         * of this instance.
         *
         * @param element  XML element to parse. Non-null.
         */
        private void update(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            XElement parentTrajectoryElement = element.Element(element.Name.Namespace +"parentTrajectory");//, element.getNamespace());
            if (parentTrajectoryElement != null)
            {
                XElement trajectoryReferenceElement = parentTrajectoryElement.Element(element.Name.Namespace + "trajectoryReference");//,element.getNamespace());
                if (trajectoryReferenceElement != null)
                {
                    parentTrajectoryName = trajectoryReferenceElement.Value.Trim(); //.getTextTrim();
                    parentTrajectoryId = trajectoryReferenceElement.Attribute("uidRef").Value ;
                }
            }

            stationsMeasurementStart = XmlUtil.update(element, "dTimTrajStart", stationsMeasurementStart);
            stationsMeasurementEnd = XmlUtil.update(element, "dTimTrajEnd", stationsMeasurementEnd);
            mdMin = XmlUtil.update(element, "mdMn", mdMin);
            mdMax = XmlUtil.update(element, "mdMx", mdMax);
            serviceCompany = XmlUtil.update(element, "serviceCompany", serviceCompany);
            magneticAngle = XmlUtil.update(element, "magDeclUsed", magneticAngle);
            gridCorrection = XmlUtil.update(element, "gridCorUsed", gridCorrection);
            azimuthOfVerticalSection = XmlUtil.update(element, "aziVertSect", azimuthOfVerticalSection);
            originNs = XmlUtil.update(element, "dispNsVertSectOrig", originNs);
            originEw = XmlUtil.update(element, "dispEwVertSectOrig", originEw);
            _isDefinitive = XmlUtil.update(element, "definitive", _isDefinitive);
            _isMemoryDump = XmlUtil.update(element, "memory", _isMemoryDump);
            _isFinal = XmlUtil.update(element, "finalTraj", _isFinal);
            azimuthReference = XmlUtil.update(element, "aziRef", azimuthReference);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData") ; //, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);

            var trajectoryStationElements = element.Elements(element.Name.Namespace +"trajectoryStation"); //,element.getNamespace());

            // If we asked for stations, we clear the present content,
            // if not, we leave the present content alone
            //if (!trajectoryStationElements.isEmpty())
            foreach (var itm in trajectoryStationElements)
            {
                stations.Clear();
                break; //just do once
            }

            int i = 0;
            foreach (Object subElement in trajectoryStationElements)
            {
                XElement stationElement = (XElement)subElement;

                WitsmlTrajectoryStation station = new WitsmlTrajectoryStation(stationElement, i++);
                stations.Add(station);
            }
        }
    }
}