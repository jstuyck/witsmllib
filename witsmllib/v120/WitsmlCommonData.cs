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
namespace witsmllib.v120
{

    /**
     * Version specific implementation of the WitsmlCommonData type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    public sealed class WitsmlCommonData : witsmllib.CommonData
    {

        /**
         * Create a WITSML common data instance from the given
         * XML element node.
         *
         * @param element  XML element to create instance from. Non-null.
         */
        public WitsmlCommonData(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            source = XmlUtil.update(element, "nameSource", source);
            timeCreated = XmlUtil.update(element, "dTimCreation", timeCreated);
            timeUpdated = XmlUtil.update(element, "dTimLastChange", timeUpdated);
            state = XmlUtil.update(element, "itemState", state);
            comments = XmlUtil.update(element, "comments", comments);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
         public static String getQuery()
        {
            String query = "<commonData>" +
                           "  <nameSource/>" +
                           "  <dTimStamp/>" +
                           "  <dTimCreation/>" +
                           "  <dTimLastChange/>" +
                           "  <itemState/>" +
                           "  <comments/>" +
                           "</commonData>";

            return query;
        }
    }
}