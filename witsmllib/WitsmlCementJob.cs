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
     * Java representation of a WITSML "cementJob".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    public abstract class WitsmlCementJob : WitsmlObject
    {
        /** The WITSML type name */
        private  static String WITSML_TYPE = "cementJob";

        protected String jobType; // jobType
        protected String jobConfiguration; // jobConfig

        /**
         * Create a new cement job instance.
         *
         * @param version   WITSML version of this instance.
         * @param id        ID of this instance.
         * @param name      Name of this instance.
         * @param parent    Parent of this instance. May be null.
         * @param parentId  ParentId of this instance if parent is null. May be null.
         */
        protected WitsmlCementJob(WitsmlServer server, String id, String name,
                                  WitsmlObject parent, String parentId)
            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public String getJobType()
        {
            return jobType;
        }

        public String getJobConfiguration()
        {
            return jobConfiguration;
        }
    }
}