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
namespace witsmllib{

//import java.util.ArrayList;
//import java.util.Collections;
//import java.util.Date;
//import java.util.List;
//import java.text.ParseException;

//import nwitsml.util.ISO8601DateParser;

/**
 * Java representation of a WITSML "logCurve".
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Collections.Generic;
using witsmllib.util;
public abstract class WitsmlLogCurve {

    /** Parent log. Non-null. */
    protected  WitsmlLog log;

    private  String name; // mnemonic
    private  int curveNo; // columnIndex

    protected String id; // uid
    protected String mnemonic; // mnemAlias
    protected String quantity; // classPOSC (1.2) classWitsml (1.3+)
    protected Int32? classIndex; // classIndex (1.4)
    protected String unit; // unit
    protected String noValue; // nullValue
    protected Boolean? _isAlternateIndex; // alternateIndex
    protected Object startIndex;  // startIndex (1.2) minIndex/minDateTimeIndex (1.3+)
    protected Object endIndex;    // endIndex (1.2) maxIndex/maxDateTimeIndex (1.3+)
    protected String description; // curveDescription
    protected Value sensorOffset; //sensorOffset
    protected String dataSource; // dataSource
    protected Value dataDensity; // densData
    protected String traceState; // traceState
    protected String traceOrigin; // traceOrigin
    protected Type dataType; // Class<?> dataType; // typeLogData

    /** Curve values */
    protected  List<Object> values = new List<Object>(); // ArrayList<Object>();

    /**
     * Create a new log curve inside the specified log.
     *
     * @param log  Parent log of this curve.
     */
    protected WitsmlLogCurve(WitsmlLog log, String name, int curveNo) {
        //Debug.Assert(log != null : "log cannot be null";

        this.log = log;
        this.name = name;
        this.curveNo = curveNo;
    }

    /**
     * Convert the log curve with specified factor.
     *
     * @param conversionFactor Conversion factor.
     */
    internal void convert(double conversionFactor) {
        //if (!(dataType is Double))  
        if(! dataType.IsAssignableFrom(typeof(Double)))
            return;

        for (int i = 0; i < values.Count; i++) {

            if(values[i] is double)
                values[i] = ((double)values[i]) * conversionFactor ; 
            //Double val = (Double) values[i];
            //if (val != null && !Double.IsNaN(val.doubleValue())) {
            //    values.set(i, new Double(val.doubleValue() * conversionFactor));
            //}
        }
    }

    public WitsmlLog getLog() {
        return log;
    }

    public String getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public int getCurveNo() {
        return curveNo;
    }

    public String getUnit() {
        return unit;
    }

    /**
     * Set unit of this log curve.
     *
     * @param unit  Unit of this log curve.
     */
    internal void setUnit(String unit) {
        //synchronized 
        lock(values) {
            this.unit = unit;
        }
    }

    public String getNameAndUnit() {
        return name + (unit != null ? " [" + unit + "]" : "");
    }

    public String getQuantity() {
        return quantity;
    }

    public String getMnemonic() {
        return mnemonic;
    }

    public String getNoValue() {
        return noValue == null ? log.getNoValue() : noValue;
    }

    public Boolean? isAlternateIndex() {
        return _isAlternateIndex;
    }

    public Object getStartIndex() {
        return startIndex;
    }

    public Object getEndIndex() {
        return endIndex;
    }

    public String getDescription() {
        return description;
    }

    public Value getSensorOffset() {
        return sensorOffset;
    }

    public String getDataSource() {
        return dataSource;
    }

    public String getTraceState() {
        return traceState;
    }

    public String getTraceOrigin() {
        return traceOrigin;
    }

    //public Class<?> getDataType() {
    public Type getDataType(){
        return dataType;
    }

    //protected static Class<?> getDataType(String dataTypeString) {
    protected static Type getDataType(String dataTypeString) {
        dataTypeString = dataTypeString.ToLower();
        if (dataTypeString == null)
            return null;
        else if (dataTypeString.Equals("double"))
            return typeof(Double);//.class;
        else if (dataTypeString.Equals("integer"))
            return typeof(Int32);//.class;
        else if (dataTypeString.Equals("long"))
            return typeof(Int64); //.class;
        //  throw new NotImplementedException(); 
        else if (dataTypeString.Equals("string"))
            return typeof(String);//.class;
        else if (dataTypeString.Equals("datetime"))
            return typeof(DateTime); //Date.class;
        else if (dataTypeString.Equals("date time")) // 1.3+
            //throw new NotImplementedException();
            return typeof(DateTime); //bm - not sure why this was set to notimplexcep... should be datetime?
        //return Date.class;
        else
        {
            return null;
        }
    }

    public List<Object> getValues() {
       // return Collections.unmodifiableList(values);
        return values; 
    }

    /**
     * Get one specific value of this log curve.
     * @see <a href="http://www.witsml.org">www.witsml.org</a>
     *
     * @return  Specified value of this curve (or null of index
     *          is out of range).
     */
    public Object getValue(int index) {
        if (index < 0 || index >= values.Count)
            throw new  IndexOutOfRangeException("Invalid index: " + index);

        return values[index];//.get(index);
    }

    /**
     * Return the last value of this curve.
     *
     * @return  The last value of this curve. Null is returned if there are
     *          no values, or the last value happens to be null.
     */
    public Object getLastValue() {
        //return values.size() > 0 ? values.get(values.size() - 1) : null;
        return values.Count > 0? values[values.Count -1] : null; 
    }

    public Object[] getRange() {
        //
        // Numbers
        //

        //if (Number.class.isAssignableFrom(dataType)) {
        //    Number min = null;
        //    Number max = null;

        //    foreach (Object value in values) {
        //        if (value == null)
        //            continue;

        //        Number n = (Number) value;
        //        if (min == null || n.doubleValue() < min.doubleValue())
        //            min = n;
        //        if (max == null || n.doubleValue() > max.doubleValue())
        //            max = n;
        //    }

        if(typeof(Decimal).IsAssignableFrom(dataType)){
            Decimal? min = null, max = null; 
            foreach(Object value in values){
                if(value == null) continue; 
                Decimal? n = value as Decimal? ; 
                if(!min.HasValue || n.Value < min.Value ) min = n;
                if(!max.HasValue || n.Value > max.Value ) max = n;
            }
            return new Object[] {min, max};
        }

        //
        // Time
        //
        //else if (dataType == DateTime.class) {
        else if(dataType.IsAssignableFrom(typeof(DateTime))){
            DateTime?  min = null;
            DateTime?  max = null;

            foreach (Object value in values) {
                if (value == null)
                    continue;

                DateTime?  n = value as DateTime? ;
                if (min == null || n.Value  < min.Value)
                    min = n;
                if (max == null || n.Value > max.Value )
                    max = n;
            }

            return new Object[] {min, max};
        }

        return new Object[] {null, null};
    }

    protected void clear() {
        values.Clear();
    }

    protected void addValue(String token){ //throws WitsmlParseException {
        Object value = null;

        //
        // NoValue
        //
        if (token == null || token.Equals(getNoValue())) {
            // Leave value = null;
        }

        //
        // Double
        //
        //else if (dataType == Double.class) {
        else if (dataType==typeof(Double)){// is Double){
            try {
                value = String.IsNullOrEmpty(token) ? (Double?)null : Double.Parse(token); // new Double(token);
            }
            catch (FormatException exception){ // FormatException exception) {
                // TODO: Look at this. There is nothing that prohibits the noValue
                // to be a non-numeric string, though it probably isn't.

                // TODO 2: Neeeded to comment out the exception in due to cases
                // where the values consists of several space separated values.
                //throw new WitsmlParseException(token, exception);
            }
        }

        //
        // Date
        //
        else if (dataType==typeof(DateTime)){// is DateTime){// (dataType == Date.class) {
            try {
                value =String.IsNullOrEmpty(token)? (DateTime? )null: DateTime.Parse(token);// token.isEmpty() ? null : ISO8601DateParser.parse(token);
            }
            catch (FormatException exception) {// ParseException exception) {
                throw new WitsmlParseException(token, exception);
            }
        }

        //
        // Int32
        //
        else if (dataType == typeof(Int32)){// is Int32){ // == Int32.class) {
            try {
                value = String.IsNullOrEmpty( token)? (Int32?)null: Int32.Parse(token); //.isEmpty() ? null : new Int32(token);
            }
            catch (FormatException exception) {
                // TODO: Look at this. There is nothing that prohibits the noValue
                // to be a non-numeric string, though it probably isn't.
                throw new WitsmlParseException(token, exception);
            }
        }

        //
        // Long
        //
        else if (dataType==typeof(Int64)){ // == Long.class) {
            try {
                value = String.IsNullOrEmpty(token)? (Int64?) null  : Int64.Parse(token); // token.isEmpty() ? null : new Long(token);
            }
            catch (FormatException exception) {
                // TODO: Look at this. There is nothing that prohibits the noValue
                // to be a non-numeric string, though it probably isn't.
                throw new WitsmlParseException(token, exception);
            }
        }

        //
        // String
        //
        else if (dataType ==  typeof(string)){ // == String.class) {
            value = token;
        }

        values.Add(value);
    }

    /**
     * Get of this log curve.
     * @see <a href="http://www.witsml.org">www.witsml.org</a>
     *
     * @return  of this curve.
     */
    public int getNValues() {
        return values.Count ; //.size();
    }

    
    public override String ToString() {
        return "\n" + name; // WitsmlObject.ToString(this, 2);
    }
}

}