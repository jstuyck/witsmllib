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
namespace witsmllib.units
{

    ////import java.util.ArrayList;
    ////import java.util.Collections;
    ////import java.util.List;

    /**
     * Model a quantity (such as "length" or "acceleration") and its
     * associated units.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.Text;
    using System.Collections.Generic;
    sealed class WitsmlQuantity
    {

        /** Name of this quantity. Non-null. */
        private  String name;

        /** Optional description. May be null. */
        private  String description;

        /**
         * List of units for this quantity. Non-null.
         * The list may be empty, but if it's not, the first unit is
         * the base unit.
         */
        private  List<WitsmlUnit> units = new List<WitsmlUnit>();

        /**
         * Create a new quantity instance.
         *
         * @param name         Name of quantity, such as "length". Non-null.
         * @param description  Optional quantity. Never null.
         */
        internal WitsmlQuantity(String name, String description)
        {
            this.name = name;
            this.description = description;
        }

        /**
         * Return name of this quantity.
         *
         * @return  Name of this quantity. Never null.
         */
        internal String getName()
        {
            return name;
        }

        /**
         * Return description of this quantity.
         *
         * @return  Description of this quantity. Never null.
         */
        internal String getDescription()
        {
            return description;
        }

        /**
         * Return the units of this quantity.
         *
         * @return  Units of this quantity. Never null.
         */
        internal List<WitsmlUnit> getUnits()
        {
            return units; // Collections.unmodifiableList(units);
        }

        /**
         * Return the base unit of this quantity.
         *
         * @return  Base unit of this quantity, or null if no units has been added.
         */
        internal WitsmlUnit getBaseUnit()
        {
            return units.Count >0? units[0]: null; //.isEmpty() ? null : units.get(0);
        }

        /**
         * Associate the specified unit to with quantity.
         *
         * @param unit        Unit to add. Non-null.
         * @param isBaseUnit  True if this is the base unit, false otherwise.
         *                    If more than one unit is added as base unit, the
         *                    last one added will have this role. If no units are
         *                    added as base unit, the first unit added will have
         *                    this role.
         */
        internal void addUnit(WitsmlUnit unit, bool isBaseUnit)
        {
            //Debug.Assert(unit != null : "unit cannot be null";
            if (isBaseUnit)
                units.Insert(0, unit);
            else units.Add(unit); 
            //units.Add(isBaseUnit ? 0 : units.size(), unit);
        }

        /** {@inheritDoc} */
        
        public override String ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(name + "\n");
            s.Append(description + "\n");
            foreach (WitsmlUnit unit in units)
                s.Append("  " + unit + "\n");

            return s.ToString();
        }
    }
}