using System;
using witsmllib.util;
using System.Xml.Linq;

namespace witsmllib.v131
{

    /// <summary>
    ///  Version dependent part of the generic WitsmlTrajectory class.
    /// </summary>
    sealed class WitsmlTrajectory : witsmllib.WitsmlTrajectory
    {

        /// <summary>
        /// Create a trajectory object with specified ID and name and parent.
        /// </summary>
        /// <param name="server">The WITSML server. Non-null.</param>
        /// <param name="id">ID of this trajectory. Non-null.</param>
        /// <param name="name">Name of this trajectory. Non-null.</param>
        /// <param name="parent">Parent of this trajectory. May be null.</param>
        /// <param name="parentId">ParentId of this trajectory if parent is null. May be null.</param>
        private WitsmlTrajectory(WitsmlServer server, String id, String name,
                                 WitsmlObject parent, String parentId)

            : base(server, id, name, parent, parentId)
        { }

        /// <summary>
        /// Create a new instance from the given XML element.
        /// </summary>
        /// <param name="server">The WITSML server. Non-null.</param>
        /// <param name="parent">Parent instance. May be null.</param>
        /// <param name="element">XElement to create instance from. Non-null.</param>
        /// <returns> New instance. Never null.</returns>
        static WitsmlObject newInstance(WitsmlServer server,
                                               WitsmlObject parent, XElement element)
        {
            if (server == null)
                throw new ArgumentException("server cannot be null");
            if (element == null)
                throw new ArgumentException("element cannot be null");

            String id = element.Attribute("uid").Value;
            String parentId = element.Attribute("uidWellbore").Value;
            String name = element.Element(element.Name.Namespace + "name").Value.Trim(); //, element.getNamespace());

            WitsmlTrajectory witsmlTrajectory = new WitsmlTrajectory(server, id, name, parent, parentId);
            witsmlTrajectory.update(element);

            return witsmlTrajectory;
        }

        /// <summary>
        /// Return query for instances of this type.
        /// </summary>
        /// <param name="id">ID of instance to find. Empty to find all. Non-null.</param>
        /// <param name="parentId">ID(s) of parent. Closest parent first. Must contain at least one element.</param>
        /// <returns>XML query for identifying objects of this type. Never null.</returns>
        static String getQuery(String id, params String[] parentId)
        {
            if (id == null)
                throw new ArgumentException("id cannot be null");
            if (parentId == null)
                throw new ArgumentException("parentId cannot be null");

            String uidWellbore = parentId.Length > 0 ? parentId[0] : "";
            String uidWell = parentId.Length > 1 ? parentId[1] : "";

            String query = "<trajectorys xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <trajectory uidWell = \"" + uidWell + "\"" +
                           "              uidWellbore =\"" + uidWellbore + "\"" +
                           "              uid = \"" + id + "\">" +
                           "    <name/>" +
                           "    <objectGrowing/>" +
                           "    <parentTrajectory>" +
                           "      <trajectoryReference/>" +
                           "    </parentTrajectory>" +
                           "    <dTimTrajStart/>" +
                           "    <dTimTrajEnd/>" +
                           "    <mdMn uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <mdMx uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <serviceCompany/>" +
                           "    <magDeclUsed uom=\"rad\"/>" +
                           "    <gridCorUsed uom=\"rad\"/>" +
                           "    <aziVertSect uom=\"rad\"/>" +
                           "    <dispNsVertSectOrig uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <dispEwVertSectOrig uom=\"" + WitsmlServer.distUom + "\"/>" +
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
        void update(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            XElement parentTrajectoryElement = element.Element(element.Name.Namespace + "parentTrajectory"); //, element.getNamespace());
            if (parentTrajectoryElement != null)
            {
                XElement trajectoryReferenceElement = parentTrajectoryElement.Element(element.Name.Namespace + "trajectoryReference");//,element.getNamespace());
                if (trajectoryReferenceElement != null)
                {
                    parentTrajectoryName = trajectoryReferenceElement.Value.Trim(); //.getTextTrim();
                    XAttribute parentTrajectoryAttribute = trajectoryReferenceElement.Attribute("uidRef");
                    if (parentTrajectoryAttribute != null)
                        parentTrajectoryId = parentTrajectoryAttribute.Value;
                }
            }

            _isGrowing = XmlUtil.update(element, "objectGrowing", _isGrowing);
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

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData"); //, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);

            var trajectoryStationElements = element.Elements(element.Name.Namespace + "trajectoryStation"); //,element.getNamespace());

            // If we asked for stations, we clear the present content,
            // if not, we leave the present content alone
            //if (!trajectoryStationElements.isEmpty())
            foreach (var itm in trajectoryStationElements)
            {
                stations.Clear();
                break; //only once
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