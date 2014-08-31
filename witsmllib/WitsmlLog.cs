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
namespace witsmllib
{

    //import java.util.ArrayList;
    //import java.util.Collections;
    //import java.util.Date;
    //import java.util.List;
    //import java.util.logging.Level;

    //import nwitsml.util.XmlUtil;

    /**
     * Java representation of a WITSML "log".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.Collections.Generic;
    using witsmllib;
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
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

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
                DateTime?  v = XmlUtil.getTime(indexString);
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

        /**
         * Get start index of this log.
         *
         * @return  Start index of this log.
         */
        public Object getStartIndex()
        {
            return startIndex;
        }

        /**
         * Get end index of this log.
         *
         * @return  End index of this log.
         */
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

        /**
         * Get comment of this log.
         * @see <a href="http://www.witsml.org">www.witsml.org</a>
         *
         * @return  Comment of this log.
         */
        public String getComment()
        {
            return comment;
        }

        /**
         * Get log curves of this log.
         * A copy is returned, to avoid locking problems (such as deadlock).
         * @see <a href="http://www.witsml.org">www.witsml.org</a>
         *
         * @return  Log curves of this log.
         */
        public List<WitsmlLogCurve> getCurves()
        {
            return curves; // Collections.unmodifiableList(curves);
        }

        /**
         * Get number of log curves of this log.
         * @see <a href="http://www.witsml.org">www.witsml.org</a>
         *
         * @return  Number of log curves of this log.
         */
        public int getNCurves()
        {
            return curves.Count; //.size();
        }

        /**
         * Return the index curve of this log.
         *
         * @return Index curve of this log (or null if not found).
         */
        public WitsmlLogCurve getIndexCurve()
        {
            return findCurve(indexCurveName);
        }

        /**
         * Find log curve with specified name.
         *
         * @param curveName  Name of curve to find.
         * @return   Requested curve (or null if not found).
         */
        public WitsmlLogCurve findCurve(String curveName)
        {
            foreach (WitsmlLogCurve logCurve in this.curves)
            {
                if (logCurve.getName().Equals(curveName))
                    return logCurve;
            }

            // Not found
            return null;
        }

        protected WitsmlLogCurve findCurve(int curveNo)
        {
            foreach (WitsmlLogCurve logCurve in this.curves)
            {
                if (logCurve.getCurveNo() == curveNo)
                    return logCurve;
            }

            // Not found
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