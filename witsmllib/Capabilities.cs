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
     * Define a WITSML <em>capabilities</em> object. The capabilities are used when
     * a server and client communicates. The server will identify its WITSML
     * version and what function and data objects it supports, while a client
     * will identify what WITSML version it expects.
     * <p>
     * The usage and content of the capabilities are by the WITSML standard
     * completely optional. Users of NWitsml are however encouraged to specify
     * their client capabilities properly.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    public sealed class Capabilities
    {
        /** True if this is server capabilitries, false if it is client capabilities. */
        private  bool _isServer;

        /** Function capabilities for server. Never null if isServer is true, null if false. */
        internal  List<FunctionCapability> functions; // Server only

        /** WITSML version supported. May be null. */
        private  String witsmlVersion;

        /** Contact name. May be null. */
        private  String contactName;

        /** Contact e-mail. May be null. */
        private  String contactEmail;

        /** Contact phone. May be null. */
        private  String contactPhone;

        /** System name. May be null. */
        private  String name;

        /** System description. May be null. */
        private  String description;

        /** System vendor name. May be null. */
        private  String vendor;

        /** System program version. May be null. */
        private  String programVersion;

        /**
         * Create a capabilities object with the specified properties.
         *
         * @param version         WITSML version accepted by client. May be bull.
         * @param isServer        Indicates if this is a server (true) or client (false)
         *                        capabilities instance.
         * @param contactName     Contact name for the site. May be null.
         * @param contactEmail    Contact e-mail for the site. May be null.
         * @param contactPhone    Contact phone for the site. May be null.
         * @param name            System product name. May be null.
         * @param description     System description. May be null.
         * @param vendor          System vendor. May be null.
         * @param programVersion  System version. May be null.
         */
        Capabilities(WitsmlVersion version,
                     bool isServer,
                     String contactName,
                     String contactEmail,
                     String contactPhone,
                     String name,
                     String description,
                     String vendor,
                     String programVersion)
        {
            this._isServer = isServer;
            this.functions = isServer ? new List<FunctionCapability>() : null;
            this.witsmlVersion = version != null ? version.getVersion() : null;
            this.contactName = contactName;
            this.contactEmail = contactEmail;
            this.contactPhone = contactPhone;
            this.name = name;
            this.description = description;
            this.vendor = vendor;
            this.programVersion = programVersion;
        }

        /**
         * Create a client capabilities object with the specified properties.
         *
         * @param version         WITSML version accepted by client. May be null.
         * @param contactName     Contact name. May be null.
         * @param contactEmail    Contact e-mail. May be null.
         * @param contactPhone    Contact phone number. May be null.
         * @param name            System product name. May be null.
         * @param description     System description. May be null.
         * @param vendor          System vendor. May be null.
         * @param programVersion  System version. May be null.
         */
        public Capabilities(WitsmlVersion version,
                            String contactName,
                            String contactEmail,
                            String contactPhone,
                            String name,
                            String description,
                            String vendor,
                            String programVersion)
            : this(version, false, contactName, contactEmail, contactPhone,
                 name, description, vendor, programVersion)
        { }

        /**
         * Create a capabilities instance by parsing the specified WITSML string.
         *
         * @param xml  XML defining the capabilities.
         */
        internal Capabilities(WitsmlVersion version, String xml)
        { //throws WitsmlParseException {
            //Debug.Assert(version != null : "version cannot be null";
            //Debug.Assert(xml != null : "xml cannot be null";

            String witsmlVersionTmp = null;
            String contactNameTmp = null;
            String contactEmailTmp = null;
            String contactPhoneTmp = null;
            String nameTmp = null;
            String descriptionTmp = null;
            String vendorTmp = null;
            String programVersionTmp = null;

            //SAXBuilder builder = new SAXBuilder();

            try
            {
                XDocument document = XDocument.Load(new StringReader(xml));
                XElement root = document.Root; //.getRootElement();
                //Debug.Assert(root != null; //TODO: Can this happen? Throw illegal arg exception?

                XNamespace @namespace = root.Name.Namespace; //.getNamespace();
                XElement capServerElement = root.Element(@namespace + "capServer");//, @namespace);
                XElement capClientElement = root.Element(@namespace + "capClient"); //, @namespace);

                _isServer = capServerElement != null;
                functions = _isServer ? new List<FunctionCapability>() : null;

                XElement capElement = _isServer ? capServerElement : capClientElement;

                if (capElement != null)
                {
                    witsmlVersionTmp = capElement.Attribute("apiVers").Value;

                    XElement contactElement = capElement.Element(@namespace + "contact");//, @namespace);
                    if (contactElement != null)
                    {
                        contactNameTmp = contactElement.Element(contactElement.Name.Namespace+"name").Value.Trim();//, contactElement.Name.Namespace.getNamespace());
                        contactEmailTmp = contactElement.Element(contactElement.Name.Namespace+"email").Value.Trim();//, contactElement.getNamespace());
                        contactPhoneTmp = contactElement.Element(contactElement.Name.Namespace+"phone").Value.Trim();//, contactElement.getNamespace());
                    }

                    nameTmp = capElement.Element(capElement.Name.Namespace+"name").Value; //, capElement.getNamespace());
                    descriptionTmp = capElement.Element(capElement.Name.Namespace + "description").Value; //, capElement.getNamespace());
                    vendorTmp = capElement.Element (capElement.Name.Namespace + "vendor").Value; //, capElement.getNamespace());
                    programVersionTmp = capElement.Element (capElement.Name.Namespace + "version").Value; //, capElement.getNamespace());

                    var functionElements = capElement.Elements(@namespace + "function");//, @namespace);
                    //for (var i = functionElements.iterator(); i.hasNext(); )
                    foreach(var i in functionElements)
                    {
                        XElement functionElement = (XElement)i;//.next();
                        String functionName = functionElement.Attribute("name").Value ;
                        String functionVersion = functionElement.Attribute("apiVers").Value ;

                        FunctionCapability function = new FunctionCapability(functionName,functionVersion);

                        var dataObjectElements = functionElement.Elements(@namespace+"dataObject"/*, @namespace*/);
                        //for (var j = dataObjectElements.iterator(); j.hasNext(); )
                        foreach(var j in dataObjectElements)
                        {
                            XElement dataObjectElement = (XElement)j;// j.next();
                            String dataObject = dataObjectElement.Value.Trim();//.getTextTrim();
                            function.addWitsmlType(dataObject);
                        }
                        addFunction(function);
                    }
                }

                witsmlVersion = witsmlVersionTmp;
                contactName = contactNameTmp;
                contactEmail = contactEmailTmp;
                contactPhone = contactPhoneTmp;
                name = nameTmp;
                description = descriptionTmp;
                vendor = vendorTmp;
                programVersion = programVersionTmp;
            }
            
            catch (IOException exception)
            {
                // Convert to a non-IO exception to hide implementation details of this class.
                throw new WitsmlParseException(xml, exception);
            }catch (Exception /*JDOMException */ exception)
            {
                // Convert to a non-JDOM exception to hide implementation details of this class.
                throw new WitsmlParseException(xml, exception);
            }
        }

        /**
         * Check if this is a server (true) or a client (false) capabilities
         * object.
         *
         * @return  True if this is a server capabilities object, false if it
         *          is a client capabilities object.
         */
        public bool isServer()
        {
            return _isServer;
        }

        /**
         * Return WITSML version(s) supported. The WITSML standard
         * unfortunately puts no constraint on the format of this
         * information, many servers will list all their supported
         * formats in a comma separated list.
         * <p>
         * The client capabilities instance will return the string
         * representation of the argument passed to the constructor,
         * or null if none was provided.
         *
         * @return  WITSML version(s) supported. May be null.
         */
        public String getWitsmlVersion()
        {
            return witsmlVersion;
        }

        /**
         * Get contact name for the site.
         *
         * @return  Contact name for the site. May be null.
         */
        public String getContactName()
        {
            return contactName;
        }

        /**
         * Return contact e-mail for the site.
         *
         * @return Contact e-mail for the site. May be null.
         */
        public String getContactEmail()
        {
            return contactEmail;
        }

        /**
         * Get contact phone for the site.
         *
         * @return  Contact phone for the site. May be null.
         */
        public String getContactPhone()
        {
            return contactPhone;
        }

        /**
         * Return name of the application program.
         *
         * @return  Name of application program. May be null.
         */
        public String getName()
        {
            return name;
        }

        /**
         * Get description of the application program.
         *
         * @return  Description of the application program. May be null.
         */
        public String getDescription()
        {
            return description;
        }

        /**
         * Get application program vendor.
         *
         * @return  Application program vendor. May be null.
         */
        public String getVendor()
        {
            return vendor;
        }

        /**
         * Get application program version.
         *
         * @return Application program version. May be null.
         */
        public String getProgramVersion()
        {
            return programVersion;
        }

        /**
         * Add function capabilities to this capabilities instance.
         * Functions are a property of the server only and is not used
         * with client capabilities.
         *
         * @param function  Function capabilties to add.
         */
        void addFunction(FunctionCapability function)
        {
            //Debug.Assert(function != null : "function cannot be null";
            //Debug.Assert(isServer : "functions are only supported for server capabilities";

            functions.Add(function);
        }

        /**
         * Return all supported functions and which objects they support.
         * Functions are a property of the server only and for a client
         * capabilities object this property will be null.
         *
         * @return  Functions of this capabilities. Null if isServer() is false.
         *          Never null if isServer() is true.
         */
        internal List<FunctionCapability> getFunctions()
        {
            return this.functions;
        }

        /**
         * Return this capabilities as a capabilities WITSML definition
         * (an XML string).
         *
         * @return  WITSML definition (an XML string) of this capabilities object.
         */
        public String toXml()
        {
            StringBuilder xml = new StringBuilder();

            // WITSML Version
            xml.Append(_isServer ? "<capServer" : "<capClient");
            xml.Append(witsmlVersion == null ? ">" : " apiVers=\"" + witsmlVersion + "\">");

            // Contact Information
            xml.Append("<contact>");
            if (contactName != null)
                xml.Append("<name>" + contactName + "</name>");
            if (contactEmail != null)
                xml.Append("<email>" + contactEmail + "</email>");
            if (contactPhone != null)
                xml.Append("<phone>" + contactPhone + "</phone>");
            xml.Append("</contact>");

            // Name
            if (name != null)
                xml.Append("<name>" + name + "</name>");

            // Description
            if (description != null)
                xml.Append("<description>" + description + "</description>");

            // Vendor
            if (vendor != null)
                xml.Append("<vendor>" + vendor + "</vendor>");

            // Version
            if (programVersion != null)
                xml.Append("<version>" + programVersion + "</version>");

            // Functions
            if (functions != null)
            {
                foreach (FunctionCapability function in functions)
                {
                    String functionName = function.getFunctionName();
                    String version = function.getVersion();

                    xml.Append("<function ");
                    xml.Append("name=\"" + functionName + "\"");
                    xml.Append(version != null ? " apiVers=\"" + version + "\">" : ">");

                    List<String> dataObjects = function.getSupportedWitsmlTypes();
                    if (dataObjects != null)
                    {
                        foreach (String dataObject in dataObjects)
                        {
                            xml.Append("<dataObject>" + dataObject + "</dataObject>");
                        }
                    }
                }
                xml.Append("</function>");
                // TODO: XML well-formed?
            }
            xml.Append(_isServer ? "</capServer>" : "</capClient>");

            return xml.ToString();
        }

        /**
         * Return a string representation of this instance.
         *
         * @return  A string representation of this instance. Never null.
         */
        
        public override String ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("WITSML Version.......: " + witsmlVersion + "\n");
            s.Append("Contact Name.........: " + contactName + "\n");
            s.Append("Contact e-Mail.......: " + contactEmail + "\n");
            s.Append("Contact Phone........: " + contactPhone + "\n");
            s.Append("Name.................: " + name + "\n");
            s.Append("Description..........: " + description + "\n");
            s.Append("Vendor...............: " + vendor + "\n");
            s.Append("Program Version......: " + programVersion + "\n");
            if (functions != null)
            {
                s.Append("Functions............: ");
                foreach (FunctionCapability function in functions)
                    s.Append("\n  " + function);
            }
            s.Append("\n");

            return s.ToString();
        }
    }
}