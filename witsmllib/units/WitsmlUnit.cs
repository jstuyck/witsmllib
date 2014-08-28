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
using System;
namespace witsmllib.units
{

    /**
     * Model a unit, such as "feet" and how values converts
     * to the base unit of the same quantity.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public sealed class WitsmlUnit
    {
        /** Name of unit such as "meter". Non-null. */
        private  String name;

        /** Symbol of unit such as "m". Non-null. */
        private  String symbol;

        /** Conversion factor a for converting to base unit. */
        private  double a;

        /** Conversion factor b for converting to base unit. */
        private  double b;

        /** Conversion factor c for converting to base unit. */
        private  double c;

        /** Conversion factor d for converting to base unit. */
        private  double d;

        /**
         * Create a new unit.
         *
         * The conversion consists of four factors that are applied as
         * follows when v is to be converted to base unit:
         *
         * <pre>
         *    base = (a * value + b) / (c * value + d);
         * </pre>
         *
         * For most conversion only a is needed. In these cases b, c and d
         * should be set to 0.0, 0.0 and 1.0 respectively. For units like
         * temperature a shift (b) is used as well, while c and d will almost
         * never be used.
         *
         * @param name   Name of unit such as "meter". Non-null.
         * @param symbol Symbol of unit such as "m". Non-null.
         * @param a      Conversion factor a for converting to base unit.
         * @param b      Conversion factor b for converting to base unit.
         * @param c      Conversion factor c for converting to base unit.
         * @param d      Conversion factor d for converting to base unit.
         */
        internal WitsmlUnit(String name, String symbol,
                   double a, double b, double c, double d)
        {
            if (name == null)
                throw new ArgumentException("name cannot be null");

            if (symbol == null)
                throw new ArgumentException("symbol cannot be null");

            this.name = name;
            this.symbol = symbol;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        /**
         * Return name of this unit.
         *
         * @return  Name of this unit. Never null.
         */
        public String getName()
        {
            return name;
        }

        /**
         * Return symbol of this unit.
         *
         * @return  Symbol of this unit. Never null.
         */
        public String getSymbol()
        {
            return symbol;
        }

        /**
         * Convert the specified value to base unit in the quantity of this unit.
         *
         * @param value  Value to convert.
         * @return       Value converted to base unit.
         */
        public double toBase(double value)
        {
            double baseValue = (a * value + b) / (c * value + d);
            return baseValue;
        }

        /**
         * Convert the specified value given in base unit to this unit.
         *
         * @param baseValue  Base value to convert.
         * @return           Value converted to this unit.
         */
        public double fromBase(double baseValue)
        {
            double value = (d * baseValue - b) / (a - c * baseValue);
            return value;
        }

        /** {@inheritDoc} */
        
        public override String ToString()
        {
            return name + " [" + symbol + "]";
        }
    }
}