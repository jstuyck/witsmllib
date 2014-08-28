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

namespace witsmllib
{

    /**
     * Exception indicating that the parsing of a WITSML response failed for
     * some reason.
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public sealed class WitsmlParseException : Exception
    {

    //    private static sealed long serialVersionUID = -3068010449842244501L;

        /**
         * Create a new WITSML parse exception indicating that the parsing of
         * a WITSML response failed for some reason.
         *
         * @param message  Message. May be null.
         * @param cause    Cause exception. May be null.
         */
        //WitsmlParseException(String message, Throwable cause)
        //{
        //    :base(message, cause);
        //}
        public WitsmlParseException(String message, Exception cause) : base(message, cause) { }
    }
}