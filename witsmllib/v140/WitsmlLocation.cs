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
     * Version specific implementation of the WitsmlLocation type.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using witsmllib.util;
    using System.Xml.Linq;
    sealed class WitsmlLocation : witsmllib.Location
    {

        /**
         * Create a WITSML location instance from the given
         * XML element node.
         *
         * @param element  commonData node. Non-null.
         */
        internal WitsmlLocation(XElement element)
        {
            //Debug.Assert(element != null : "element cannot be null";

            wellCRS = XmlUtil.update(element, "wellCRS", wellCRS);
            latitude = XmlUtil.update(element, "latitude", latitude);
            longitude = XmlUtil.update(element, "longitude", longitude);
            easting = XmlUtil.update(element, "easting", easting);
            northing = XmlUtil.update(element, "northing", northing);
            westing = XmlUtil.update(element, "westing", westing);
            southing = XmlUtil.update(element, "southing", westing);
            projectedX = XmlUtil.update(element, "projectedX", projectedX);
            projectedY = XmlUtil.update(element, "projectedY", projectedY);
            localX = XmlUtil.update(element, "localX", localX);
            localY = XmlUtil.update(element, "localY", localY);
            _isOriginal = XmlUtil.update(element, "original", _isOriginal);
            description = XmlUtil.update(element, "description", description);
        }

        /**
         * Return complete XML query for this type.
         *
         * @return  XML query. Never null.
         */
        public static String getQuery()
        {
            String query = "<wellCRS/>" +
                           "<latitude uom=\"rad\"/>" +
                           "<longitude uom=\"rad\"/>" +
                           "<easting uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<northing uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<westing uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<southing uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<projectedX uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<projectedY uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<localX uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<localY uom=\"" + WitsmlServer.distUom+"\"/>" +
                           "<original/>" +
                           "<description/>";

            return query;
        }
    }
}