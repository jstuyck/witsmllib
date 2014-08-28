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
     * Java representation of a WITSML "formationMarker".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlFormationMarker : WitsmlObject
    {

        /** The WITSML type name */
        private static String WITSML_TYPE = "formationMarker";

        protected Value mdPrognosed; // mdPrognosed
        protected Value tvdPrognosed; // tvdPrognosed
        protected Value mdTopSample; // mdTopSample
        protected Value tvdTopSample; // tvdTopSample
        protected Value bedThickness; // thicknessBed
        protected Value apparentThickness; // thicknessApparent
        protected Value perpendicularThickness; // thicknessPerpen
        protected Value mdLogSample; // mdLogSample
        protected Value tvdLogSample; // tvdLogSample
        protected Value dip; // dip
        protected Value dipDirection; // dipDirection
        protected String startigraphicClassification; // chronostratigraphic
        protected String formationName; // nameFormation
        protected String description; // description

        protected WitsmlFormationMarker(WitsmlServer server, String id, String name,
                                        WitsmlObject parent, String parentId)
            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public Value getMdPrognosed()
        {
            return mdPrognosed;
        }

        public Value getTvdPrognosed()
        {
            return tvdPrognosed;
        }

        public Value getMdTopSample()
        {
            return mdTopSample;
        }

        public Value getTvdTopSample()
        {
            return tvdTopSample;
        }

        public Value getBedThickness()
        {
            return bedThickness;
        }

        public Value getApparentThickness()
        {
            return apparentThickness;
        }

        public Value getPerpendicularThickness()
        {
            return perpendicularThickness;
        }

        public Value getMdLogSample()
        {
            return mdLogSample;
        }

        public Value getTvdLogSample()
        {
            return mdLogSample;
        }

        public Value getDip()
        {
            return dip;
        }

        public Value getDipDirection()
        {
            return dipDirection;
        }

        public String getFormationName()
        {
            return formationName;
        }

        public String getDescription()
        {
            return description;
        }
    }
}