using System;


namespace witsmllib
{

    public abstract class WitsmlRealtimeChannel
    {

        protected String groupId; // id - 1.3.1/1.4.0 only
        protected String mnemonic; // mnemonic
        protected String classWitsml; // classWitsml - 1.2.0 only
        protected DateTime? time; // dTim
        protected Value md; // md
        protected Value value;  // value - TODO: Make an array
        protected String dataType; // dataType
        protected Value dataDensity; // densData
        protected String dataQuality; // qualData
        protected Value formationExposureTime; // fet
        protected String description; // description - 1.2.0 only
        protected WitsmlInterval interval;  // interval - 1.2.0 only
        protected String dataSource; // source - 1.2.0 only

        protected WitsmlRealtimeChannel()
        {
            // Nothing
        }

        public String getGroupId()
        {
            return groupId;
        }

        public String getMnemonic()
        {
            return mnemonic;
        }

        public String getClassWitsml()
        {
            return classWitsml;
        }

        public DateTime? getTime()
        {
            return time;
        }

        public Value getMd()
        {
            return md;
        }

        public Value getValue()
        {
            return value;
        }

        public String getDataType()
        {
            return dataType;
        }

        public Value getDataDensity()
        {
            return dataDensity;
        }

        public String getDataQuality()
        {
            return dataQuality;
        }

        public Value getFormationExposureTime()
        {
            return formationExposureTime;
        }

        public String getDescription()
        {
            return description;
        }

        public WitsmlInterval getInterval()
        {
            return interval;
        }

        public String getDataSource()
        {
            return dataSource;
        }

        
        public override String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }
    }
}