using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
{
    public abstract class WitsmlWellboreGeometrySection : WitsmlObject
    {
        private static String WITSML_TYPE = "wbGeometrySection";


        protected String _typeHoleCasing;    //Type of fixed component. ENUM
        protected Value _mdTop;              //Measured depth at Top of Interval.
        protected Value _mdBottom;           //Measured depth at bottom of the section.
        protected Value _tvdTop;             //True vertical depth at top of the section.
        protected Value _tvdBottom;          //True vertical depth at bottom of the section.
        protected Value _idSection;          //Inner diameter.
        protected Value _odSection;          //Outer diameter - Only for casings and risers.
        protected Value _wtPerLen;           //Weight per unit length for casing sections.
        protected String _grade;             //Material grade for the tubular section.
        protected Boolean? _curveConductor;  //Curved conductor? Values are "true" (or "1") and "false" (or "0").
        protected Value _diaDrift;           //Maximum diameter that can pass through.
        protected Value _factFric;           //Friction factor.
        protected Object _extensionNameValue;//cs_extensionNameValue

        protected WitsmlWellboreGeometrySection(WitsmlServer server,
                                                String id, String name,
                                                WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public String TypeHoleCasing()
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

        public Value GetIdSection()
        {
            return _idSection;
        }

        public Value GetOdSection()
        {
            return _odSection;
        }

    }
}
