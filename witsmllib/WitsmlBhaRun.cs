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

    /**
     * Java representation of a WITSML "bhaRun".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlBhaRun : WitsmlObject
    {
        /** The WITSML type name */
        private static String WITSML_TYPE = "bhaRun";

        protected DateTime? startTime; // dTimStart
        protected DateTime? endTime; // dTimStop
        protected DateTime? drillingStartTime; // dTimStartDrilling
        protected DateTime? drillingEndTime; // dTimStopDrilling
        protected Value plannedDls; // planDogleg
        protected Value actualDls; // actDogleg
        protected Value maxActualDls; // actDoglegMx
        protected String status; // statusBha
        protected String bitRunNumber; // numBitRun
        protected String stringRunNumber; // numStringRun
        protected String reason; // reasonTrip
        protected String objective; // objectiveBha
        // protected WitsmlDrillingParameters drillingParameters; // drillingParams

        /**
         * Create a well object with specified ID.
         *
         * @param id  ID of this well.
         */
        protected WitsmlBhaRun(WitsmlServer server, String id, String name,
                               WitsmlObject parent, String parentId)
            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public DateTime? getStartTime()
        {
            return startTime;
        }

        public DateTime? getEndTime()
        {
            return endTime;
        }

        public DateTime? getDrillingStartTime()
        {
            return drillingStartTime;
        }

        public DateTime? getDrillingEndTime()
        {
            return drillingEndTime;
        }

        public Value getPlannedDls()
        {
            return plannedDls;
        }

        public Value getActualDls()
        {
            return actualDls;
        }

        public Value getMaxActualDls()
        {
            return maxActualDls;
        }

        public String getStatus()
        {
            return status;
        }

        public String getBitRunNumber()
        {
            return bitRunNumber;
        }

        public String getStringRunNumber()
        {
            return stringRunNumber;
        }

        public String getReason()
        {
            return reason;
        }

        public String getObjective()
        {
            return objective;
        }
    }
}