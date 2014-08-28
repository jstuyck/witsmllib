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
namespace witsmllib
{

    //import java.util.List;
    //import java.util.Date;

    /**
     * Java representation of a WITSML "well".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public class WitsmlWell : WitsmlObject
    {
        /** The WITSML type name */
        private static String WITSML_TYPE = "well";

        protected String legalName; // nameLegal
        protected String licenseNumber; // numLicense
        protected String wellNumber; // numGovt
        protected DateTime?  licenseIssueTime; // dTimLicense
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
        protected DateTime?  spudTime; // dTimSpud
        protected DateTime?  pluggedTime; // dTimPa
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
        
            :base(server, WITSML_TYPE, id, name, parent, null)
        {}

        public String getLegalName()
        {
            return legalName;
        }

        public String getLicenseNumber()
        {
            return licenseNumber;
        }

        public DateTime?  getLicenseIssueTime()
        {
            return licenseIssueTime;
        }

        /**
         * Get field of this well.
         *
         * @return  Field of this well.
         */
        public String getField()
        {
            return field;
        }

        /**
         * Get country of this well.
         *
         * @return  Country of this well.
         */
        public String getCountry()
        {
            return country;
        }

        /**
         * Get government number of this well.
         *
         * @return  Government number of this well.
         */
        public String getWellNumber()
        {
            return wellNumber;
        }

        /**
         * Get state of this well.
         *
         * @return  State of this well.
         */
        public String getState()
        {
            return state;
        }

        /**
         * Get county of this well.
         *
         * @return  County of this well.
         */
        public String getCounty()
        {
            return county;
        }

        /**
         * Get region of this well.
         *
         * @return  Region of this well. May be null.
         */
        public String getRegion()
        {
            return region;
        }

        /**
         * Get district of this well.
         *
         * @return  District of this well.
         */
        public String getDistrict()
        {
            return district;
        }

        /**
         * Get block of this well.
         *
         * @return  Block of this well.
         */
        public String getBlock()
        {
            return block;
        }

        /**
         * Return time zone identifier of this well.
         *
         * @return Time zone identifier of this well. May be null.
         */
        public String getTimeZone()
        {
            return timeZone;
        }

        /**
         * Get operator of this well.
         *
         * @return  Operator of this well.
         */
        public String getOperator()
        {
            return @operator;
        }

        public String getOperatorDivision()
        {
            return operatorDivision;
        }

        public Value getOperatorInterestShare()
        {
            return operatorInterestShare;
        }

        /**
         * Get API number of this well.
         *
         * @return  API number of this well.
         */
        public String getApiNumber()
        {
            return apiNumber;
        }

        /**
         * Get status of this well.
         *
         * @return  Status of this well.
         */
        public String getStatus()
        {
            return status;
        }

        /**
         * Get purpose of this well.
         *
         * @return  Purpose of this well.
         */
        public String getPurpose()
        {
            return purpose;
        }

        /**
         * Get fluid type of this well.
         *
         * @return  Fluid type of this well. May be null.
         */
        public String getFluidType()
        {
            return fluidType;
        }

        /**
         * Get flow direction of this well.
         *
         * @return  Flow direction of this well. May be null.
         */
        public String getFlowDirection()
        {
            return flowDirection;
        }

        /**
         * Get spud time of this well.
         *
         * @return  Spud time of this well.
         */
        public DateTime?  getSpudTime()
        {
            return spudTime; // spudTime != null ? new Date(spudTime.getTime()) : null;
        }

        /**
         * Get plugged time of this well.
         *
         * @return  Plugged time of this well.
         */
        public DateTime?  getPluggedTime()
        {
            return pluggedTime; // pluggedTime != null ? new Date(pluggedTime.getTime()) : null;
        }

        /**
         * Get well head elevation  of this well.
         *
         * @return  Well head elevation of this well.
         */
        public Value getWellHeadElevation()
        {
            return wellHeadElevation;
        }

        /**
         * Get datum of this well.
         *
         * @return  Datum of this well.
         */
        public String getDatum()
        {
            return datum;
        }

        /**
         * Get ground elevation of this well.
         *
         * @return  Ground elevation of this well.
         */
        public Value getGroundElevation()
        {
            return groundElevation;
        }

        /**
         * Get water depth of this well.
         *
         * @return  Water depth of this well. May be null.
         */
        public Value getWaterDepth()
        {
            return waterDepth;
        }

        /**
         * Return location of this well.
         *
         * @return  Location of this well. May be null.
         */
        public WitsmlLocation getLocation()
        {
            return location;
        }
    }
}