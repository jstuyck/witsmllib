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
using System.Collections.Generic;
namespace witsmllib
{

    //import java.util.Collections;
    //import java.util.ArrayList;
    //import java.util.List;
    //import java.util.Date;

    /**
     * Java representation of a WITSML "realtime".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public abstract class WitsmlRealtime : WitsmlObject
    {

        /** The WITSML type name */
        private static String WITSML_TYPE = "realtime";

        protected String subscriptionId;
        protected DateTime? time;
        protected Value md;
        protected Int32? sequenceNumber;
        protected String activityCode;
        protected String activitySubcode;
        protected String wellName;
        protected String wellboreName;
        protected String serviceCompany;
        protected Int32? runNumber;
        protected DateTime? creationDate;
        protected String description;
        protected  List<WitsmlRealtimeChannel> channels = new List<WitsmlRealtimeChannel>();

        /**
         * Create a realtime object with specified parent.
         *
         * @param wellbore  Parent wellbore of this realtime object.
         */
        protected WitsmlRealtime(WitsmlServer server, String id, WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, null, parent, parentId)
        {}

        public String getSubscriptionId()
        {
            return subscriptionId;
        }

        public DateTime?  getTime()
        {
            return time; // time != null ? new Date(time.getTime()) : null;
        }

        public Value getMd()
        {
            return md;
        }

        public Int32? getSequenceNumber()
        {
            return sequenceNumber;
        }

        public String getActivityCode()
        {
            return activityCode;
        }

        public String getActivitySubcode()
        {
            return activitySubcode;
        }

        public String getServiceCompany()
        {
            return serviceCompany;
        }

        public Int32? getRunNumber()
        {
            return runNumber;
        }

        public DateTime? getCreationDate()
        {
            return creationDate;
        }

        public String getDescription()
        {
            return description;
        }

        public List<WitsmlRealtimeChannel> getChannels()
        {
            return channels; // Collections.unmodifiableList(channels);
        }
    }
}