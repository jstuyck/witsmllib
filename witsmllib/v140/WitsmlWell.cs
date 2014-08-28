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
namespace witsmllib.v140
{

    /**
     * Version specific implementation of the WitsmlWell type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlWell : witsmllib.WitsmlWell
    {

        /**
         * Create a new WITSML well instance.
         *
         * @param server  Server this instance lives within. Non-null.
         * @param id      ID of instance. May be null.
         * @param name    Name of instance. May be null.
         * @param parent  Parent of instance. Always null on this level.
         */
        private WitsmlWell(WitsmlServer server, String id, String name, WitsmlObject parent)
        
            :base(server, id, name, parent)
        {}

        /**
         * Factory method for this type.
         *
         * @param server   Server the new instance lives within. Non-null.
         * @param parent   Parent instance. Always null on this level.
         * @param element  XML element to create instance from. Non-null.
         * @return         New WITSML well instance. Never null.
         */
        static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent,
                                        XElement element)
        {
            //Debug.Assert(server != null : "server cannot be null";
            //Debug.Assert(element != null : "element cannot be null";

            String id = element.Attribute("uid").Value;
            String name = element.Element(element.Name.Namespace +"name").Value.Trim(); //, element.getNamespace());

            WitsmlWell witsmlWell = new WitsmlWell(server, id, name, parent);
            witsmlWell.update(element);

            return witsmlWell;
        }

        /**
         * Return complete XML query for this type.
         *
         * @param id        ID of instance to get. May be empty to indicate all.
         *                  Non-null.
         * @param parentId  Parent IDs. Closest first. May be empty if instances
         *                  are accessed from the root. Non-null.
         * @return          XML query. Never null.
         */
        static String getQuery(String id, params String[] parentId)
        {
            //Debug.Assert(id != null : "id cannot be null";
            //Debug.Assert(parentId != null : "parentId cannot be null";

            String query = "<wells xmlns=\"" + WitsmlVersion.VERSION_1_4_0.getNamespace() + "\">" +
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
                           "    <pcInterest/>" + //BM ADD uom
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
                           "    <waterDepth uom=\"" + WitsmlServer.distUom+"\"/>" +
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

        /**
         * Parse the specified DOM element and instantiate the properties
         * of this instance.
         *
         * @param element  XML element to parse. Non-null.
         */
        void update(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

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
            operatorInterestShare = XmlUtil.update(element, "pcInterest", operatorInterestShare);
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

            XElement locationElement = element.Element(element.Name.Namespace+"wellLocation");//, element.getNamespace());
            if (locationElement != null)
                location = new WitsmlLocation(locationElement);

            XElement commonDataElement = element.Element(element.Name.Namespace+"commonData"); //, element.getNamespace());
            if (commonDataElement != null)
                commonData = new WitsmlCommonData(commonDataElement);
        }
    }
}