using System;
using System.Collections.Generic;
using System.Linq;
using witsmllib;
using witsmllib.util;

namespace witsmllib
{
    public class WitsmlLog : WitsmlObject
    {

        /** The WitsML type name */
        private static String WITSML_TYPE = "log";

        protected List<WitsmlLogCurve> curves = new List<WitsmlLogCurve>();

        protected Boolean? _isGrowing; // objectGrowing
        protected Int32? nRows; // dataRowCount
        protected String serviceCompany; // serviceCompany
        protected String runNumber; // runNumber
        protected Int32? bhaRunNumber; // bhaRunNumber
        protected String pass; // pass
        protected DateTime? creationTime; // creationDate
        protected String description; // description
        protected char /*String*/ dataDelimiter = ',';// ","; // dataDelimiter (1.4)
        protected String indexType; // indexType
        protected Object startIndex; // startIndex (startDateTimeIndex 1.3+)
        protected Object endIndex; // endIndex (endDateTimeIndex 1.3+)
        protected String direction; // direction
        protected String indexCurveName; // indexCurve
        protected Value stepIncrement; // stepIncrement
        protected String indexUnit; // indexUnits (1.2)
        protected String noValue; // nullValue
        protected String unitNamingSystem; // uomNamingSystem (1.2)
        protected String comment; // otherData

        protected WitsmlLog(WitsmlServer server, String id, String name,
                            WitsmlObject parent, String parentId)

            : base(server, WITSML_TYPE, id, name, parent, parentId)
        { }

        public Boolean? isGrowing()
        {
            return _isGrowing;
        }

        public Int32? getNRows()
        {
            return nRows;
        }

        /**
         * Get service company of this log.
         *
         * @return  Service company of this log.
         */
        public String getServiceCompany()
        {
            return serviceCompany;
        }

        /**
         * Get run number of this log.
         *
         * @return  Run number of this log.
         */
        public String getRunNumber()
        {
            return runNumber;
        }

        public Int32? getBhaRunNumber()
        {
            return bhaRunNumber;
        }

        public String getPass()
        {
            return pass;
        }

        public DateTime? getCreationTime()
        {
            return creationTime; // creationTime != null ? new DateTime(creationTime.getTime()) : null;
        }

        public String getDescription()
        {
            return description;
        }

        public String getDataDelimiter()
        {
            return dataDelimiter.ToString();
        }

        public String getIndexType()
        {
            return indexType;
        }

        /**
         * TODO: Look at the visibility of this function.
         *
         * Get index object from specified index string and internal index type.
         *
         * @param indexString  Index as a string. May be null.
         * @return             Index as an object, converted according to the
         *                     indexType of this log.
         */
        public Object getIndex(String indexString)
        {
            //Debug.Assert(indexType != null;

            if (indexString == null)
                return null;

            //
            // Date
            //
            if (indexType.ToLower().Contains("time"))
            {
                DateTime? v = XmlUtil.getTime(indexString);
                if (v != null)
                    return v;
            }

            //
            // Date
            //
            else
            {
                try
                {
                    //double v = 
                    return Double.Parse(indexString);
                    //return Double.valueOf(v);
                }
                catch (FormatException exception)
                {
                    // TODO: Throw parse exception
                }
            }

            // Other
            return indexString;
        }

        /// <summary>
        /// Return the start index of this log. 
        /// 
        /// When the log header defines the direction as "Increasing",
        /// the startIndex is the starting (minimum) index value at which the first valid data point is located.
        /// When the log header defines the direction as "Decreasing", 
        /// the startIndex is the starting (maximum) index value at which the first valid data point is located. 
        /// </summary>
        /// <returns>The start index of this log. May be null if absent or unknown.</returns>
        public Object getStartIndex()
        {
            return startIndex;
        }

        /// <summary>
        /// Return the end index of this log. 
        /// 
        /// When the log header defines the direction as "Increasing", 
        /// the endIndex is the ending (maximum) index value at which the last valid data point is located. 
        /// When the log header defines the direction as Decreasing,
        /// the endIndex is the ending (minimum) index value at which the last valid data point is located. 
        /// </summary>
        /// <returns>The end index of this log. May be null if absent or unknown.</returns>
        public Object getEndIndex()
        {
            return endIndex;
        }

        public String getDirection()
        {
            return direction;
        }

        public Value getStepIncrement()
        {
            return stepIncrement;
        }

        public String getIndexUnit()
        {
            return indexUnit;
        }

        public String getNoValue()
        {
            return noValue;
        }

        public String getUnitNamingSystem()
        {
            return unitNamingSystem;
        }

        /// <summary>
        /// Get comment of this log.
        /// </summary>
        /// <returns>Comment of this log.</returns>
        public String getComment()
        {
            return comment;
        }

        /// <summary>
        /// Return the curves of this log. In the returned list the curves are organized by their curveNo (columnIndex), lowest index first. 
        /// </summary>
        /// <returns>The curves of this log. Never null.</returns>
        public List<WitsmlLogCurve> getCurves()
        {
            return curves; // Collections.unmodifiableList(curves);
        }

        /// <summary>
        /// Return the number of curves of this log. 
        /// </summary>
        /// <returns>Number of log curves of this log. [>=0].</returns>
        public int getNCurves()
        {
            return curves.Count; //.size();
        }

        /// <summary>
        /// The mnemonic of the index curve plus the column index. 
        /// 
        /// A column index of zero indicates an implied trace whose
        /// values start at startIndex and increment by stepIncrement for each row. 
        /// </summary>
        /// <returns>The index curve name of this log. May be null if absent or unknown.</returns>
        public WitsmlLogCurve getIndexCurve()
        {
            return findCurve(indexCurveName);
        }

        /// <summary>
        /// Find log curve with specified name. 
        /// </summary>
        /// <param name="curveName">Name of curve to find. </param>
        /// <returns>Requested curve (or null if not found).</returns>
        public WitsmlLogCurve findCurve(String curveName)
        {
            return this.curves.Where(x => x.getName().Equals(curveName)).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveNo"></param>
        /// <returns></returns>
        protected WitsmlLogCurve findCurve(int curveNo)
        {

            foreach (WitsmlLogCurve logCurve in this.curves)
            {
                if (logCurve.getCurveNo() == curveNo)
                    return logCurve;
            }

          
            return null;
        }

        /**
         * Do unit conversion on all log curves in an attempt to get them into SI.
         *
         * A hack to get values of some known measures into the SI
         * unit of measurement.
         * Conversion factors are deliberately listed explicitly here
         * to keep the hack local to these few lines of code.
         *
         * TODO I: Use a proper unit conversion system to handle this.
         * TODO II: Add more units.
         */
        protected void unitConvert()
        {
            foreach (WitsmlLogCurve curve in this.curves)
            {
                String unit = curve.getUnit();
                if (unit == null)
                {
                    continue;
                }
                // Assume this is atomic since we hold the lock on the list
                if (unit.Equals("ft"))
                {
                    curve.convert(0.3048); // ft -> m
                    curve.setUnit("m");
                }
                else if (unit.Equals("dega"))
                { // deg -> rad
                    curve.convert(0.017453292);
                    curve.setUnit("rad");
                }
            }
        }

        /**
         * Guess if this is a MD log or not.
         *
         * WitsML does not offer an unambiguous way to tell if a log is
         * MD, TVD, time or something else, so we have to make an educated
         * guess based on the name of the index curve.
         *
         * @return True if this is an MD log.
         */
        public bool isMdLog()
        {
            if ("dept".Equals(this.indexCurveName, StringComparison.OrdinalIgnoreCase))
                return true;

            if ("md".Equals(this.indexCurveName, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        // Add any curves reported in identification, but not present during
        // instantiation
        protected void insertMissingCurves(WitsmlLogCurve[] curveArray)
        {
            for (int i = 0; i < curveArray.Length; i++)
            {
                if (curveArray[i] == null)
                {
                    foreach (WitsmlLogCurve curve in curves)
                    {
                        bool isFound = false;
                        for (int j = 0; j < curveArray.Length; j++)
                        {
                            if (curveArray[j] == curve)
                            {
                                isFound = true;
                                break;
                            }
                        }

                        if (!isFound)
                        {
                            curveArray[i] = curve;
                            break;
                        }
                    }
                }
            }
        }
    }
}