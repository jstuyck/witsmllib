using System;

namespace witsmllib
{
    public class WitsmlWell : WitsmlObject
    {

        private static String WITSML_TYPE = "well";

        protected String legalName; // nameLegal
        protected String licenseNumber; // numLicense
        protected String wellNumber; // numGovt
        protected DateTime? licenseIssueTime; // dTimLicense
        protected String field; // field
        protected String country; // country
        protected String state; // state
        protected String county; // county
        protected String region; // region
        protected String district; // district
        protected String block; // block
        protected String timeZone; // timeZone
        protected String @operator; // operator
        protected String operatorDivision; // operatorDiv
        protected Value operatorInterestShare; // pcInterest
        protected String apiNumber; // numAPI
        protected String status; // statusWell
        protected String purpose; // purposeWell
        protected String fluidType; // fluidWell (1.3+)
        protected String flowDirection; // directionWell (1.3+)
        protected DateTime? spudTime; // dTimSpud
        protected DateTime? pluggedTime; // dTimPa
        protected Value wellHeadElevation; // dtmPermToWellhead (1.2) wellheadElevation(1.3+)
        protected Value groundElevation; // groundElevation
        protected Value waterDepth; // waterDepth
        protected WitsmlLocation location; //location (1.2) wellLocation (1.3+)

        protected String datum;

        /**
         * Create a well object with specified ID.
         *
         * @param id  ID of this well.
         */
        protected WitsmlWell(WitsmlServer server, String id, String name,
                             WitsmlObject parent)

            : base(server, WITSML_TYPE, id, name, parent, null)
        { }

        public String getLegalName()
        {
            return legalName;
        }

        public String getLicenseNumber()
        {
            return licenseNumber;
        }

        public DateTime? getLicenseIssueTime()
        {
            return licenseIssueTime;
        }

        /// <summary>
        /// Get field for this well.
        /// </summary>
        /// <returns>Field for this well</returns>
        public String getField()
        {
            return field;
        }

        /// <summary>
        /// Get Country for this well.
        /// </summary>
        /// <returns>country for this well.</returns>
        public String getCountry()
        {
            return country;
        }

        /// <summary>
        ///  Get government number of this well.
        /// </summary>
        /// <returns> Government number of this well.</returns>
        public String getWellNumber()
        {
            return wellNumber;
        }

        /// <summary>
        /// Get state of this well.
        /// </summary>
        /// <returns>State of this well.</returns>
        public String getState()
        {
            return state;
        }

        /// <summary>
        ///  Get county of this well.
        /// </summary>
        /// <returns>County of this well.</returns>
        public String getCounty()
        {
            return county;
        }

        /// <summary>
        /// Get region of this well.
        /// </summary>
        /// <returns>Region of this well.</returns>
        public String getRegion()
        {
            return region;
        }

        /// <summary>
        /// Get district of this well.
        /// </summary>
        /// <returns>District of this well.</returns>
        public String getDistrict()
        {
            return district;
        }

        /// <summary>
        ///  Get block of this well.
        /// </summary>
        /// <returns> Block of this well.</returns>
        public String getBlock()
        {
            return block;
        }

        /// <summary>
        /// Return time zone identifier of this well.
        /// </summary>
        /// <returns>Time zone identifier of this well. May be null.</returns>
        public String getTimeZone()
        {
            return timeZone;
        }

        /// <summary>
        /// Get operator of this well.
        /// </summary>
        /// <returns>Operator of this well.</returns>
        public String getOperator()
        {
            return @operator;
        }

        /// <summary>
        /// Get Operator Division of this well.
        /// </summary>
        /// <returns>Operator Division of this well.</returns>
        public String getOperatorDivision()
        {
            return operatorDivision;
        }

        /// <summary>
        /// Get Operator Interest Share.
        /// </summary>
        /// <returns>Operator Interest Share.</returns>
        public Value getOperatorInterestShare()
        {
            return operatorInterestShare;
        }

        /// <summary>
        /// Get API number of this well.
        /// </summary>
        /// <returns>API number of this well.</returns>
        public String getApiNumber()
        {
            return apiNumber;
        }

        /// <summary>
        /// Get Status of this well.
        /// </summary>
        /// <returns>Status of this well.</returns>
        public String getStatus()
        {
            return status;
        }

        /// <summary>
        /// Get purpose of this well.
        /// </summary>
        /// <returns>Purspose of this well.</returns>
        public String getPurpose()
        {
            return purpose;
        }

        /// <summary>
        /// Get Fluid Type of this well.
        /// </summary>
        /// <returns>Fluid Type of this well.</returns>
        public String getFluidType()
        {
            return fluidType;
        }

        /// <summary>
        /// Get flow direction of this well.
        /// </summary>
        /// <returns>Flow direction of this well. May be null</returns>
        public String getFlowDirection()
        {
            return flowDirection;
        }

        /// <summary>
        /// Get spud time of this well.
        /// </summary>
        /// <returns>Spud time of this well.</returns>
        public DateTime? getSpudTime()
        {
            return spudTime; // spudTime != null ? new Date(spudTime.getTime()) : null;
        }

        /// <summary>
        /// Get plugged time of this well.
        /// </summary>
        /// <returns>Plugged time of this well.</returns>
        public DateTime? getPluggedTime()
        {
            return pluggedTime; // pluggedTime != null ? new Date(pluggedTime.getTime()) : null;
        }

        /// <summary>
        /// Get well head elevation of this well.
        /// </summary>
        /// <returns>Well head elevation of this well.</returns>
        public Value getWellHeadElevation()
        {
            return wellHeadElevation;
        }

        /// <summary>
        /// Get datum of this well.
        /// </summary>
        /// <returns>Datum of this well.</returns>
        public String getDatum()
        {
            return datum;
        }

        /// <summary>
        /// Get ground elevation of this well.
        /// </summary>
        /// <returns>Ground elevation of this well.</returns>
        public Value getGroundElevation()
        {
            return groundElevation;
        }

        /// <summary>
        /// Get water depth of this well.
        /// </summary>
        /// <returns>Water depth of this well.</returns>
        public Value getWaterDepth()
        {
            return waterDepth;
        }

        /// <summary>
        /// Get the location of this well.
        /// </summary>
        /// <returns>Location of this well.</returns>
        public WitsmlLocation getLocation()
        {
            return location;
        }
    }
}