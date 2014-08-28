namespace witsmllib
{

    /// <summary>
    /// List of the possible versions of the WITSML standard.
    /// </summary>
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

        public string getVersion()
        {
            return _version;
        }

        public string getNamespace()
        {
            return _ns;
        }

        public override string ToString()
        {
            return _version;
        }
    }


}