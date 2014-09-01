using System;

namespace witsmllib
{

    
   
    public abstract class WitsmlTarget : WitsmlObject
    {

        
        private  static String WITSML_TYPE = "target";

        protected String parentTargetId; // uidTargetParent
        protected Value north; // dispNsCenter
        protected Value east; // dispEwCenter
        protected Value tvd; // tvd
        protected Value dispNsOffset;           //North-south offset of target intercept point from shape center.
        protected Value dispEwOffset;           //East-west offset of target intercept point from shape center.
        protected Value thickAbove;             //Height of target above center point.
        protected Value thickBelow;             //Depth of target below center point.
        protected Value dip;                    //Angle of dip with respect to horizontal.




        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="parentId"></param>
        protected WitsmlTarget(WitsmlServer server, String id, String name,
                               WitsmlObject parent, String parentId)
            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public String getParentTargetId()
        {
            return parentTargetId;
        }

        public Value getNorth()
        {
            return north;
        }

        public Value getEast()
        {
            return east;
        }

        public Value getTvd()
        {
            return tvd;
        }



        /// <summary>
        /// Return the north south offset of this target. 
        /// </summary>
        /// <returns>The north south offset of this target. May be null if absent or unknown.</returns>
        public Value getNorthSouthOffset()
        {
            return dispNsOffset;
        }

        /// <summary>
        /// Return the east west offset of this target.
        /// </summary>
        /// <returns>The east west offset of this target. May be null if absent or unknown.</returns>
        public Value getEastWestOffset()
        {
            return dispEwOffset;
        }

        /// <summary>
        /// Return the dip of this target. 
        /// </summary>
        /// <returns>The dip of this target. May be null if absent or unknown.</returns>
        public Value getDip()
        {
            return dip;
        }


    }
}