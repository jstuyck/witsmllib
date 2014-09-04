
using System;
using System.Text;
using System.Collections.Generic;

namespace witsmllib
{

    /// <summary>
    /// Model the object capabilities for a given WITSML function. 
    /// Note that the client program does not provide function capabilities.
    /// </summary>

    sealed class FunctionCapability
    {
       
        private String functionName;
        private String version;      
        private List<String> supportedWitsmlTypes = new List<String>();

        /// <summary>
        /// Create a new function capability object for the function
        /// with the specified name.
        /// </summary>
        /// <param name="functionName">Name of the function</param>
        /// <param name="version">Version of the function</param>
        internal FunctionCapability(String functionName, String version)
        {
            if (functionName == null)
                throw new ArgumentNullException("functionName cannot be null");
       
            this.functionName = functionName;
            this.version = version;
        }

        /// <summary>
        /// Return name of the function (such as "WMLS_GetFromStore" etc.).
        /// </summary>
        /// <returns>The name of the function.</returns>
        internal String getFunctionName()
        {
            return functionName;
        }

        /// <summary>
        /// Return WITSML version this capability is valid for. 
        /// </summary>
        /// <returns>the WITSML version this capability is valid for. </returns>
        internal String getVersion()
        {
            return version;
        }

        /**
         * Add data object supported by this function. Data object is
         * a valid WITSML data type name.
         *
         * @param dataObject  Data object supported by this function.
         */
        internal void addWitsmlType(String witsmlType)
        {
            supportedWitsmlTypes.Add(witsmlType);
        }

        /// <summary>
        /// Return the WITSML types (such as "well", "wellbore", "log" etc.) supported by this function. 
        /// </summary>
        /// <returns>WITSML types supported by this function. Never null.</returns>
        internal List<String> getSupportedWitsmlTypes()
        {
            return supportedWitsmlTypes;
        }

        /** {@inheritDoc} */

        public override String ToString()
        {
            StringBuilder s = new StringBuilder(functionName);
            s.Append("(");
            foreach (String witsmlType in supportedWitsmlTypes)
                s.Append(witsmlType + " ");
            s.Append(")");
            return s.ToString();
        }
    }
}