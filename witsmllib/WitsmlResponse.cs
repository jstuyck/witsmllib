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

    /**
     * All response parameters from the WITSML server when
     * calling a WSDL function.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    sealed class WitsmlResponse
    {
        /** The actual response string. May be null if nothing is returned for some reson. */
        private  String response;

        /** The status code. Null if not applicable for a given WSDL function. */
        private  Int32? statusCode;

        /** Any server message. Null if not applicable or not provided. */
        private  String serverMessage;

        /**
         * Create a WITSML response instance.
         *
         * @param response       The response string. May be null.
         * @param statusCode     Status code if applicable. May be null.
         * @param serverMessage  Any server message. May be null.
         * @return
         */
        internal WitsmlResponse(String response, Int32? statusCode, String serverMessage)
        {
            this.response = response;
            this.statusCode = statusCode;
            this.serverMessage = serverMessage;
        }

        /**
         * Return the actual response.
         *
         * @return Actual response. May be null.
         */
        internal String getResponse()
        {
            return response;
        }

        /**
         * Return the WSDL function status code.
         *
         * @return  The WSDL function status code. May be null.
         */
        internal Int32? getStatusCode()
        {
            return statusCode;
        }

        /**
         * Return any server message.
         *
         * @return  Any WITSML server message. May be null.
         */
        internal String getServerMessage()
        {
            return serverMessage;
        }
    }
}