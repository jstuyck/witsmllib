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

    //import java.util.Date;
    //import java.util.List;

    /**
     * Java representation of a WITSML "wellbore".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlWellbore : WitsmlObject
    {

        /** The WITSML type name. */
        private static String WITSML_TYPE = "wellbore";

        protected String number;  // number
        protected String apiSuffix; // suffixAPI
        protected String wellboreNumber; // numGovt
        protected String status; // statusWellbore
        protected Boolean? isActive; // isActive (1.4)
        protected String purpose; // purposeWellbore
        protected String type; // typeWellbore
        protected String shape; // shape
        protected DateTime? kickoffTime; // dTimKickoff
        protected Boolean? _isTotalDepthReached; // acheivedTD
        protected Value mdCurrent; // mdCurrent
        protected Value tvdCurrent; // tvdCurrent
        protected Value mdBitCurrent; // mdBitCurrent (1.4)
        protected Value tvdBitCurrent; // tvdBitCurrent (1.4)
        protected Value mdKickoff; // mdKickoff
        protected Value tvdKickoff; // tvdKickoff
        protected Value mdPlanned; // mdPlanned
        protected Value tvdPlanned; // tvdPlanned
        protected Value mdSubSeaPlanned; // mdSubSeaPlanned
        protected Value tvdSubSeaPlanned; // tvdSubSeaPlanned
        protected Int32? nTargetDays; // dayTarget

        /**
         * Create a wellbore object with speified ID and parent.
         *
         * @param id    ID of this well.
         * @param well  Parent well of this wellbore. Non-null.
         */
        protected WitsmlWellbore(WitsmlServer server,
                                 String id, String name,
                                 WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        public String getNumber()
        {
            return number;
        }

        public String getApiSuffix()
        {
            return apiSuffix;
        }

        public String getWellboreNumber()
        {
            return wellboreNumber;
        }

        /**
         * Get status of this wellbore.
         *
         * @return  Status of this wellbore.
         */
        public String getStatus()
        {
            return status;
        }

        /**
         * Get purpose of this wellbore.
         *
         * @return  Purpose of this wellbore.
         */
        public String getPurpose()
        {
            return purpose;
        }

        /**
         * Get type of this wellbore.
         *
         * @return  Type of this wellbore.
         */
        public String getType()
        {
            return type;
        }

        /**
         * Get shape of this wellbore.
         *
         * @return  Shape of this wellbore.
         */
        public String getShape()
        {
            return shape;
        }

        public DateTime? getKickoffTime()
        {
            return kickoffTime;
        }

        public Boolean? isTotalDepthReached()
        {
            return _isTotalDepthReached;
        }

        public Value getMdCurrent()
        {
            return mdCurrent;
        }

        public Value getTvdCurrent()
        {
            return tvdCurrent;
        }

        public Value getMdBitCurrent()
        {
            return mdBitCurrent;
        }

        public Value getTvdBitCurrent()
        {
            return tvdBitCurrent;
        }

        public Value getMdKickoff()
        {
            return mdKickoff;
        }

        public Value getTvdKickoff()
        {
            return tvdKickoff;
        }

        public Value getMdPlanned()
        {
            return mdPlanned;
        }

        public Value getTvdPlanned()
        {
            return tvdPlanned;
        }

        public Value getMdSubSeaPlanned()
        {
            return mdSubSeaPlanned;
        }

        public Value getTvdSubSeaPlanned()
        {
            return tvdSubSeaPlanned;
        }

        public Int32? getNTargetDays()
        {
            return nTargetDays;
        }
    }

}