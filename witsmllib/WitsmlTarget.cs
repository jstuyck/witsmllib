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
     * Java representation of a WITSML "target".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlTarget : WitsmlObject
    {

        /** The WITSML type name */
        private  static String WITSML_TYPE = "target";

        protected String parentTargetId; // uidTargetParent
        protected Value north; // dispNsCenter
        protected Value east; // dispEwCenter
        protected Value tvd; // tvd

        /**
         * Create a well object with specified ID.
         *
         * @param id  ID of this well.
         */
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
    }
}