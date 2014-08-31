using System;

namespace witsmllib
{

   /// <summary>
   /// Need to be changed to Match proper spec.
   /// </summary>
    public abstract class CommonData
    {

        protected String source; // sourceName

        protected DateTime?  timeCreated; // dTimCreation

        protected DateTime?  timeUpdated; // dTimLastChange

        protected String state; // itemState

        protected String serviceCategory;  // serviceCategory - 1.4.0 only

        protected String comments; // comments

        protected String acqusitionTimeZone; // acqusitionTimeZone - 1.4.0 only

        protected CommonData()
        {
        }

        public String getSource()
        {
            return source;
        }

        public DateTime? getTimeCreated()
        {
            return timeCreated;
        }

        public DateTime? getTimeUpdated()
        {
            return timeUpdated;
        }

        public String getState()
        {
            return state;
        }

        public String getServiceCategory()
        {
            return serviceCategory;
        }

        public String getComments()
        {
            return comments;
        }

        public String getAcqusitionTimeZone()
        {
            return acqusitionTimeZone;
        }

        /** {@inheritDoc} */
        
        public override String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }
    }
}