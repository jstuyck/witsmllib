using System;

namespace witsmllib
{

   /// <summary>
   /// Need to be changed to Match proper spec.
   /// </summary>
    public abstract class CommonData
    {

        protected String source;                // sourceName
        protected DateTime? dTimCreation;       // When the data was created at the persistent data store. This is an API server parameter releted to the "Special Handling of Change Information" within a server. See the relevant API specification for the behavior related to this element.
        protected DateTime? dTimLastChange;     // Last change of any element of the data at the persistent data store. This is an API server parameter releted to the "Special Handling of Change Information" within a server. See the relevant API specification for the behavior related to this element.
        protected State state;                 // itemState
        protected String serviceCategory;       // serviceCategory - 1.4.0 only
        protected String comments;              // comments
        protected String acqusitionTimeZone;    // acqusitionTimeZone - 1.4.0 only

        protected CommonData()
        {
        }

        public String getSource()
        {
            return source;
        }

        public void setSource(string source)
        {
            this.source = source;
        }

       
        public DateTime? getTimeCreated()
        {
            return dTimCreation;
        }

        public void setTimeCreated(DateTime timeCreated)
        {
            dTimCreation = timeCreated;
        }


        public DateTime? getTimeUpdated()
        {
            return dTimLastChange;
        }

        public void setTimeUpdated(DateTime timeUpdated)
        {
            dTimLastChange = timeUpdated;
        }

        public State getState()
        {
            return state;
        }

        public void setState(State state)
        {
            this.state = state;
        }

        public String getServiceCategory()
        {
            return serviceCategory;
        }

        public void setServiceCategory(string serviceCategory)
        {
            this.serviceCategory = serviceCategory;
        }

        public void setComments(String comments)
        {
            this.comments = comments;
        }

        public String getComments()
        {
            return comments;
        }

        /// <summary>
        /// get the AcquisitionTimezone
        /// Witsml 1.4 only
        /// </summary>
        /// <returns></returns>
        public String getAcqusitionTimeZone()
        {
            return acqusitionTimeZone;
        }

        /** {@inheritDoc} */
        
        public override String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }

        public enum State
        {
            ACTUAL, // Actual data measured or entered at the well site.
            MODEL,  // Model data used for "what if" calculations.
            PLAN,   // A planned object.
            UNKNOWN // The value is not known.
        }
    }
}