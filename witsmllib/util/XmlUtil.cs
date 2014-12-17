using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace witsmllib.util
{

    /**
     * Collection of XML utilities.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    
    public sealed class XmlUtil
    {

        /**
         * Private constructor to prevent client instantiation.
         */
        private XmlUtil()
        {
            //Debug.Assert(false : "This constructor should never bve called";
        }

        /**
         * Create a DOM document from a specified XML string.
         *
         * @param xml  String to create DOM document from. Non-null.
         * @return     DOM document. Null if the parse operation failed somehow.
         */
        private static XDocument newDocument(String xml)
        {
            return XDocument.Parse(xml); 
        }

        /**
         * Check if the specified XML is valid according to the specified schema.
         *
         * @param xml         XML to validate. Non-null.
         * @param schemaFile  Schema file. Non-null.
         * @return            True if the XML is valid, false otherwise.
         * @throws  ArgumentException  If xml is null or schemaFile is null or
         *                                    schemaFile doesnt exist.
         */
        public static void validate(String xml, string schemaFilePath)
        { 

            if (xml == null)
                throw new ArgumentNullException("xml cannot be null");

            if (schemaFilePath == null)
                throw new ArgumentNullException("schemaFile cannot be null");

            if (!File.Exists(schemaFilePath))
                throw new FileNotFoundException("file " + schemaFilePath + " doesn't exist: " + schemaFilePath);

            // Parse the XML string into a DOM tree.
            XDocument document = newDocument(xml);
            
            // Create a SchemaFactory capable of understanding WXS schemas.
            //SchemaFactory schemaFactory =
            //    SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);

            //// Load a WXS schema, represented by a Schema instance.
            //Schema schema = schemaFactory.newSchema(schemaFile);

            XmlSchema schema = XmlSchema.Read(new FileStream(schemaFilePath, FileMode.Open), null);   //TODO - add handler to this?
            var schemaset = new XmlSchemaSet();
            schemaset.Add(schema);
             // Create a Validator object, which can be used to validate
            // an instance document.
           // Validator validator = schema.newValidator();

            // Validate the DOM tree.
            try
            {
                //validator.validate(new DOMSource(document));
                document.Validate(schemaset, null); 
            }
            catch (IOException exception)
            {
                //Debug.Assert(false : exception.getMessage();
            }
        }

        /**
         * Check if the specified XML is well-formed.
         *
         * @param xml  XML to check. Non-null.
         * @return     True if the XML is well-formed, false otherwise.
         * @throws ArgumentException  If xml is null.
         */
        public static bool isWellFormed(String xml)
        {
            if (xml == null)
                throw new ArgumentException("xml cannot be null");

            try
            {
                newDocument(xml);
                return true;
            }
            catch (Exception)// (SAXException exception)
            {
                return false;
            }
        }

        /**
         * Pretty print the specified XML string.
         *
         * @param xml  XML string to pretty print. Non-null.
         * @return     A pretty printed version of the input, or the string
         *             itself if it doesn't form a well-formed XML.
         * @throws ArgumentException  If xml is null.
         */
        public static String prettyPrint(String xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml cannot be null");
            XDocument x = XDocument.Parse(xml);
            
            return x.ToString();
        }

        /// <summary>
        ///  Convenience method for parsing a standard WITSML double string
        ///  to Double instance
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns>Double instance, or null if input is null or incorrect format.</returns>
        private static Double? getDouble(String valueString)
        {
            double res;
            if (Double.TryParse(valueString, out res))
                return res;
            else
                return null;
        }

        /// <summary>
        ///  Convenience method for parsing a standard WITSML integer string
        ///  to an integer instance.
        /// </summary>
        /// <param name="valueString">WITSML integer string. May be null.</param>
        /// <returns>int instance, or null if input is null or incorrect format.</returns>
        private static int? getInteger(String valueString)
        {
            int res;
            if (int.TryParse(valueString, out res))
                return res;
            else
                return null;
        }

        /// <summary>
        /// Convenience method for parsing a standard WITSML time string
        /// </summary>
        /// <param name="timeString">WITSML datetime string. May be null.</param>
        /// <returns>Datetime instance, or null if input is null or incorrect format.</returns>
        public static DateTime?  getTime(String timeString)
        {
            DateTime res;
            if (DateTime.TryParse(timeString, out res))
                return res;
            else
                return null;
        }

        /// <summary>
        /// Convenience method for parsing a standard WITSML boolean
        /// </summary>
        /// <param name="booleanString">WITSML boolean string. May be null.</param>
        /// <returns>Bolean instance, or null if input is null or incorect format.</returns>
        public static Boolean? getBoolean(String booleanString)
        {
            
            if (booleanString == null)
                return null;

            return booleanString.ToLower().Equals("true") ||
                   booleanString.Equals("1");
        }

        //TODO - use IsEmpty instead of check for null?
        /// <summary>
        /// Return the new value for the specfied element.
        /// </summary>
        /// <param name="parentElement">Parent of element to get new value for. Non-null.</param>
        /// <param name="childName"> Name of child element to extract value of. Non-null.</param>
        /// <param name="oldValue">The present value. This will be returned if the element
        /// requested does not exist. May be null.</param>
        /// <returns>The child element value, or oldValue if the requested
        /// element doesn't exist. May be null.</returns>
        public static String update(XElement parentElement, string childName, string oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //Add namespace to this?
            return element == null ? oldValue : element.Value.Trim(); 
        }
        //public static String update(XElement parentElement, String childName, String oldValue)
        //{
        //    XElement element = parentElement.Element(childName, parentElement.getNamespace());
        //    return element == null ? oldValue : element.getTextTrim();
        //}

        public static DateTime?  update(XElement parentElement, String childName, DateTime?  oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); // getChild(childName, parentElement.getNamespace());
            return element == null ? oldValue : getTime(element.Value.Trim()); 
        }
        //public static DateTime? update(XElement parentElement, String childName, DateTime? oldValue)
        //{
        //    XElement element = parentElement.Element(childName, parentElement.getNamespace());
        //    return element == null ? oldValue : getTime(element.getTextTrim());
        //}

        public static Int32? update(XElement parentElement, String childName, Int32? oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //.Element(childName, parentElement.getNamespace());
            return element == null ? oldValue : getInteger(element.Value.Trim()); //.getTextTrim());
        }

        public static Boolean? update(XElement parentElement, String childName, Boolean? oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName); //.Element(childName, parentElement.getNamespace());
            return element == null ? oldValue : getBoolean(element.Value.Trim());//.getTextTrim());
        }

        public static Value update(XElement parentElement, String childName, Value oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace + childName);//.Element(childName, parentElement.getNamespace());
            if (element == null)
                return oldValue;

            //BM - sample data was missing uom.. is this not required? 
            var att = element.Attribute("uom");
            
            String unit = att==null? null: att.Value;//.Attribute("uom");
            Double? value = getDouble(element.Value.Trim()); //.getTextTrim());

            return new Value(value.Value , unit);
        }

        public static String update(XElement parentElement, String childName, String attributeName, String oldValue)
        {
            XElement element = parentElement.Element(parentElement.Name.Namespace+ childName); //.Element(childName, parentElement.getNamespace());
            String newValue = element != null ? element.Attribute(attributeName).Value: null; //.Attribute(attributeName) : null;
            return newValue != null ? newValue : oldValue;
        }
    }

}