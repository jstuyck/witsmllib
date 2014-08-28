using System;

namespace witsmllib
{
    public abstract class WitsmlWellbore : WitsmlObject
    {
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


        protected WitsmlWellbore(WitsmlServer server,
                                 String id, String name,
                                 WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

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

        /// <summary>
        /// Get status of this wellbore.
        /// </summary>
        /// <returns>Status of this wellbore.</returns>
        public String getStatus()
        {
            return status;
        }

        /// <summary>
        /// Get purpose of this wellbore.
        /// </summary>
        /// <returns>Purpose of this wellbore.</returns>
        public String getPurpose()
        {
            return purpose;
        }

        /// <summary>
        /// Get type of this wellbore.
        /// </summary>
        /// <returns>Type of this wellbore.</returns>
        public String getType()
        {
            return type;
        }

        /// <summary>
        /// Get shape of this wellbore.
        /// </summary>
        /// <returns>Shape of this wellbore.</returns>
        public String getShape()
        {
            return shape;
        }

        /// <summary>
        /// Get kick off Time of this wellbore.
        /// </summary>
        /// <returns>Kick off Time of this wellbore.</returns>
        public DateTime? getKickoffTime()
        {
            return kickoffTime;
        }

        public Boolean? isTotalDepthReached()
        {
            return _isTotalDepthReached;
        }

        /// <summary>
        /// Get current measured depth of this wellbore.
        /// </summary>
        /// <returns>Current measured depth of this wellbore.</returns>
        public Value getMdCurrent()
        {
            return mdCurrent;
        }

        /// <summary>
        /// Get the true vertical depth of this wellbore.
        /// </summary>
        /// <returns>True vertical depth of this wellbore.</returns>
        public Value getTvdCurrent()
        {
            return tvdCurrent;
        }

        /// <summary>
        /// Get the current bit depth of this wellbore.
        /// </summary>
        /// <returns>Current bit depth of this wellbore.</returns>
        public Value getMdBitCurrent()
        {
            return mdBitCurrent;
        }

        /// <summary>
        /// Get the true vertical depth of the bit of this wellbore.
        /// </summary>
        /// <returns>True vertical depth of the bit of this wellbore.</returns>
        public Value getTvdBitCurrent()
        {
            return tvdBitCurrent;
        }

        /// <summary>
        /// Get the measured depth of the kick off of this wellbore.
        /// </summary>
        /// <returns>Measured depth of the kick off of this wellbore.</returns>
        public Value getMdKickoff()
        {
            return mdKickoff;
        }

        /// <summary>
        /// Get the true vertical depth of the kick off of this wellbore.
        /// </summary>
        /// <returns>True vertical depth of the kick off of this wellbore.</returns>
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