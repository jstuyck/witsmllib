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

    //import java.text.DateFormat;
    //import java.text.SimpleDateFormat;
    //import java.util.Date;

    /**
     * Meta-information about a WITSML request.
     *
     * @see WitsmlAccessListener
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.Text;
    public class WitsmlAccessEvent
    {
        /** The server being accessed. Non-null. */
        private  WitsmlServer witsmlServer;

        /** The WSDL function called. Non-null. */
        private  String wsdlFunction;

        /** The WITSML type accessed. May be null if N/A. */
        private  String witsmlType;

        /** Time of request. Milliseconds since the epoch. */
        private  long requestTime;

        /** The request string. Null if N/A. */
        private  String request;

        /** The response string. Null if N/A or the operation failed. */
        private  String response;

        /** The WSDL function status code. Null if N/A. */
        private  Int32? statusCode;

        /** Server message. Null if N/A or not supplied. */
        private  String serverMessage;

        /** Server response time in milliseconds. */
        private  long responseTime;

        /** The exception thrown. Null if operation was OK. */
        private  Exception /*Throwable*/ throwable;

        /**
         * Create a WITSML response information instance.
         *
         * @param requestTime   Time of request in milliseconds since the epoch.
         * @param requestXml    The request XML. Non-null.
         * @param responseXml   The response XML. May be null.
         * @param throwable     Throwable if one was thrown, or null if not.
         */
        internal WitsmlAccessEvent(WitsmlServer witsmlServer,
                          String wsdlFunction, String witsmlType,
                          long requestTime,
                          String request,
                          String response,
                          Int32? statusCode,
                          String serverMessage,
                          Exception throwable) //Throwable throwable)
        {
            //Debug.Assert(witsmlServer != null : "witsmlServer cannot be null";
            //Debug.Assert(wsdlFunction != null : "wsdlFunction cannot be null";
            //Debug.Assert(requestTime <= System.currentTimeMillis() : "Request time in the future: " + requestTime;

            this.witsmlServer = witsmlServer;
            this.wsdlFunction = wsdlFunction;
            this.witsmlType = witsmlType;
            this.requestTime = requestTime;
            this.request = request;
            this.response = response;
            this.statusCode = statusCode;
            this.serverMessage = serverMessage;
            this.responseTime = DateTime.Now.Ticks - requestTime ; // System.currentTimeMillis() - requestTime;
            this.throwable = throwable;
        }

        /**
         * Return the WITSML server instance of this event.
         *
         * @return  WITSML server instance of this event. Never null.
         */
        public WitsmlServer getWitsmlServer()
        {
            return witsmlServer;
        }

        /**
         * Return name of the WSDL function being called.
         *
         * @return Name of the WSDL function being called. Never null.
         */
        public String getWsdlFunction()
        {
            return wsdlFunction;
        }

        /**
         * Return the WITSML type being queried.
         *
         * @return  The WITSML type being queried. Null if not applicable
         *          for the given WSDL function.
         */
        public String getWitsmlType()
        {
            return witsmlType;
        }

        /**
         * Get time of request.
         *
         * @return  Time request was made. Never null.
         */
        public DateTime? getRequestTime()
        {
            return new DateTime(requestTime);
        }

        /**
         * Get the client request string. Typcailly an XML query, but this
         * may be depend on the WSDL function being called.
         *
         * @return  The request string. Null if not applicable for the WSDL
         *          function being called.
         */
        public String getRequest()
        {
            return request;
        }

        /**
         * Get the server response string. Typically an XML response, but this
         * may depend on the WSDL function being called.
         *
         * @return  The WITSML response. Null if not applicable for the WSDL
         *          function being called, or the operation failed for some reason.
         */
        public String getResponse()
        {
            return response;
        }

        /**
         * Return the status code defined by the WSDL function.
         *
         * @return  The status code defined by most WSDL functions. Null
         *          if not applicable for the WSDL function being called.
         */
        public Int32? getStatusCode()
        {
            return statusCode;
        }

        /**
         * Return the WITSML server message supplied with the response.
         *
         * @return  The WITSML server message. Null if none supplied or if
         *          not applicable for the WSDL function being called.
         */
        public String getServerMessage()
        {
            return serverMessage;
        }

        /**
         * Return the response time, i.e. the time from the request was
         * sent to the WITSML server until the response was available
         * at the client.
         *
         * @return  The response time in milliseconds. >= 0.
         */
        public long getResponseTime()
        {
            return responseTime;
        }

        /**
         * Return the throwable if one was thrown during the request.
         *
         * @return  The throwable thrown during the request, or null if
         *          the respoinse was returned successfully.
         */
        public Exception /*Throwable*/ getThrowable()
        {
            return throwable;
        }

        /**
         * Return a string representation of this instance.
         *
         * @return  A string representation of this instance. Never null.
         */
        
        public override String ToString()
        {
            StringBuilder s = new StringBuilder();

            //DateFormat timeFormat = new SimpleDateFormat("dd.MM.yy HH:mm:ss");

            String requestText = "";
            if (request != null)
            {
                int length = Math.Min(50, request.Length ); //.Length());
                requestText = request.Substring(0, length);
            }

            String responseText = "";
            if (response != null)
            {
                int length = Math.Min(50, response.Length); //.Length());
                responseText = response.Substring(0, length);
            }

            s.Append("Server...........: " + witsmlServer + "\n");
            s.Append("WSDL function....: " + wsdlFunction + "\n");
            s.Append("Time.............: " + requestTime.ToString("dd.MM.yy HH:mm:ss"));// timeFormat.format(new Date(requestTime)) + "\n");
            s.Append("WITSML type......: " + witsmlType + "\n");
            s.Append("Request..........: " + requestText + "\n");
            s.Append("Response.........: " + responseText + "\n");
            s.Append("Response size....: " + (response != null ? response.Length.ToString() : "0") + "\n");
            s.Append("Response time....: " + responseTime + "ms\n");
            s.Append("Status code......: " + statusCode + "\n");
            s.Append("Server message...: " + serverMessage + "\n");
            s.Append("Exception........: " + (throwable != null ? throwable.Message : "") + "\n");

            return s.ToString();
        }
    }
}