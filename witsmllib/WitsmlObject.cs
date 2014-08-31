using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Text;

namespace witsmllib
{
    /// <summary>
    /// Base class for all WITSML data objects.
    /// </summary>
    public abstract class WitsmlObject
    {

        private String type;                    //Type of this instance. Non-null. 
        private WitsmlServer server;            //WITSML Version of this intsance. Non-null.
        private String id;                      //Unique (for this WITSML store) ID. May be null.
        private String name;                    //Name of this instance. May be null.
        private WitsmlObject parent;            //Parent instance. Null for instances at root level.
        private String parentId;                //ID of parent. Null if at root level or extracted without parent.
        protected CommonData commonData;  //Common data of this instance. May be null

        /// <summary>
        /// Create a new WITSML object with specified properties.
        /// </summary>
        /// <param name="server">Server backing this instance. Non-null.</param>
        /// <param name="type">WITSML type of this instance. Non-null.</param>
        /// <param name="id">ID of this instance. Null if ID is not supported for this type.</param>
        /// <param name="name">Name of this instance. May be null if not loaded or not suppoerted for this type.</param>
        /// <param name="parent"></param>
        /// <param name="parentId"></param>
        protected WitsmlObject(WitsmlServer server,
                               String type, String id, String name, WitsmlObject parent,
                               String parentId)
        {
            if (server == null)
                throw new ArgumentNullException("server cannot be null");
            if (type == null)
                throw new ArgumentNullException("type cannot be null");
         
            this.server = server;
            this.type = type;
            this.id = id;
            this.name = name;
            this.parent = parent;
            this.parentId = parent != null ? parent.getId() : parentId;
        }

        /// <summary>
        /// Return ID of this instance.
        /// </summary>
        /// <returns>ID of this instance. Null for types that doesn't support ID's.</returns>
        public String getId()
        {
            return id;
        }

        /// <summary>
        /// Return name of this instance. 
        /// </summary>
        /// <returns>Name of this instance. Null if name is not loaded.</returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// Return the WITSML type of this instance. 
        /// </summary>
        /// <returns>The WITSML type of this instance. Never null.</returns>
        public String getWitsmlType()
        {
            return type;
        }

        /// <summary>
        /// Return the WITSML server backing this instance. 
        /// </summary>
        /// <returns>The WITSML server backing this instance. Never null.</returns>
        public WitsmlServer getWitsmlServer()
        {
            return server;
        }

       /// <summary>
        /// Return the WITSML version of this instance. 
       /// </summary>
        /// <returns>The WITSML version of this instance. Never null.</returns>
        public WitsmlVersion getVersion()
        {
            return server.getVersion();
        }

       /// <summary>
        /// Return ID of parent instance. 
       /// </summary>
        /// <returns>ID of parent instamce. Null if at root level or instance is extracted without parent reference.</returns>
        public String getParentId()
        {
            return parentId;
        }

       /// <summary>
        /// Return parent of this instance. 
       /// </summary>
        /// <returns>Parent of this instance. Null if at root level, or instance is extracted without parent reference.</returns>
        public WitsmlObject getParent()
        {
            return parent;
        }

        /// <summary>
        /// Return the common data of this instance. 
        /// </summary>
        /// <returns>Common data of this instance. Null if not loaded or if common data is not supported for this type.</returns>
        public CommonData getCommonData()
        {
            return commonData;
        }


        public CommonData newCommonData()
        {
            throw new NotImplementedException();
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
        public override bool Equals(Object @object)
        {
            if (@object == this)
                return true;
            if (@object == null)
                return false;
            if (!(@object is WitsmlObject))
                return false;

            WitsmlObject witsmlObject = (WitsmlObject)@object;
            return id == null ? @object == this : id.Equals(witsmlObject.id);
        }

        public static String ToString(Object instance, int indent)
        {
            Dictionary<String, String> values = new Dictionary<String, String>();

            try
            {
                //var methods = Assembly.GetCallingAssembly().GetType().GetMethods(); 
                var methods = instance.GetType().GetMethods();
                // Method[] methods = instance.getClass().getMethods();
                foreach (var method in methods)
                {
                    String name = method.Name;
                    bool isGetter = name.StartsWith("get");
                    bool hasArguments = method.GetParameters().Length > 0; //.getParameterTypes().Length > 0;
                    //Class<?> returnType = method.getReturnType();
                    Type returnType = method.ReturnType;
                    bool returnsCollection = returnType.IsArray; // Arrays.asList(returnType.getInterfaces()).contains(Collection.class);

                    if (isGetter && !hasArguments && !returnsCollection)
                    {
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
            catch (SecurityException exception)
            {
                //Debug.Assert(false : "Programming error: " + exception.getMessage();
            }
            catch (AccessViolationException exception)
            {// IllegalAccessException exception) {
                //Debug.Assert(false : "Programming error: " + exception.getMessage();
            }
            catch (TargetInvocationException exception)
            { // InvocationTargetException exception) {
                //Debug.Assert(false : "Programming error: " + exception.getCause().getMessage();
            }

            int maxKeyLength = 0;
            foreach (var entry in values)
            {//.entrySet()) {
                int keyLength = entry.Key.Length;//.getKey().Length();
                if (keyLength > maxKeyLength)
                    maxKeyLength = keyLength;
            }

            StringBuilder s = new StringBuilder();
            foreach (var entry in values)
            {
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

        public override String ToString()
        {
            return ToString(this, 0);
        }
    }
}