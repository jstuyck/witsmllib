using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
{
    /// <summary>
    /// Meta-information about a WITSML request. 
    /// Access events are retrieved by installing an WitsmlAccessListener in the WitsmlServer instance. 
    /// 
    /// This class is immutable. 
    /// </summary>
    public sealed class WitsmlRequest
    {
        private WitsmlServer _server;
        private DateTime _requestime;
        private string _request;
        private string _witsmlType;
        private string _wsdlFunctionName;


        /// <summary>
        /// Get the client request string. 
        /// Typcailly an XML query, but this may be depend on the WSDL function being called. 
        /// </summary>
        /// <returns>The request string. Null if not applicable for the WSDL function being called.</returns>
        public String getRequest()
        {
            return _request;
        }

        /// <summary>
        /// Get time of request. 
        /// </summary>
        /// <returns>Time request was made. Never null.</returns>
        public DateTime getRequestTime()
        {
            return _requestime;
        }

        /// <summary>
        /// Return the WITSML server instance of this request.
        /// </summary>
        /// <returns>WITSML server instance of this request. Never null.</returns>
        public WitsmlServer getWitsmlServer()
        {
            return _server;
        }

        /// <summary>
        /// Return the WITSML type being queried. 
        /// </summary>
        /// <returns>The WITSML type being queried. Null if not applicable for the given WSDL function.</returns>
        public String getWitsmlType()
        {
            return _witsmlType;
        }

        /// <summary>
        /// Return name of the WSDL function being called. 
        /// </summary>
        /// <returns>Name of the WSDL function being called. Never null.</returns>
        public String getWsdlFunction()
        {
            return _wsdlFunctionName;
        }

        public override string ToString()
        {
            return base.ToString();
        }



    }
}
