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
namespace witsmllib
{

    /**
     * Java representation of a WITSML "location".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlLocation
    {

        protected String crs; // wellCRS

        protected Value latitude; // latitude

        protected Value longitude; // longitude

        protected Value easting; // easting

        protected Value northing; // northing

        protected Value westing; // westing

        protected Value southing; // southing

        protected Value x; // projectedX

        protected Value y; // projectedY

        protected Value xLocal; //localX

        protected Value yLocal; // localY

        protected Boolean? _isOriginal; // original

        protected String description; // description

        protected String inputType; // inputType

        protected WitsmlLocation()
        {
        }

        public String getCrs()
        {
            return crs;
        }

        public Value getLatitude()
        {
            return latitude;
        }

        public Value getLongitude()
        {
            return longitude;
        }

        public Value getEasting()
        {
            return easting;
        }

        public Value getNorthing()
        {
            return northing;
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
            return x;
        }

        public Value getY()
        {
            return y;
        }

        public Value getXLocal()
        {
            return xLocal;
        }

        public Value getYLocal()
        {
            return yLocal;
        }

        public bool? isOriginal()
        {
            return _isOriginal;
        }

        public String getDescription()
        {
            return description;
        }

        public String getInputType()
        {
            return inputType;
        }

        /** {@inheritDoc} */
        
        public override String ToString()
        {
            return "\n" + WitsmlObject.ToString(this, 2);
        }
    }
}