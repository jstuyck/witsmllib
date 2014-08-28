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

    //import java.util.Date;

    /**
     * Java representation of a WITSML "mudLog".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlMudLog : WitsmlObject
    {

        /** The WITSML type name */
        private  static String WITSML_TYPE = "mudLog";

        protected DateTime? time; // dTim
        protected String mudLogCompany; // mudLogCompany
        protected String mudLogEngineers; // mudLogEngineers
        protected Value mdStart; // startMd
        protected Value mdEnd; // endMd

        /**
         * Create a mud log object with speified ID and parent.
         *
         * @param id        ID of this mud log.
         * @param wellbore  Parent wellbore of this mud log.
         */
        protected WitsmlMudLog(WitsmlServer server, String id, String name,
                               WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        /**
         * Get company of this mud log.
         *
         * @return  Company of this mud log. May be null.
         */
        public String getMudLogCompany()
        {
            return mudLogCompany;
        }

        /**
         * Get engineers of this mud log.
         *
         * @return  Engineers of this mud log. May be null.
         */
        public String getMudLogEngineers()
        {
            return mudLogEngineers;
        }

        public DateTime? getTime()
        {
            return time;
        }

        public Value getMdStart()
        {
            return mdStart;
        }

        public Value getMdEnd()
        {
            return mdEnd;
        }
    }
}