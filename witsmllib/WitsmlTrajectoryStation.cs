using System;

namespace witsmllib
{
    /// <summary>
    /// WitsmlTrajectoryStation Class
    /// </summary>
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
        protected Location location;
        protected CommonData commonData;

        /// <summary>
        /// Create a trajectory station with given station number.
        /// </summary>
        /// <param name="stationNo">tation number of this trajectory station.</param>
        protected WitsmlTrajectoryStation(int stationNo)
        {
            this.stationNo = stationNo;
        }

        /// <summary>
        /// Get ID of this station.
        /// </summary>
        /// <returns>ID of this station.</returns>
        public String getId()
        {
            return id;
        }

        /// <summary>
        /// Return number in the sequence of this station.
        /// </summary>
        /// <returns>Number in the sequence of this station. 0-based.</returns>
        public int getStationNo()
        {
            return stationNo;
        }

        /// <summary>
        /// Get time of this station.
        /// </summary>
        /// <returns>Time of this station.</returns>
        public DateTime? getTime()
        {   

            return time;
        }

        /// <summary>
        /// Get type of this station.
        /// </summary>
        /// <returns>Type of this station.</returns>
        public String getType()
        {
            return type;
        }

        public String getSurveyToolType()
        {
            return surveyToolType;
        }

        /// <summary>
        ///  Get MD of this station.
        /// </summary>
        /// <returns>MD of this station.</returns>
        public Value getMd()
        {
            return md;
        }

        /// <summary>
        /// Get TVD of this station.
        /// </summary>
        /// <returns>TVD of this station.</returns>
        public Value getTvd()
        {
            return tvd;
        }

        /// <summary>
        /// Get inclination of this station.
        /// </summary>
        /// <returns>Inclination of this station.</returns>
        public Value getInclination()
        {
            return inclination;
        }

        /// <summary>
        /// Get azimuth of this station.
        /// </summary>
        /// <returns>Azimuth of this station.</returns>
        public Value getAzimuth()
        {
            return azimuth;
        }

        /// <summary>
        /// Get angle of magnetic toolface of this station.
        /// </summary>
        /// <returns>Angle of magnetic toolface of this station.</returns>
        public Value getToolfaceMagneticAngle()
        {
            return toolfaceMagneticAngle;
        }

        
        /// <summary>
        /// Get angle of gravity toolface of this station.
        /// </summary>
        /// <returns>Angle of gravity toolface of this station.</returns>
        public Value getToolfaceGravityAngle()
        {
            return toolfaceGravityAngle;
        }

        /// <summary>
        /// Get north coordinate of this station.
        /// </summary>
        /// <returns>North position of this station.</returns>
        public Value getNorth()
        {
            return north;
        }

        /// <summary>
        /// Get East coordinate of this station.
        /// </summary>
        /// <returns></returns>
        public Value getEast()
        {
            return east;
        }

        /// <summary>
        /// Get Vertical Section distance of this station.
        /// </summary>
        /// <returns>Vertical Section distance of this station.</returns>
        public Value getVerticalSectionDistance()
        {
            return verticalSectionDistance;
        }

        /// <summary>
        /// Get the dogleg severity of this station.
        /// </summary>
        /// <returns>Dogleg severity of this station.</returns>
        public Value getDls()
        {
            return dls;
        }

        /// <summary>
        /// Get turn rate of this station.
        /// </summary>
        /// <returns>Turn rate of this station.</returns>
        public Value getTurnRate()
        {
            return turnRate;
        }

        /// <summary>
        /// Get build rate of this station.
        /// </summary>
        /// <returns>Build rate of this station</returns>
        public Value getBuildRate()
        {
            return buildRate;
        }

        /// <summary>
        /// Get measured depth of this station.
        /// </summary>
        /// <returns>Measured depth of this station.</returns>
        public Value getDMd()
        {
            return dMd;
        }

        /// <summary>
        /// Get true vertical depth of this station.
        /// </summary>
        /// <returns>True vertical depth of this station.</returns>
        public Value getDTvd()
        {
            return dTvd;
        }

        /// <summary>
        /// Get error model of this station.
        /// </summary>
        /// <returns>Error model of this station.</returns>
        public String getErrorModel()
        {
            return errorModel;
        }

        /// <summary>
        /// Get gravity uncertainty of this station.
        /// </summary>
        /// <returns>Gravity uncertainty of this station.</returns>
        public Value getGravityUncertainty()
        {
            return gravityUncertainty;
        }

        /// <summary>
        /// Get dip angle uncertainty of this station.
        /// </summary>
        /// <returns>Dip angle uncertainty of this station.</returns>
        public Value getDipAngleUncertainty()
        {
            return dipAngleUncertainty;
        }

        /// <summary>
        /// Get magnetic uncertainty of this station.
        /// </summary>
        /// <returns>Magnetic uncertainty of this station.</returns>
        public Value getMagneticUncertainty()
        {
            return magneticUncertainty;
        }

        /// <summary>
        /// Get if accelerometer correction is used for this station.
        /// </summary>
        /// <returns>If accelerometer correction is used for this station.</returns>
        public Boolean? isAccelerometerCorrectionUsed()
        {
            return _isAccelerometerCorrectionUsed;
        }

        /// <summary>
        /// Get if magnetometer correction is used for this station.
        /// </summary>
        /// <returns>If magnetometer correction is used for this station.</returns>
        public Boolean? isMagnetometerCorrectionUsed()
        {
            return _isMagnetometerCorrectionUsed;
        }

        /// <summary>
        /// Get if sag correction is used for this station.
        /// </summary>
        /// <returns>If sag correction is used for this station.</returns>
        public Boolean? isSagCorrectionUsed()
        {
            return _isSagCorrectionUsed;
        }

        /// <summary>
        /// Get if drillString magnetism correction is used for this station.
        /// </summary>
        /// <returns>If drillString magnetism correction is used for this station.</returns>
        public Boolean? isDrillStringMagnetismCorrectionUsed()
        {
            return _isDrillStringMagnetismCorrectionUsed;
        }

        /// <summary>
        /// Get gravitation field reference of this station.
        /// </summary>
        /// <returns>Gravitation field reference of this station.</returns>
        public Value getGravitationFieldReference()
        {
            return gravitationFieldReference;
        }

        /// <summary>
        /// Get magnetic field reference of this station.
        /// </summary>
        /// <returns>Magnetic field reference of this station.</returns>
        public Value getMagneticFieldReference()
        {
            return magneticFieldReference;
        }

        /// <summary>
        /// Get magnetic dip angle reference of this station.
        /// </summary>
        /// <returns>Magnetic dip angle reference of this station.</returns>
        public Value getMagneticDipAngleReference()
        {
            return magneticDipAngleReference;
        }

        /// <summary>
        /// Get magnetic model of this station.
        /// </summary>
        /// <returns>Magnetic model of this station.</returns>
        public String getMagneticModel()
        {
            return magneticModel;
        }

        /// <summary>
        /// Get magnetic model valid interval of this station.
        /// </summary>
        /// <returns>Magnetic model valid interval of this station.</returns>
        public String getMagneticModelValidInterval()
        {
            return magneticModelValidInterval;
        }

        /// <summary>
        /// Get gravitational model of this station.
        /// </summary>
        /// <returns>Gravitational model of this station.</returns>
        public String getGravitationalModel()
        {
            return gravitationalModel;
        }

        /// <summary>
        /// Get status of this station.
        /// </summary>
        /// <returns>Status of this station.</returns>
        public String getStatus()
        {
            return status;
        }

        /// <summary>
        /// Get location of this station.
        /// </summary>
        /// <returns>Location of this station.</returns>
        public Location getLocation()
        {
            return location;
        }

        /// <summary>
        /// Get common data of this station.
        /// </summary>
        /// <returns>Common data of this station.</returns>
        public CommonData getCommonData()
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