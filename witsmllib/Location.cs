using System;

namespace witsmllib
{
   /// <summary>
   /// representation of the WITSML location object
   /// </summary>
    public abstract class Location
    {

        protected String wellCRS;       // A pointer to the wellCRS that defines the CRS for the coordinates. While optional, it is strongly recommended that this be specified.
        protected Value latitude;       // The latitude with north being positive.
        protected Value longitude;      // The longitude with east being positive.
        protected Value easting;        // The projected coordinate with east being positive. This is the most common type of projected coordinates. UTM coordinates are expressed in Easting and Northing.
        protected Value northing;       // The projected coordinate with north being positive. This is the most common type of projected coordinates. UTM coordinates are expressed in Easting and Northing.
        protected Value westing;        // The projected coordinate with west being positive. The positive directions are reversed from the usual Easting and Northing values. These values are generally located in the southern hemisphere, most notably in South Africa and Australia.
        protected Value southing;       // The projected coordinate with south being positive. The positive directions are reversed from the usual Easting and Northing values. These values are generally located in the southern hemisphere, most notably in South Africa and Australia.
        protected Value projectedX;     // The projected X coordinate with the positive direction unknown. ProjectedX and ProjectedY are used when it is not known what the meaning of the coordinates is. If the meaning is known, the Easting/Northing or Westing/Southing should be used. Use of this pair implies a lack of knowledge on the part of the sender.
        protected Value projectedY;     // The projected Y coordinate with the positive direction unknown. ProjectedX and ProjectedY are used when it is not known what the meaning of the coordinates is. If the meaning is known, the Easting/Northing or Westing/Southing should be used. Use of this pair implies a lack of knowledge on the part of the sender.
        protected Value localX;         // The local (engineering) X coordinate. The CRS will define the orientation of the axis.
        protected Value localY;         // The local (engineering) Y coordinate. The CRS will define the orientation of the axis.
        protected Boolean? _isOriginal; // Flag indicating (if "true" or "1") that this pair of values was the original data given for the location. If the pair of values was calculated from an original pair of values, this flag should be "false" (or "0"), or not present.
        protected String description;   // A Comment, generally given to help the reader interpret the coordinates if the CRS and the chosen pair do not make them clear.
        protected String inputType;     // inputType ???

        protected Location()
        {

        }

        public String getDescription()
        {
            return description;
        }

        public String getCrs()
        {
            return wellCRS;
        }

        public Value getLatitude()
        {
            return latitude;
        }

        public Value getLongitude()
        {
            return longitude;
        }

        public Value getNorthing()
        {
            return northing;
        }

        public Value getEasting()
        {
            return easting;
        }
        
        public Value getWesting()
        {
            return westing;
        }

        public Value getSouthing()
        {
            return southing;
        }

        public Value getX()
        {
            return projectedX;
        }

        public Value getY()
        {
            return projectedY;
        }

        public Value getXLocal()
        {
            return localX;
        }

        public Value getYLocal()
        {
            return localY;
        }

        public bool? isOriginal()
        {
            return _isOriginal;
        }

        
        public String getInputType()
        {
            return inputType;
        }
   
        public override String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }
    }
}