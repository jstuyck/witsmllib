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
namespace witsmllib{

/**
 * Model a property value with a corresponding unit.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
public sealed class Value : IComparable<Value> { // Comparable<Value> {

    /** Value of this instance. May be null to indicate absent. */
    private  Double? value;

    /** Unit of this instance. May be null indicating unitless or unknown. */
    private  String unit;

    /**
     * Create a new immutable value with unit instance.
     *
     * @param value  Value of this instance.
     *               May be null to indicate absent.
     * @param unit   Unit of this instance. May be null to indicate
     *               unitless or unknown.
     */
    public Value(Double value, String unit) {
        this.value = value;
        this.unit = unit;
    }

    /**
     * Return value of this instance.
     *
     * @return  Value of this instance. May be null indicating absent.
     */
    public Double? getValue() {
        return value;
    }

    /**
     * Return unit of this instance.
     *
     * @return  Unit of this instance. May be null indicating unitless
     *          or unknown.
     */
    public String getUnit() {
        return unit;
    }

    /**
     * Compare this value to the specified value. Ordering follows
     * the natural order of the number of the instances. Instances
     * of null value are ordered after those without null.
     *
     * @param v  Value to compare to. Non-null.
     * @return   A negative integer, zero, or a positive integer as
     *           this object is less than,
     *           equal to, or greater than the specified object.
     * @throws IllegalArgumentEAxception  If v is null.
     */
    public int CompareTo(Value v) {
        Double v1 = value.HasValue ? value.Value  : Double.MaxValue; //.MAX_VALUE;
        Double v2 = v.value.HasValue ? v.value.Value  : Double.MaxValue ; //.MAX_VALUE;

        return v1.CompareTo(v2 ); //.compareTo(v2);
    }

    /**
     * Return a string representation of this instance.
     *
     * @return  A string representation of this instance. Never null.
     */
    
    public override String ToString() {
        return value + " [" + unit + "]";
    }
}
}