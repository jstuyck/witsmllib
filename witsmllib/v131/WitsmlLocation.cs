using System;
using System.Xml.Linq;

using witsmllib.util;


namespace witsmllib.v131
{
    /// <summary>
    /// Version specific implementation of the WitsmlLocation type.
    /// </summary>
    public sealed class WitsmlLocation : witsmllib.Location
    {

        /// <summary>
        /// Create a WITSML location instance from the given XML element node.
        /// </summary>
        /// <param name="element">commonData node. Non-null.</param>
        public WitsmlLocation(XElement element)
        {
            if (element == null)
                throw new ArgumentException("element cannot be null");

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

        /// <summary>
        /// Return complete XML query for this type.
        /// </summary>
        /// <returns>XML Query. Never null.</returns>
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