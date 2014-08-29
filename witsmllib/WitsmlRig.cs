using System;

namespace witsmllib
{

    
    /// <summary>
    /// Rig Witsml Class
    /// </summary>
    public abstract class WitsmlRig : WitsmlObject
    {

        /** The WITSML type name */
        private static String WITSML_TYPE = "rig";

        protected String owner; // owner
        protected String rigType; // typeRig
        protected String manufacturer; // manufacturer
        protected String startYear; // yearEntService
        protected String rigClass; //classRig
        protected String approvals; // approvals
        protected String registrationLocation; // registration
        protected String phoneNumber; // telNumber
        protected String faxNumber; // faxNumber
        protected String emailAddress; // emailAddress
        protected String contactName; // nameContact
        protected Value drillDepthRating; // ratingDrillDepth
        protected Value waterDepthRating; // ratingWaterDepth
        protected Boolean? _isOffshore; // isOffshore
        protected Value drillingDatumToPermanentDatumDistance; // dtmRefToDtmPerm
        protected Value airGap; // airGap
        protected String datum; // dtmReference
        protected DateTime? operationStartTime; // dTimStartOp
        protected DateTime? operationEndTime; // dTimEndOp

        protected WitsmlRig(WitsmlServer server, String id, String name,
                            WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        public String getOwner()
        {
            return owner;
        }

        public String getRigType()
        {
            return rigType;
        }

        public String getManufacturer()
        {
            return manufacturer;
        }

        public String getStartYear()
        {
            return startYear;
        }

        public String getRigClass()
        {
            return rigClass;
        }

        public String getApprovals()
        {
            return approvals;
        }

        public String getRegistrationLocation()
        {
            return registrationLocation;
        }

        public String getPhoneNumber()
        {
            return phoneNumber;
        }

        public String getFaxNumber()
        {
            return faxNumber;
        }

        public String getEmailAddress()
        {
            return emailAddress;
        }

        public String getContactName()
        {
            return contactName;
        }

        public Value getDrillDepthRating()
        {
            return drillDepthRating;
        }

        public Value getWaterDepthRating()
        {
            return waterDepthRating;
        }

        public Boolean? isOffshore()
        {
            return _isOffshore;
        }

        public Value getDrillingDatumToPermanentDatumDistance()
        {
            return drillingDatumToPermanentDatumDistance;
        }

        public Value getAirGap()
        {
            return airGap;
        }

        public String getDatum()
        {
            return datum;
        }

        public DateTime? getOperationStartTime()
        {
            return operationStartTime;
        }

        public DateTime? getOperationEndTime()
        {
            return operationEndTime;
        }
    }
}