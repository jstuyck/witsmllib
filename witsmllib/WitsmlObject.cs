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

/**
 * Base class for all WITSML data objects.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Collections.Generic;
using System.Reflection;
    using System.Security;
    using System.Text;
public abstract class WitsmlObject {

    /** Type of this instance. Non-null. */
    private  String type;

    /** WITSML Version of this intsance. Non-null. */
    private  WitsmlServer server;

    /** Unique (for this WITSML store) ID. May be null. */
    private  String id;

    /** Name of this instance. May be null. */
    private  String name;

    /** Parent instance. Null for instances at root level. */
    private WitsmlObject parent;

    /** ID of parent. Null if at root level or extracted without parent. */
    private String parentId;

    /** Common data of this instance. May be null. */
    protected WitsmlCommonData commonData;

    /**
     * Create a new WITSML object with specified properties.
     *
     * @param server     Server backing this instance. Non-null.
     * @param type       WITSML type of this instance. Non-null.
     * @param id         ID of this instance. Null if ID is not supported
     *                   for this type.
     * @param name       Name of this instance. May be null if not loaded
     *                   or not suppoerted for this type.
     * @param parent     Parent instance. Null if accessed from root level,
     *                   or parentId is specified instead.
     * @parame parentId  ID of parent instance. Null if accessed from root
     *                   level or parent is specified instead.
     */
    protected WitsmlObject(WitsmlServer server,
                           String type, String id, String name, WitsmlObject parent,
                           String parentId) {
        //Debug.Assert(server != null : "server cannot be null";
        //Debug.Assert(type != null : "type cannot be null";
        //Debug.Assert(parent == null || parent.getId().Equals(parentId);

        this.server = server;
        this.type = type;
        this.id = id;
        this.name = name;
        this.parent = parent;
        this.parentId = parent != null ? parent.getId() : parentId;
    }

    /**
     * Return ID of this instance.
     *
     * @return  ID of this instance. Null for types that
     *          doesn't support ID's.
     */
    public String getId() {
        return id;
    }

    /**
     * Return name of this instance.
     *
     * @return  Name of this instance. Null if name is not loaded.
     */
    public String getName() {
        return name;
    }

    /**
     * Return the WITSML type of this instance.
     *
     * @return  The WITSML type of this instance. Never null.
     */
    public String getWitsmlType() {
        return type;
    }

    /**
     * Return the WITSML server backing this instance.
     *
     * @return  The WITSML server backing this instance. Never null.
     */
    public WitsmlServer getWitsmlServer() {
        return server;
    }

    /**
     * Return the WITSML version of this instance.
     *
     * @return  The WITSML version of this instance. Never null.
     */
    public WitsmlVersion getVersion() {
        return server.getVersion();
    }

    /**
     * Return ID of parent instance.
     *
     * @return  ID of parent instamce. Null if at root level or instance
     *          is extracted without parent reference.
     */
    public String getParentId() {
        return parentId;
    }

    /**
     * Return parent of this instance.
     *
     * @return  Parent of this instance. Null if at root level, or instance
     *          is extracted without parent reference.
     */
    public WitsmlObject getParent() {
        return parent;
    }

    /**
     * Return the common data of this instance.
     *
     * @return  Common data of this instance. Null if not loaded or if common
     *          data is not supported for this type.
     */
    public WitsmlCommonData getCommonData() {
        return commonData;
    }

    /** {@inheritDoc} */
    
    //public int hashCode() {
    //    return id != null ? id.hashCode() : base.hashCode();
    //}

    /**
     * Check if the specified object is equal to this instance.
     * The two are equal if they are the same instance or if their ID matches.
     *
     * @param object  Object to check against. May be null.
     * @return        True if this and the specified object are equal, false otherwise.
     */
    public override bool Equals(Object @object) {
		if (@object == this)
			return true;
		if (@object == null)
			return false;
		if (!(@object is WitsmlObject))
			return false;

		WitsmlObject witsmlObject = (WitsmlObject) @object;
        return id == null ? @object == this : id.Equals(witsmlObject.id);
    }

    public static String ToString(Object instance, int indent) {
        Dictionary<String, String> values = new Dictionary<String, String>();

        try {
            //var methods = Assembly.GetCallingAssembly().GetType().GetMethods(); 
            var methods = instance.GetType().GetMethods();
           // Method[] methods = instance.getClass().getMethods();
            foreach (var method in methods) {
                String name = method.Name;
                bool isGetter = name.StartsWith("get");
                bool hasArguments = method.GetParameters().Length > 0 ; //.getParameterTypes().Length > 0;
                //Class<?> returnType = method.getReturnType();
                Type returnType = method.ReturnType;
                bool returnsCollection = returnType.IsArray; // Arrays.asList(returnType.getInterfaces()).contains(Collection.class);

                if (isGetter && !hasArguments && !returnsCollection) {
                    String key = instance.GetType().Name + "." + name.Substring(3); //.getClass().getSimpleName() + "." + name.substring(3);
                    Object @object = method.Invoke(instance, null); //.invoke(instance);

                    String value;
                    if (@object == null)
                        value = "";
                    else if (@object == instance)
                        value = "this";
                    else
                        value = @object.ToString();

                    values.Add(key, value);
                }
            }
        }
        catch (SecurityException exception) {
            //Debug.Assert(false : "Programming error: " + exception.getMessage();
        }
        catch (AccessViolationException exception){// IllegalAccessException exception) {
            //Debug.Assert(false : "Programming error: " + exception.getMessage();
        }
        catch (TargetInvocationException exception){ // InvocationTargetException exception) {
            //Debug.Assert(false : "Programming error: " + exception.getCause().getMessage();
        }

        int maxKeyLength = 0;
        foreach (var entry in values){//.entrySet()) {
            int keyLength = entry.Key.Length ;//.getKey().Length();
            if (keyLength > maxKeyLength)
                maxKeyLength = keyLength;
        }

        StringBuilder s = new StringBuilder();
        foreach (var entry in values) {
            String key = entry.Key; //.getKey();
            String value = entry.Value; //.getValue();

            for (int i = 0; i < indent; i++)
                s.Append(" ");

            s.Append(key);
            for (int i = key.Length; i < maxKeyLength; i++)
                s.Append(".");
            s.Append(": " + value);

            if (!value.EndsWith("\n"))
                s.Append("\n");
        }

        return s.ToString();
    }

    /** {@inheritDoc} */
    
    public override String ToString() {
        return ToString(this, 0);
    }
}
}