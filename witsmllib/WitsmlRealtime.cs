using System;
using System.Collections.Generic;

namespace witsmllib
{

    public abstract class WitsmlRealtime : WitsmlObject
    {
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
        protected List<WitsmlRealtimeChannel> channels = new List<WitsmlRealtimeChannel>();

       /// <summary>
        ///  Create a realtime object with specified parent.
       /// </summary>
       /// <param name="server"></param>
       /// <param name="id"></param>
       /// <param name="parent"></param>
       /// <param name="parentId"></param>
        protected WitsmlRealtime(WitsmlServer server, String id, WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, null, parent, parentId)
        { }

        public String getSubscriptionId()
        {
            return subscriptionId;
        }

        public DateTime? getTime()
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