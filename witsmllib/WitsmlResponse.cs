using System;

namespace witsmllib
{

    /**
     * All response parameters from the WITSML server when
     * calling a WSDL function.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */

    public sealed class WitsmlResponse
    {
        
        private String response;        // The actual response string. May be null if nothing is returned for some reson. 
        private int? statusCode;        // The status code. Null if not applicable for a given WSDL function. 
        private String serverMessage;   // Any server message. Null if not applicable or not provided.
        private long responseTime;     // Time taken by the server to responde.

       

        /// <summary>
        /// Create an instance of the Object WitsmlResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        /// <param name="serverMessage"></param>
        internal WitsmlResponse(string response, int? statusCode, string serverMessage, long responseTime)
        {
            this.response = response;
            this.statusCode = statusCode;
            this.serverMessage = serverMessage;
            this.responseTime = responseTime;
        }

        /// <summary>
        /// Get the server response string. Typically an XML response, but this may depend on the WSDL function being called. 
        /// </summary>
        /// <returns>The WITSML response. Null if not applicable for the WSDL function being called, or the operation failed for some reason.</returns>
        internal string getResponse()
        {
            return response;
        }

        /// <summary>
        /// Return the status code defined by the WSDL function. 
        /// </summary>
        /// <returns>The status code defined by most WSDL functions. Null if not applicable for the WSDL function being called.</returns>
        internal int? getStatusCode()
        {
            return statusCode;
        }

        /// <summary>
        /// Return the WITSML server message supplied with the response. 
        /// </summary>
        /// <returns>The WITSML server message. Null if none supplied or if not applicable for the WSDL function being called.</returns>
        internal string getServerMessage()
        {
            return serverMessage;
        }

        /// <summary>
        /// Return the response time. I.e. the time from the request was sent to the WITSML server until the response was available at the client. 
        /// </summary>
        /// <returns>The response time in milliseconds. >= 0.</returns>
        internal long getResponseTime()
        {
            throw new NotImplementedException();
        }



    }
}