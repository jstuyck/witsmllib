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
//import java.util.Collection;
//import java.util.Collections;

/**
 * Model the object capabilities for a given WITSML function.
 * An example would be the "WMLS_GetFromStore" function where the
 * objects could be "well", "wellbore" and "log" etc.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Text;
using System.Collections.Generic;
sealed class FunctionCapability {
    /** Name of function. */
    private  String functionName;

    /** Version. */
    private  String version;

    /** Objects supported for this function. */
    private  List<String> supportedWitsmlTypes = new List<String>();

    /**
     * Create a new function capability object for the function
     * with the specified name.
     *
     * @param name  Name of the function.
     */
    internal FunctionCapability(String functionName, String version) {
        //Debug.Assert(functionName != null : "functionName cannot be null";

        this.functionName = functionName;
        this.version = version;
    }

    /**
     * Return name of the function (such as "WMLS_GetFromStore" etc.)
     *
     * @return  Name of the function. Never null.
     */
    internal String getFunctionName() {
        return functionName;
    }

    /**
     * Return WITSML version this capability is valid for.
     *
     * @return  WITSML version this capability is valid for. May be null.
     */
    internal String getVersion() {
        return version;
    }

    /**
     * Add data object supported by this function. Data object is
     * a valid WITSML data type name.
     *
     * @param dataObject  Data object supported by this function.
     */
    internal void addWitsmlType(String witsmlType) {
        supportedWitsmlTypes.Add(witsmlType);
    }

    /**
     * Return the WITSML types (such as "well", "wellbore", "log" etc.)
     * supported by this function.
     *
     * @return  WITSML types supported by this function. Never null.
     */
    internal List<String> getSupportedWitsmlTypes() {
        return supportedWitsmlTypes; // Collections.unmodifiableCollection(supportedWitsmlTypes);
    }

    /** {@inheritDoc} */
    
    public override String ToString() {
        StringBuilder s = new StringBuilder(functionName);
        s.Append("(");
        foreach (String witsmlType in supportedWitsmlTypes)
            s.Append(witsmlType + " ");
        s.Append(")");
        return s.ToString();
    }
}
}