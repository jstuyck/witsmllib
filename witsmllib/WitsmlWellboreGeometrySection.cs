using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
{
    public abstract class WitsmlWellboreGeometrySection : WitsmlObject
    {
        private static String WITSML_TYPE = "wbGeometrySection";


        protected CasingType _typeHoleCasing;    //Type of fixed component. ENUM
        protected Value _mdTop;              //Measured depth at Top of Interval.
        protected Value _mdBottom;           //Measured depth at bottom of the section.
        protected Value _tvdTop;             //True vertical depth at top of the section.
        protected Value _tvdBottom;          //True vertical depth at bottom of the section.
        protected Value _idSection;          //Inner diameter.
        protected Value _odSection;          //Outer diameter - Only for casings and risers.
        protected Value _wtPerLen;           //Weight per unit length for casing sections.
        protected string _grade;             //Material grade for the tubular section.
        protected bool? _curveConductor;     //Curved conductor? Values are "true" (or "1") and "false" (or "0").
        protected Value _diaDrift;           //Maximum diameter that can pass through.
        protected Value _factFric;           //Friction factor.
        protected Object _extensionNameValue;//cs_extensionNameValue

        protected WitsmlWellboreGeometrySection(WitsmlServer server,
                                                String id, String name,
                                                WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        /// <summary>
        /// Get the type of fixed component
        /// </summary>
        /// <returns>The type of fixed component</returns>
        public CasingType getCasingType()
        {
            return _typeHoleCasing;
        }

        public Value GetMdTop()
        {
            return _mdTop;
        }

        public Value GetMdBottom()
        {
            return _mdBottom;
        }

        public Value GetTvdTop()
        {
            return _tvdTop;
        }

        public Value GetTvdBottom()
        {
            return _tvdBottom;
        }

        /// <summary>
        /// Get the inner diameter of the section.
        /// </summary>
        /// <returns>Inner diameter of the section.</returns>
        public Value getInnerDiameter()
        {
            return _idSection;
        }

        public Value getOuterDiameter()
        {
            return _odSection;
        }

        /// <summary>
        /// Get the friction Facetor of the section
        /// </summary>
        /// <returns>The friction Facetor of the section</returns>
        public Value getFrictionFactor()
        {
            return _factFric;
        }

        /// <summary>
        /// Get the material Grade of the section.
        /// </summary>
        /// <returns>The material Grade of the section.</returns>
        public string getMaterialGrade()
        {
            return _grade;
        }

        /// <summary>
        /// Get the Maximum diameter that can pass through the section.
        /// </summary>
        /// <returns>Maximum diameter that can pass through the section.</returns>
        public Value getMaxDiameter()
        {
            return _diaDrift;
        }

        /// <summary>
        /// Get the Weight per unit length of the section.
        /// </summary>
        /// <returns>The Weight per unit length of the section.</returns>
        public Value getWeightPerLength()
        {
            return _wtPerLen;
        }

        /// <summary>
        /// Get if it's a Curved conductor 
        /// Values are "true" (or "1") and "false" (or "0").
        /// </summary>
        /// <returns></returns>
        public bool? isCurvedConductor()
        {
            return _curveConductor;
        }

        /// <summary>
        /// Set the type of fixed component
        /// </summary>
        /// <param name="casingType">The type of fixed component</param>
        public void setCasingType(CasingType casingType)
        {
            _typeHoleCasing = casingType;
        }

        /// <summary>
        /// Set if it's a Curved conductor 
        /// </summary>
        /// <param name="isCurvedConductor">Is it a Curved conductor?</param>
        public void setCurvedConductor(bool isCurvedConductor)
        {
            _curveConductor = isCurvedConductor;
        }

        /// <summary>
        /// Set the friction factor of the section
        /// </summary>
        /// <param name="frictionFactor">The friction factor of the section</param>
        public void setFrictionFactor(Value frictionFactor)
        {
            _factFric = frictionFactor;
        }

        /// <summary>
        /// Set the inner diameter of the section.
        /// </summary>
        /// <param name="innerDiameter">The inner diameter of the section.</param>
        public  void setInnerDiameter(Value innerDiameter)
        {
            _idSection = innerDiameter;
        }

        /// <summary>
        /// Set the maximum diameter of the section.
        /// </summary>
        /// <param name="maxDiameter">The maximum diameter of the section.</param>
        public void setMaxDiameter(Value maxDiameter)
        {
            _diaDrift = maxDiameter;
        }

        public void setMdBottom(Value mdBottom)
        {
            _mdBottom = mdBottom;
        }

        public void setMdTop(Value mdTop)
        {
            _mdTop = mdTop;
        }

        public void setOuterDiameter(Value outerDiameter)
        {
            _odSection = outerDiameter;
        }

        public void setTvdBottom(Value tvdBottom)
        {
            _tvdBottom = tvdBottom;
        }

        public void setTvdTop(Value tvdTop)
        {
            _tvdTop = tvdTop;
        }

        public void setWeightPerLength(Value weightPerLength)
        {
            _wtPerLen = weightPerLength;
        }
    }

    public enum CasingType
    {
        BLOW_OUT_PREVENTER,
        CASING,
        CONDUCTOR,
        CURVED_CONDUCTOR,
        LINER,
        OPEN_HOLE,
        RISER,
        TUBING,
        UNKNOWN
    }
}
