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
     * List of the possible versions of the WITSML standard.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */

    public class WitsmlVersion
    {

        public static WitsmlVersion VERSION_1_2_0 = new WitsmlVersion("1.2.0", "http://www.witsml.org/schemas/120");
        public static WitsmlVersion VERSION_1_3_1 = new WitsmlVersion("1.3.1", "http://www.witsml.org/schemas/131");
        public static WitsmlVersion VERSION_1_4_0 = new WitsmlVersion("1.4.0", "http://www.witsml.org/schemas/140");

        private string _version;
        private string _ns;
        public WitsmlVersion(string version, string ns)
        {
            _version = version;
            _ns = ns;
        }
        public string getVersion() { return _version; }
        public string getNamespace() { return _ns; }
        public string ToString() { return _version; }
    }

    //public enum WitsmlVersion {
    //    /** Version 1.2.0. */
    //    VERSION_1_2_0("1.2.0", "http://www.witsml.org/schemas/120"),

    //    /** Version 1.3.1.1. */
    //    VERSION_1_3_1("1.3.1", "http://www.witsml.org/schemas/131"),

    //    /** Version 1.4.0. */
    //    VERSION_1_4_0("1.4.0", "http://www.witsml.org/schemas/140");

    //    /** Textual description of this version. Non-null. */
    //    private String version;

    //    /** Location of namespace for this version. Non-null. */
    //    private String namespace;

    //    /**
    //     * Create a version enumeration element.
    //     *
    //     * @param version  Textual description. Non-nul.
    //     */
    //    private WitsmlVersion(String version, String namespace) {
    //        //Debug.Assert(version != null : "version cannot be null";
    //        //Debug.Assert(namespace != null : "namespace cannot be null";

    //        this.version = version;
    //        this.namespace = namespace;
    //    }

    //    /**
    //     * Return version of the element as a string.
    //     *
    //     * @return  Version of this element as a string. Never null.
    //     */
    //    public String getVersion() {
    //        return version;
    //    }

    //    /**
    //     * Return namespace for this specific WITSML version.
    //     *
    //     * @return Namespace for this WITSML version. Never null.
    //     */
    //    public String getNamespace() {
    //        return namespace;
    //    }

    //    /** {@inheritDoc} */
    //    
    //    public String ToString() {
    //        return version;
    //    }
    //}
}