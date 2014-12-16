using System;
using witsmllib.util;
using System.Xml.Linq;

namespace witsmllib.v131
{
    sealed class WitsmlWell : witsmllib.WitsmlWell
    {

        /// <summary>
        /// Create a new WITSML well instance.
        /// </summary>
        /// <param name="server">Server this instance lives within. Non-null.</param>
        /// <param name="id">ID of instance. May be null.</param>
        /// <param name="name">Name of instance. May be null.</param>
        /// <param name="parent">Parent of instance. Always null on this level.</param>
        private WitsmlWell(WitsmlServer server, String id, String name, WitsmlObject parent)

            : base(server, id, name, parent)
        { }

        /// <summary>
        /// Factory method for this type.
        /// </summary>
        /// <param name="server">Server the new instance lives within. Non-null.</param>
        /// <param name="parent">Parent instance. Always null on this level.</param>
        /// <param name="element">XML element to create instance from. Non-null.</param>
        /// <returns>New WITSML well instance. Never null.</returns>
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent,
                                        XElement element)
        {
            if (server == null)
                throw new ArgumentNullException("server cannot be null");
            if (element == null)
                throw new ArgumentNullException("element cannot be null");

            String id = element.Attribute("uid").Value;
            String name = element.Element(element.Name.Namespace + "name").Value.Trim(); //, element.getNamespace());

            WitsmlWell witsmlWell = new WitsmlWell(server, id, name, parent);
            witsmlWell.update(element);

            return witsmlWell;
        }

        /// <summary>
        /// Return complete XML query for this type.
        /// </summary>
        /// <param name="id">ID of instance to get. May be empty to indicate all. Non-null.</param>
        /// <param name="parentId">Parent IDs. Closest first. May be empty if instances are accessed from the root. Non-null.</param>
        /// <returns>XML query. Never null.</returns>
        static String getQuery(String id, params String[] parentId)
        {
            if (id == null)
                throw new ArgumentNullException("id cannot be null");
            if (parentId == null)
                throw new ArgumentNullException("parentId cannot be null");

            String query = "<wells xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
                           "  <well uid=\"" + id + "\">" +
                           "    <name/>" +
                           "    <nameLegal/>" +
                           "    <numLicense/>" +
                           "    <numGovt/>" +
                           "    <dTimLicense/>" +
                           "    <field/>" +
                           "    <country/>" +
                           "    <state/>" +
                           "    <county/>" +
                           "    <region/>" +
                           "    <district/>" +
                           "    <block/>" +
                           "    <timeZone/>" +
                           "    <operator/>" +
                           "    <operatorDiv/>" +
                           "    <pcInterest/>" + //BM added uom
                           "    <numAPI/>" +
                           "    <statusWell/>" +
                           "    <purposeWell/>" +
                           "    <fluidWell/>" +
                           "    <directionWell/>" +
                           "    <dTimSpud/>" +
                           "    <dTimPa/>" +
                           "    <wellheadElevation/>" +
                           "    <wellDatum/>" +
                           "    <groundElevation/>" +
                           "    <waterDepth uom=\"" + WitsmlServer.distUom + "\"/>" +
                           "    <wellLocation>" +
                           WitsmlLocation.getQuery() +
                           "    </wellLocation>" +
                           "    <referencePoint/>" +
                           "    <wellCRS/>" +
                           WitsmlCommonData.getQuery() +
                           "  </well>" +
                           "</wells>";

            return query;
        }

        /// <summary>
        /// Parse the specified DOM element and instantiate the properties of this instance.
        /// </summary>
        /// <param name="element">XML element to parse. Non-null.</param>
        void update(XElement element)
        {           
            if (element == null)
                throw new ArgumentException("element cannot be null");

            legalName = XmlUtil.update(element, "nameLegal", legalName);
            licenseNumber = XmlUtil.update(element, "numLicense", licenseNumber);
            wellNumber = XmlUtil.update(element, "numGovt", wellNumber);
            licenseIssueTime = XmlUtil.update(element, "dTimLicense", licenseIssueTime);
            field = XmlUtil.update(element, "field", field);
            country = XmlUtil.update(element, "country", country);
            state = XmlUtil.update(element, "state", state);
            county = XmlUtil.update(element, "county", county);
            region = XmlUtil.update(element, "region", region);
            district = XmlUtil.update(element, "district", district);
            block = XmlUtil.update(element, "block", block);
            timeZone = XmlUtil.update(element, "timeZone", timeZone);
            @operator = XmlUtil.update(element, "operator", @operator);
            operatorDivision = XmlUtil.update(element, "operatorDiv", operatorDivision);
            try
            {
                operatorInterestShare = XmlUtil.update(element, "pcInterest", operatorInterestShare);
            }
            catch (Exception e)
            {

            }
            apiNumber = XmlUtil.update(element, "numAPI", apiNumber);
            status = XmlUtil.update(element, "statusWell", status);
            purpose = XmlUtil.update(element, "purposeWell", purpose);
            fluidType = XmlUtil.update(element, "fluidWell", fluidType);
            flowDirection = XmlUtil.update(element, "directionWell", flowDirection);
            spudTime = XmlUtil.update(element, "dTimSpud", spudTime);
            pluggedTime = XmlUtil.update(element, "dTimPa", pluggedTime);
            wellHeadElevation = XmlUtil.update(element, "wellheadElevation", wellHeadElevation);
            groundElevation = XmlUtil.update(element, "groundElevation", groundElevation);
            waterDepth = XmlUtil.update(element, "waterDepth", waterDepth);

            XElement locationElement = element.Element(element.Name.Namespace + "wellLocation");//, element.getNamespace());
            if (locationElement != null)
                location = new WitsmlLocation(locationElement);

            XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}