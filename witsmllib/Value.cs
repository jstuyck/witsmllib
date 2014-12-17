using System;

namespace witsmllib
{

    public sealed class Value : IComparable<Value>
    {
        private Double? value;
        private String unit;

        /// <summary>
        /// Create a new immutable value with unit instance. 
        /// </summary>
        /// <param name="value">Value of this instance. May be null to indicate absent.</param>
        /// <param name="unit">Unit of this instance. May be null to indicate unitless or unknown.</param>
        public Value(Double value, String unit)
        {
            this.value = value;
            this.unit = unit;
        }

        /// <summary>
        /// Return value of this instance. 
        /// </summary>
        /// <returns>Value of this instance. May be null indicating absent.</returns>
        public Double? getValue()
        {
            return value;
        }

        
        /// <summary>
        /// Return unit of this instance. 
        /// </summary>
        /// <returns>Unit of this instance. May be null indicating unitless or unknown.</returns>
        public String getUnit()
        {
            return unit;
        }

        /// <summary>
        /// Compare this value to the specified value. 
        /// Ordering follows the natural order of the number of the instances. 
        /// Instances of null value are ordered after those without null.
        /// </summary>
        /// <param name="v">Value to compare to. Non-null.</param>
        /// <returns>A negative integer, zero, or a positive integer as this object is less than,
        /// qual to, or greater than the specified object.</returns>
        public int CompareTo(Value v)
        {
            if (v == null)
                throw new ArgumentNullException("v cannot be null.");
            Double v1 = value.HasValue ? value.Value : Double.MaxValue;
            Double v2 = v.value.HasValue ? v.value.Value : Double.MaxValue; 

            return v1.CompareTo(v2); 
        }

       
        /// <summary>
        /// Return a string representation of this instance. 
        /// </summary>
        /// <returns>A string representation of this instance. Never null.</returns>
        public override String ToString()
        {
            return value + " [" + unit + "]";
        }
    }
}