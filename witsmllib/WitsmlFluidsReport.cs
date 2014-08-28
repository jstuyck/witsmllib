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
     * Java representation of a WITSML "fluidsReport".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib;
    public abstract class WitsmlFluidsReport : WitsmlObject
    {
        /** The WITSML type name */
        private static String WITSML_TYPE = "fluidsReport";

        protected DateTime? time; // dTim
        protected Value md; // md
        protected Value tvd; // tvd
        protected Int32? reportNumber; //numReport

        protected WitsmlFluidsReport(WitsmlServer server, String id, String name,
                                     WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        public DateTime? getTime()
        {
            return time;
        }

        public Value getMd()
        {
            return md;
        }

        public Value getTvd()
        {
            return tvd;
        }

        public Int32? getReportNumber()
        {
            return reportNumber;
        }
    }
}