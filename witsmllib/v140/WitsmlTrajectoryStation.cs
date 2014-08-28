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
using System;
using witsmllib.util;
using System.Xml.Linq;
namespace witsmllib.v140
{


    /**
     * Version specific implementation of the WitsmlTrajectoryStation type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    sealed class WitsmlTrajectoryStation : witsmllib.WitsmlTrajectoryStation
    {

        /**
         * Create a WITSML trajectory station instance from the given
         * XML element node.
         *
         * @param element    XML element to parse. Non-null.
         * @param stationNo  Station number.
         */
        internal WitsmlTrajectoryStation(XElement element, int stationNo)
        
            :base(stationNo){

            id = element.Attribute("uid").Value;

            time = XmlUtil.update(element, "dTimStn", time);
            type = XmlUtil.update(element, "typeTrajStation", type);
            surveyToolType = XmlUtil.update(element, "surveyToolType", surveyToolType);
            md = XmlUtil.update(element, "md", md);
            tvd = XmlUtil.update(element, "tvd", tvd);
            inclination = XmlUtil.update(element, "incl", inclination);
            azimuth = XmlUtil.update(element, "azi", azimuth);
            toolfaceMagneticAngle = XmlUtil.update(element, "mtf", toolfaceMagneticAngle);
            toolfaceGravityAngle = XmlUtil.update(element, "gtf", toolfaceGravityAngle);
            north = XmlUtil.update(element, "dispNs", north);
            east = XmlUtil.update(element, "dispEw", east);
            verticalSectionDistance = XmlUtil.update(element, "vertSect", verticalSectionDistance);
            dls = XmlUtil.update(element, "dls", dls);
            turnRate = XmlUtil.update(element, "rateTurn", turnRate);
            buildRate = XmlUtil.update(element, "rateBuild", buildRate);
            dMd = XmlUtil.update(element, "mdDelta", dMd);
            dTvd = XmlUtil.update(element, "tvdDelta", dTvd);
            errorModel = XmlUtil.update(element, "modelToolError", errorModel);
            gravityUncertainty = XmlUtil.update(element, "gravTotalUncert", gravityUncertainty);
            dipAngleUncertainty = XmlUtil.update(element, "dipAngleUncert", dipAngleUncertainty);
            magneticUncertainty = XmlUtil.update(element, "magTotalUncert", magneticUncertainty);
            _isAccelerometerCorrectionUsed = XmlUtil.update(element, "gravAccelCorUsed", _isAccelerometerCorrectionUsed);
            _isMagnetometerCorrectionUsed = XmlUtil.update(element, "magXAxialCorUsed", _isMagnetometerCorrectionUsed);
            _isSagCorrectionUsed = XmlUtil.update(element, "sagCorUsed", _isSagCorrectionUsed);
            _isDrillStringMagnetismCorrectionUsed = XmlUtil.update(element, "magDrlstrCorUsed", _isDrillStringMagnetismCorrectionUsed);
            gravitationFieldReference = XmlUtil.update(element, "gravTotalFieldReference", gravitationFieldReference);
            magneticFieldReference = XmlUtil.update(element, "magTotalFieldReference", magneticFieldReference);
            magneticDipAngleReference = XmlUtil.update(element, "magDipAngleReference", magneticDipAngleReference);
            magneticModel = XmlUtil.update(element, "magModelUsed", magneticModel);
            magneticModelValidInterval = XmlUtil.update(element, "magModelValid", magneticModelValidInterval);
            gravitationalModel = XmlUtil.update(element, "geoModelUsed", gravitationalModel);
            status = XmlUtil.update(element, "statusTrajStation", status);

            XElement locationElement = element.Element(element.Name.Namespace+"location"); //, element.getNamespace());
            if (locationElement != null)
                location = new WitsmlLocation(locationElement);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
        public static String getQuery()
        {
            String query = "<trajectoryStation uid = \"\">" +
                           "    <dTimStn/>" +
                           "    <typeTrajStation/>" +
                           "    <typeSurveyTool/>" +
                           "    <md uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <tvd uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <incl uom=\"rad\"/>" +
                           "    <azi uom=\"rad\"/>" +
                           "    <mtf uom=\"rad\"/>" +
                           "    <gtf uom=\"rad\"/>" +
                           "    <dispNs uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <dispEw uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <vertSect uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <dls/>" +
                           "    <rateTurn/>" +
                           "    <rateBuild/>" +
                           "    <mdDelta uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <tvdDelta uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <modelToolError/>" +
                           "    <gravTotalUncert/>" +
                           "    <dipAngleUncert uom=\"rad\"/>" +
                           "    <magTotalUncert/>" +
                           "    <gravAccelCorUsed/>" +
                           "    <magXAxialCorUsed/>" +
                           "    <sagCorUsed/>" +
                           "    <magDrlstrCorUsed/>" +
                           "    <gravTotalFieldReference/>" +
                           "    <magTotalFieldReference/>" +
                           "    <magDipAngleReference uom=\"rad\"/>" +
                           "    <magModelUsed/>" +
                           "    <magModelValid/>" +
                           "    <geoModelUsed/>" +
                           "    <statusTrajStation/>" +
                           "    <rawData/>" +
                           "    <corUsed/>" +
                           "    <valid/>" +
                           "    <matrixCov/>" +
                           "    <location>" +
                           WitsmlLocation.getQuery() +
                           "    </location>" +
                           "    <sourceStation/>" +
                           WitsmlCommonData.getQuery() +
                           "</trajectoryStation>";

            return query;
        }
    }
}