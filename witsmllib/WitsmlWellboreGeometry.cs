using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
{
    /// <summary>
    /// Captures information about the configuration of the permanently
    /// installed components in a wellbore. It does not define the transient
    /// drilling strings (see the tubular data-object) or the hanging
    /// production components. This data-object is uniquely identified
    /// within the context of one wellbore data-object.
    /// </summary>
    public abstract class WitsmlWellboreGeometry : WitsmlObject
    {
        private static String WITSML_TYPE = "wbGeometry";

        private DateTime? _dTimReport;     //Time report generated.
        private Value _mdBottom;           //Measured depth at bottom.
        private Value _gapAir;             //Air gap.
        private Value _depthWaterMean;     //Water depth.
        private WitsmlWellboreGeometrySection _wbGeometrySection; //Wellbore geometry section object.

        protected WitsmlWellboreGeometry(WitsmlServer server,
                                      String id, String name,
                                      WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        /// <summary>
        /// Get the Time when the report was generated.
        /// </summary>
        protected DateTime? DTimReport
        {
            get { return _dTimReport; }
        }

        /// <summary>
        /// Get the measured depth at bottom.
        /// </summary>
        public Value MdBottom
        {
            get { return _mdBottom; }
        }

        /// <summary>
        /// Get the Air gap.
        /// </summary>
        public Value GepAir
        {
            get { return _gapAir; }
        }

        /// <summary>
        /// Get the water depth.
        /// </summary>
        public Value DepthWaterMean
        {
            get { return _depthWaterMean; }
        }

        /// <summary>
        /// Get the Wellbore geometry section object.
        /// </summary>
        public WitsmlWellboreGeometrySection WbGeometrySection
        {
            get { return _wbGeometrySection; }
        }


     
    }
}
