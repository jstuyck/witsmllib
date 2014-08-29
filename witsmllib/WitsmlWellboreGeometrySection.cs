using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
{
    class WitsmlWellboreGeometrySection : WitsmlObject
    {
        private static String WITSML_TYPE = "wbGeometrySection";


        protected String typeHoleCasing;    //Type of fixed component. ENUM

        protected Value mdTop;              //Measured depth at Top of Interval.
        protected Value mdBottom;           //Measured depth at bottom of the section.
        protected Value tvdTop;             //True vertical depth at top of the section.
        protected Value tvdBottom;          //True vertical depth at bottom of the section.
        protected Value idSection;          //Inner diameter.
        protected Value odSection;          //Outer diameter - Only for casings and risers.
        protected Value wtPerLen;           //Weight per unit length for casing sections.
        protected String grade;             //Material grade for the tubular section.

        protected Boolean? curveConductor;  //Curved conductor? Values are "true" (or "1") and "false" (or "0").
        protected Value diaDrift;           //Maximum diameter that can pass through.
        protected Value factFric;           //Friction factor.
        protected Object extensionNameValue;//cs_extensionNameValue

        protected WitsmlWellboreGeometrySection(WitsmlServer server,
                                                String id, String name,
                                                WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

    }
}
