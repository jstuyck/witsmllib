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
using System;
namespace witsmllib
{

    /**
     * Java representation of a WITSML "risk".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public abstract class WitsmlRisk : WitsmlObject
    {

        /** The WITSML type name */
        private static String WITSML_TYPE = "risk";

        protected String reference; // objectReference
        protected String referenceType; // objectReferemnce.object
        protected String referenceId; // objectReference.uidRef
        protected String type; // type
        protected String category; // category
        protected String subCategory; //subCategory
        protected Value mdHoleStart; // mdHoleStart
        protected Value mdHoleEnd; // mdHoleEnd
        protected Value tvdHoleStart; // mdHoleStart
        protected Value tvdHoleEnd; // mdHoleEnd
        protected Value holeDiameter; // diaHole
        protected Int32? severityLevel;
        protected Int32? probabilityLevel;
        protected String summary;
        protected String details;
        protected String identification;
        protected String mitigation;

        protected WitsmlRisk(WitsmlServer server, String id, String name,
                             WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        public String getReference()
        {
            return reference;
        }

        public String getReferenceType()
        {
            return referenceType;
        }

        public String getReferenceId()
        {
            return referenceId;
        }

        public String getType()
        {
            return type;
        }

        public String getCategory()
        {
            return category;
        }

        public String getSubCategory()
        {
            return subCategory;
        }

        public Value getMdHoleStart()
        {
            return mdHoleStart;
        }

        public Value getMdHoleEnd()
        {
            return mdHoleEnd;
        }

        public Value getTvdHoleStart()
        {
            return tvdHoleStart;
        }

        public Value getTvdHoleEnd()
        {
            return tvdHoleEnd;
        }

        public Value getHoleDiameter()
        {
            return holeDiameter;
        }

        public Int32? getSeverityLevel()
        {
            return severityLevel;
        }

        public Int32? getProbabilityLevel()
        {
            return probabilityLevel;
        }

        public String getSummary()
        {
            return summary;
        }

        public String getDetails()
        {
            return details;
        }

        public String getIdentification()
        {
            return identification;
        }

        public String getMitigation()
        {
            return mitigation;
        }
    }
}