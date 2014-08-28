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
namespace witsmllib
{

    //import java.util.Date;

    /**
     * Java representation of a WITSML "trajectoryStation".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public abstract class WitsmlTrajectoryStation : IComparable<WitsmlTrajectoryStation>
    {
        private  int stationNo;

        protected String id;
        protected DateTime? time;
        protected String type; // typeTrajStation
        protected String surveyToolType; // typeSurveyTool
        protected Value md;  // md
        protected Value tvd; // tvd
        protected Value inclination; // incl
        protected Value azimuth; // azi
        protected Value toolfaceMagneticAngle; // mtf
        protected Value toolfaceGravityAngle; // gtf
        protected Value north; // dispNs
        protected Value east; // dispEw
        protected Value verticalSectionDistance; // vertSect
        protected Value dls; // dls
        protected Value turnRate; // rateTurn
        protected Value buildRate; // rateBuild
        protected Value dMd; // mdDelta
        protected Value dTvd; // tvdDelta
        protected String errorModel; // modelToolError
        protected Value gravityUncertainty; // gravTotalUncert
        protected Value dipAngleUncertainty; // dipAngleUncert
        protected Value magneticUncertainty; // magTotalUncert
        protected Boolean? _isAccelerometerCorrectionUsed; // gravAccelCorUsed
        protected Boolean? _isMagnetometerCorrectionUsed; // magXAxialCorUsed
        protected Boolean? _isSagCorrectionUsed; // sagCorUsed
        protected Boolean? _isDrillStringMagnetismCorrectionUsed; // magDrlstrCorUsed
        protected Value gravitationFieldReference; // gravTotalFieldReference
        protected Value magneticFieldReference; // magTotalFieldReference
        protected Value magneticDipAngleReference; // magDipAngleReference
        protected String magneticModel; // magModelUsed
        protected String magneticModelValidInterval; // magModelValid
        protected String gravitationalModel; // geoModelUsed
        protected String status; // statusTrajStation
        protected WitsmlLocation location;
        protected WitsmlCommonData commonData;

        /**
         * Create a trajectory station with given station number.
         *
         * @param stationNo  Station number of this trajectory station.
         */
        protected WitsmlTrajectoryStation(int stationNo)
        {
            this.stationNo = stationNo;
        }

        /**
         * Get ID of this station.
         *
         * @return  ID of this station.
         */
        public String getId()
        {
            return id;
        }

        /**
         * Return number in the sequence of this station.
         *
         * @return  Number in the sequence of this station. 0-based.
         */
        public int getStationNo()
        {
            return stationNo;
        }

        /**
         * Get time of this station.
         *
         * @return  Time of this station.
         */
        public DateTime? getTime()
        {
            return time;// != null ? new Date(time.getTime()) : null;
        }

        /**
         * Get type of this station.
         *
         * @return  Type of this station.
         */
        public String getType()
        {
            return type;
        }

        public String getSurveyToolType()
        {
            return surveyToolType;
        }

        /**
         * Get MD of this station.
         *
         * @return  MD of this station.
         */
        public Value getMd()
        {
            return md;
        }

        /**
         * Get TVD of this station.
         *
         * @return  TVD of this station.
         */
        public Value getTvd()
        {
            return tvd;
        }

        /**
         * Get inclination of this station.
         *
         * @return  Inclination of this station.
         */
        public Value getInclination()
        {
            return inclination;
        }

        /**
         * Get azimuth of this station.
         *
         * @return  Azimuth of this station.
         */
        public Value getAzimuth()
        {
            return azimuth;
        }

        /**
         * Get angle of magnetic toolface of this station.
         *
         * @return  Angle of magnetic toolface of this station.
         */
        public Value getToolfaceMagneticAngle()
        {
            return toolfaceMagneticAngle;
        }

        /**
         * Get angle of gravity toolface of this station.
         *
         * @return  Angle of gravity toolface of this station.
         */
        public Value getToolfaceGravityAngle()
        {
            return toolfaceGravityAngle;
        }

        /**
         * Get north position of this station.
         *
         * @return  North position of this station.
         */
        public Value getNorth()
        {
            return north;
        }

        /**
         * Get south position of this station.
         *
         * @return  South position of this station.
         */
        public Value getEast()
        {
            return east;
        }

        public Value getVerticalSectionDistance()
        {
            return verticalSectionDistance;
        }

        public Value getDls()
        {
            return dls;
        }

        public Value getTurnRate()
        {
            return turnRate;
        }

        public Value getBuildRate()
        {
            return buildRate;
        }

        public Value getDMd()
        {
            return dMd;
        }

        public Value getDTvd()
        {
            return dTvd;
        }

        public String getErrorModel()
        {
            return errorModel;
        }

        public Value getGravityUncertainty()
        {
            return gravityUncertainty;
        }

        public Value getDipAngleUncertainty()
        {
            return dipAngleUncertainty;
        }

        public Value getMagneticUncertainty()
        {
            return magneticUncertainty;
        }

        public Boolean? isAccelerometerCorrectionUsed()
        {
            return _isAccelerometerCorrectionUsed;
        }
        public Boolean? isMagnetometerCorrectionUsed()
        {
            return _isMagnetometerCorrectionUsed;
        }
        public Boolean? isSagCorrectionUsed()
        {
            return _isSagCorrectionUsed;
        }
        public Boolean? isDrillStringMagnetismCorrectionUsed()
        {
            return _isDrillStringMagnetismCorrectionUsed;
        }

        public Value getGravitationFieldReference()
        {
            return gravitationFieldReference;
        }

        public Value getMagneticFieldReference()
        {
            return magneticFieldReference;
        }

        public Value getMagneticDipAngleReference()
        {
            return magneticDipAngleReference;
        }

        public String getMagneticModel()
        {
            return magneticModel;
        }

        public String getMagneticModelValidInterval()
        {
            return magneticModelValidInterval;
        }

        public String getGravitationalModel()
        {
            return gravitationalModel;
        }

        public String getStatus()
        {
            return status;
        }

        public WitsmlLocation getLocation()
        {
            return location;
        }

        public WitsmlCommonData getCommonData()
        {
            return commonData;
        }

        
        public int CompareTo(WitsmlTrajectoryStation station)
        {
            if (md != null && station.md != null)
                return md.CompareTo(station.md);
            else if (time.HasValue  && station.time .HasValue )
                return  time.Value .CompareTo(station.time.Value );

            // Default: Put one of them in front
            return 1;
        }

        
        public override  String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }
    }
}