using System;
using System.Collections.Generic;

namespace witsmllib
{

    
   
    public abstract class WitsmlTarget : WitsmlObject
    {

        
        private  static String WITSML_TYPE = "target";

        protected String parentTargetId;        //uidTargetParent A pointer to the parent target. This represents a relationship between a drillers and geological target.
        protected Value dispNsCenter;           //Northing of target center point in map coordinates.
        protected Value dispEwCenter;           //Easting of target center point in map coordinates.
        protected Value tvd;                    //Vertical depth of the measurements.
        protected Value dispNsOffset;           //North-south offset of target intercept point from shape center.
        protected Value dispEwOffset;           //East-west offset of target intercept point from shape center.
        protected Value thickAbove;             //Height of target above center point.
        protected Value thickBelow;             //Depth of target below center point.
        protected Value dip;                    //Angle of dip with respect to horizontal.
        protected Value strike;                 //Direction of dip with respect to north azimuth reference.
        protected Value rotation;               //Direction of target geometry with respect to north azimuth reference.
        protected Value lenMajorAxis;           //Distance from center to perimeter in rotation direction. This may be ignored depending on the value of typeTargetScope.
        protected Value widMinorAxis;           //Distance from center to perimeter at 90 deg to rotation direction. This may be ignored depending on the value of typeTargetScope.
        protected Scope typeTargetScope;        //The type of scope of the drilling target. ENUM
        protected Value dispNsSectOrig;         //Origin north-south used as starting point for sections, mandatory parameter when sections are used..
        protected Value dispEwSectOrig;         //Origin east-west used as starting point for sections, mandatory parameter when sections are used.
        protected string aziRef;                //Specifies the definition of north. ENUM
        protected Category catTarg;             //Geological or drillers target. ENUM
        protected List<Location> location;            //The 2D coordinates of the item at the start of the section. The location object is mandatory for the first section starting point.


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

        /// <summary>
        /// Return the azimuth reference of this target. 
        /// Specifies the definition of north. 
        /// </summary>
        /// <returns>The azimuth reference of this target. May be null if absent or unknown.</returns>
        public string getAzimuthReference()
        {
            return aziRef;
        }

        /// <summary>
        /// Return the category of this target. 
        /// Geological or drillers target. 
        /// </summary>
        /// <returns>The category of this target. May be null if absent or unknown.</returns>
        public Category getCategory()
        {
            return catTarg;
        }

        /// <summary>
        /// Return the northing of this target. 
        /// Northing of target center point in map coordinates 
        /// </summary>
        /// <returns>The northing of this target. May be null if absent or unknown.</returns>
        public Value getNorthing()
        {
            return dispNsCenter;
        }

        /// <summary>
        /// Return the easting of this target. 
        /// Easting of target center point in map coordinates 
        /// </summary>
        /// <returns>The easting of this target. May be null if absent or unknown.</returns>
        public Value getEasting()
        {
            return dispEwCenter;
        }

        /// <summary>
        /// Return the TVD of this target.
        /// True vertical depth of the measurements 
        /// </summary>
        /// <returns>The TVD of this target. May be null if absent or unknown.</returns>
        public Value getTvd()
        {
            return tvd;
        }

        /// <summary>
        /// Return the north south offset of this target. 
        /// North-south offset of target intercept point from shape center. 
        /// </summary>
        /// <returns>The north south offset of this target. May be null if absent or unknown.</returns>
        public Value getNorthSouthOffset()
        {
            return dispNsOffset;
        }

        /// <summary>
        /// Return the east west offset of this target.
        /// East-west offset of target intercept point from shape center. 
        /// </summary>
        /// <returns>The east west offset of this target. May be null if absent or unknown.</returns>
        public Value getEastWestOffset()
        {
            return dispEwOffset;
        }

        /// <summary>
        /// Return the north south section origin of this target.
        /// Origin north-south used as starting point for sections, mandatory parameter when sections are used. 
        /// </summary>
        /// <returns>The north south section origin of this target. May be null if absent or unknown.</returns>
        public Value getNorthSouthSectionOrigin()
        {
            return dispNsSectOrig;
        }

        /// <summary>
        /// Return the east west section origin of this target. 
        /// Origin east-west used as starting point for sections, mandatory parameter when sections are used. 
        /// </summary>
        /// <returns>The east west section origin of this target. May be null if absent or unknown.</returns>
        public Value getEastWestSectionOrigin()
        {
            return dispEwSectOrig;
        }

        /// <summary>
        /// Return the major axis length of this target.
        /// Distance from center to perimeter in rotation direction. This may be ignored depending on the value of typeTargetScope. 
        /// </summary>
        /// <returns>The major axis length of this target. May be null if absent or unknown.</returns>
        public Value getMajorAxisLength()
        {
            return lenMajorAxis;
        }

        /// <summary>
        /// Return the minor axis width of this target. 
        /// Distance from center to perimeter at 90 deg to rotation direction. This may be ignored depending on the value of typeTargetScope. 
        /// </summary>
        /// <returns>The minor axis width of this target. May be null if absent or unknown.</returns>
        public Value getMinorAxisWidth()
        {
            return widMinorAxis;
        }

        /// <summary>
        /// Return the rotation of this target. 
        /// Direction of target geometry with respect to north azimuth reference. 
        /// </summary>
        /// <returns>The rotation of this target. May be null if absent or unknown.</returns>
        public Value getRotation()
        {
            return rotation;
        }

        /// <summary>
        /// Return the dip of this target. 
        /// </summary>
        /// <returns>The dip of this target. May be null if absent or unknown.</returns>
        public Value getDip()
        {
            return dip;
        }

        /// <summary>
        /// Return the scope of this target. 
        /// The type of scope of the drilling target. 
        /// </summary>
        /// <returns>The scope of this target. May be null if absent or unknown.</returns>
        public Scope getScope()
        {
            return typeTargetScope;
        }

        /// <summary>
        /// Return the strike of this target. 
        /// Direction of dip with respect to north azimuth reference. 
        /// </summary>
        /// <returns>The strike of this target. May be null if absent or unknown.</returns>
        public Value getStrike()
        {
            return strike;
        }

        /// <summary>
        /// Return the thickness above of this target. 
        /// Height of target above center point. 
        /// </summary>
        /// <returns>The thickness above of this target. May be null if absent or unknown.</returns>
        public Value getThicknessAbove()
        {
            return thickAbove;
        }

        /// <summary>
        /// Return the thickness below of this target. 
        /// Height of target below center point. 
        /// </summary>
        /// <returns>The thickness below of this target. May be null if absent or unknown.</returns>
        public Value getThicknessBelow()
        {
            return thickBelow;
        }

        public enum Category
        {
            GEOLOGICAL,
            UNKNOWN
        }

        public enum Scope
        {
            ELLIPSOID,  //
            ELLIPTICAL, //Elliptical targets.
            HARDLINE,   //Boundary Conditions.
            IRREGULAR,  //Includes half circle and polygon.
            LEASE_LINE, //Boundary Conditions.
            LINE,       //Line target.
            PLANE,      //Plane target.
            POINT,      //Point Target.
            RECTANGULAR,//Rectangular Targets.
            UNKNOWN,    //The value is not known.
            VOLUME_3D   //Generic 3 dimensional target.
        }
    }
}