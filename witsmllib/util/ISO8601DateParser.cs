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
using System.Text;
using System;
using System.Globalization;
namespace witsmllib.util
{

    //import java.util.Calendar;
    //import java.util.Date;
    //import java.util.GregorianCalendar;
    //import java.util.NoSuchElementException;
    //import java.util.StringTokenizer;
    //import java.util.TimeZone;
    //import java.text.ParseException;

    /**
     * Date parser for the ISO 8601 format.
     * See <a href="http://www.w3.org/TR/xmlschema-2/#dateTime">
     * http://www.w3.org/TR/xmlschema-2/#dateTime</a>
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public sealed class ISO8601DateParser
    {

        /**
         * Private constroctor to avoid client instantiation.
         */
        private ISO8601DateParser()
        {
            // //Debug.Assert(false : "This constructor should never be called";
            //Debug.Asset(false,"This constructor should never be called"
        }

        /**
         * Parse the given string in ISO 8601 format and build a Date object.
         *
         * @param  isoDate the date in ISO 8601 format. Non-null.
         * @return A date instance. Never null.
         * @throws ArgumentException  If isoDate is null.
         * @throws ParseException If the supplied string is not valid according
         *                        to ISO8601
         */
        public static DateTime? parse(String isoDate)
        { //throws ParseException {
            if (isoDate == null)
                throw new ArgumentNullException("isoDate cannot be null");
            return DateTime.Parse(isoDate); 

            //Calendar calendar = getCalendar(isoDate);
            //return calendar.getTime();
        }

        /**
         * Generate a ISO 8601 string representation of the specified date.
         *
         * @param date The date to create string representation of. Non-null.
         * @return     String representing the date in the ISO 8601 format.
         *             Never null.
         * @throws ArgumentException  If date is null.
         */
        public static String ToString(DateTime? date)
        {
            if (date == null)
                throw new ArgumentNullException("date cannot be null");

           // StringBuilder isoDate = new StringBuilder(); 
            DateTime cal = date.Value.ToUniversalTime(); //.ToUniversalTime();
            return String.Format("{0,4:0000}-{1,2:00}-{2,2:00}T{3,2:00}:{4,2:00}:{5,2:00}.{6,3:000}Z", cal.Year, cal.Month, cal.Day, cal.Hour, cal.Minute, cal.Second, cal.Millisecond);// / 10);

            //// Convert to UTC
            //Calendar calendar = new GregorianCalendar(TimeZone.getTimeZone("UTC"));
            //calendar.setTime(date);

            //// Write to string
            //StringBuilder isoDate = new StringBuilder();
            //isoDate.Append(calendar.get(Calendar.YEAR));
            //isoDate.Append("-");
            //isoDate.Append(ToString(calendar.get(Calendar.MONTH) + 1));
            //isoDate.Append("-");
            //isoDate.Append(ToString(calendar.get(Calendar.DAY_OF_MONTH)));
            //isoDate.Append("T");
            //isoDate.Append(ToString(calendar.get(Calendar.HOUR_OF_DAY)));
            //isoDate.Append(":");
            //isoDate.Append(ToString(calendar.get(Calendar.MINUTE)));
            //isoDate.Append(":");
            //isoDate.Append(ToString(calendar.get(Calendar.SECOND)));
            //isoDate.Append(".");
            //isoDate.Append(ToString(calendar.get(Calendar.MILLISECOND) / 10));
            //isoDate.Append("Z");

            //return isoDate.ToString();
        }

        //private static bool check(StringTokenizer tokenizer, String token)
        //{ //throws ParseException {
        //    try
        //    {
        //        if (tokenizer.nextToken().Equals(token))
        //            return true;

        //        throw new FormatException("Missing [" + token + "]", 0);
        //    }
        //    catch (NoSuchElementException exception)
        //    {
        //        return false;
        //    }
        //}

        private static /* Calendar */ DateTime? getCalendar(String isoDate)
        { //throws ParseException {
            // YYYY-MM-DDThh:mm:ss.sTZD
         //   StringTokenizer tokenizer = new StringTokenizer(isoDate, "-T:.+Z", true);

            //try
            //{
                return DateTime.Parse(isoDate);
            //}
            //catch (FormatException exception)
            //{

            //    throw new  ParseException(exception.getMessage(), 0);
            //}


            //var tokenizer = isoDate.Split('-', 'T', ':', '.', '+', 'Z');
            //new Calendar(


            //Calendar calendar = new GregorianCalendar();
            //calendar.clear();

            //try
            //{

            //    // Year
            //    if (tokenizer.hasMoreTokens())
            //    {
            //        int year = Int32.parseInt(tokenizer.nextToken());
            //        calendar.set(Calendar.YEAR, year);
            //    }
            //    else
            //    {
            //        return calendar;
            //    }

            //    // Month
            //    if (check(tokenizer, "-") && (tokenizer.hasMoreTokens()))
            //    {
            //        int month = Int32.parseInt(tokenizer.nextToken()) - 1;
            //        calendar.set(Calendar.MONTH, month);
            //    }
            //    else
            //    {
            //        return calendar;
            //    }

            //    // Day
            //    if (check(tokenizer, "-") && (tokenizer.hasMoreTokens()))
            //    {
            //        int day = Int32.parseInt(tokenizer.nextToken());
            //        calendar.set(Calendar.DAY_OF_MONTH, day);
            //    }
            //    else
            //    {
            //        return calendar;
            //    }

            //    // Hour
            //    if (check(tokenizer, "T") && (tokenizer.hasMoreTokens()))
            //    {
            //        int hour = Int32.parseInt(tokenizer.nextToken());
            //        calendar.set(Calendar.HOUR_OF_DAY, hour);
            //    }
            //    else
            //    {
            //        calendar.set(Calendar.HOUR_OF_DAY, 0);
            //        calendar.set(Calendar.MINUTE, 0);
            //        calendar.set(Calendar.SECOND, 0);
            //        calendar.set(Calendar.MILLISECOND, 0);
            //        return calendar;
            //    }

            //    // Minutes
            //    if (check(tokenizer, ":") && (tokenizer.hasMoreTokens()))
            //    {
            //        int minutes = Int32.parseInt(tokenizer.nextToken());
            //        calendar.set(Calendar.MINUTE, minutes);
            //    }
            //    else
            //    {
            //        calendar.set(Calendar.MINUTE, 0);
            //        calendar.set(Calendar.SECOND, 0);
            //        calendar.set(Calendar.MILLISECOND, 0);
            //        return calendar;
            //    }

            //    //
            //    // Not mandatory now
            //    //

            //    // Secondes
            //    if (!tokenizer.hasMoreTokens())
            //    {
            //        return calendar;
            //    }

            //    String token = tokenizer.nextToken();
            //    if (token.Equals(":"))
            //    { // secondes
            //        if (tokenizer.hasMoreTokens())
            //        {
            //            int secondes = Int32.parseInt(tokenizer.nextToken());
            //            calendar.set(Calendar.SECOND, secondes);
            //            if (!tokenizer.hasMoreTokens())
            //            {
            //                return calendar;
            //            }

            //            // Milliseconds
            //            token = tokenizer.nextToken();
            //            if (token.Equals("."))
            //            {
            //                // bug fixed, thx to Martin Bottcher
            //                String nt = tokenizer.nextToken();
            //                while (nt.Length() < 3)
            //                {
            //                    nt += "0";
            //                }
            //                nt = nt.substring(0, 3); // Cut trailing characters
            //                int nMilliseconds = Int32.parseInt(nt);
            //                calendar.set(Calendar.MILLISECOND, nMilliseconds);
            //                if (!tokenizer.hasMoreTokens())
            //                {
            //                    return calendar;
            //                }
            //                token = tokenizer.nextToken();
            //            }
            //            else
            //            {
            //                calendar.set(Calendar.MILLISECOND, 0);
            //            }
            //        }
            //        else
            //        {
            //            throw new ParseException("No seconds specified", 0);
            //        }
            //    }
            //    else
            //    {
            //        calendar.set(Calendar.SECOND, 0);
            //        calendar.set(Calendar.MILLISECOND, 0);
            //    }

            //    // Timezone
            //    if (token.Equals("Z"))
            //    {
            //        calendar.setTimeZone(TimeZone.getTimeZone("UTC"));
            //    }
            //    else
            //    {
            //        StringBuilder s = new StringBuilder("GMT" + token);
            //        while (tokenizer.hasMoreTokens())
            //            s.Append(tokenizer.nextToken());

            //        TimeZone timeZone = TimeZone.getTimeZone(s.ToString());
            //        calendar.setTimeZone(timeZone);

            //        /*

            //        if (!(token.Equals("+") || token.Equals("-")))
            //            throw new ParseException("only + or - allowed", 0);

            //        bool isPlus = token.Equals("+");
            //        if (!tokenizer.hasMoreTokens())
            //            throw new ParseException("Missing hour field", 0);

            //        int tzhour = Int32.parseInt(tokenizer.nextToken());
            //        int tzmin  = 0;

            //        if (check(tokenizer, ":") && (tokenizer.hasMoreTokens()))
            //            tzmin = Int32.parseInt(tokenizer.nextToken());
            //        else
            //            throw new ParseException("Missing minute field", 0);

            //        if (isPlus) {
            //            calendar.Add(Calendar.HOUR, tzhour);
            //            calendar.Add(Calendar.MINUTE, tzmin);
            //        }
            //        else {  // minus
            //            calendar.Add(Calendar.HOUR, -tzhour);
            //            calendar.Add(Calendar.MINUTE, -tzmin);
            //        }
            //        */
            //    }
            //}
            //catch (FormatException exception)
            //{
            //    throw new ParseException(exception.getMessage(), 0);
            //}

            //return calendar;
        }

        private static String ToString(int value)
        {
            return value >= 0 && value < 10 ? "0" + value : "" + value;
        }
    }
}