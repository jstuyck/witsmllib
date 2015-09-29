using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace witsmllib
{

    public enum UomBase
    {
        Metric,
        Imperial
    }

    public sealed class WitsmlServer
    {


        protected static UomBase uomBase = UomBase.Metric; //defaul value
        private WitsmlStoreAccessor accessor;
        private Capabilities clientCapabilities;
        private WitsmlVersion version;
        private List<WeakReference> accessListeners = new List<WeakReference>();

        public static UomBase UOM { get { return uomBase; } set { uomBase = value; } }
        internal static string distUom { get { return uomBase == UomBase.Metric ? "m" : "ft"; } }
        internal static string volUom { get { return uomBase == UomBase.Metric ? "m3" : "ft3"; } }
        internal static string forceUom { get { return uomBase == UomBase.Metric ? "N" : "lbf"; } }

        //TODO : add for "m/s", pa, etc 

        /// <summary>
        /// Create a new WITSML server instance.
        /// </summary>
        /// <param name="url"> URL to server. Non-null.</param>
        /// <param name="userName">User name for server login. Non-null.</param>
        /// <param name="password"> Password for server login. Non-null.</param>
        /// <param name="version">Version we will communicate through. Non-null.</param>
        /// <param name="clientCapabilities">The client signature to pass to server. Non-null.</param>
        public WitsmlServer(String url, String userName, String password,
                            WitsmlVersion version,
                            Capabilities clientCapabilities)
        {

            if (url == null)
                throw new ArgumentException("url cannot be null");

            if (userName == null)
                throw new ArgumentException("userName cannot be null");

            if (version == null)
                throw new ArgumentException("version cannot be null");

            if (clientCapabilities == null)
                throw new ArgumentException("clientCapabilities cannot be null");

            this.clientCapabilities = clientCapabilities;
            this.version = version;

            accessor = new WitsmlStoreAccessor(url, userName, password, clientCapabilities.toXml());
        }

        /// <summary>
        /// Add the specified access listener to this server. The listener is called
        /// every time there is a remote call to the WITSML server.
        /// </summary>
        /// <param name="accessListener">Listener to add. If the listener is already 
        /// added to this server, this call has no effect. Non-null.
        /// </param>
        public void addAccessListener(WitsmlAccessListener accessListener)
        {
            if (accessListener == null)
                throw new ArgumentException("accessListener cannot be null");

            // Check if it is there already
            foreach (WeakReference reference in accessListeners)
                if (accessListener.Equals(reference.Target))
                    return;

            accessListeners.Add(new WeakReference(accessListener));
        }

        /// <summary>
        /// Remove the specified access listener from this server.
        /// </summary>
        /// <param name="accessListener">
        /// Listener to remove. Non-null. If the instance 
        /// has not been previously added as a listener
        /// to this server, this call has no effect.
        /// </param>
        public void removeAccessListener(WitsmlAccessListener accessListener)
        {
            if (accessListener == null)
                throw new ArgumentException("accessListener cannot be null");

            foreach (var i in accessListeners)
            {
                WeakReference reference = i as WeakReference;
                WitsmlAccessListener listener = reference.Target as WitsmlAccessListener;
                if (listener.Equals(accessListener))
                {
                    accessListeners.Remove(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Notify listeners about WITSML server access.
        /// </summary>
        /// <param name="wsdlFunction">The WSDL function being called. Non-null.</param>
        /// <param name="witsmlType">The WITSML type being accessed. Null if not applicable.</param>
        /// <param name="requestTime">Time of request.</param>
        /// <param name="request">The request string. Null if not applicable.</param>
        /// <param name="response">The response string. Null if not applicable or the call failed for some reason.</param>
        /// <param name="statusCode">The status code from the server. Null if not applicable for the WSDL function.</param>
        /// <param name="serverMessage">Message from the WITSML server. Null if not supplied or not applicable for the WSDL function.</param>
        /// <param name="throwable">Exception thrown. Null if none.</param>
        private void notify(String wsdlFunction,
                            String witsmlType,
                            long requestTime,
                            String request,
                            String response,
                            Int32? statusCode,
                            String serverMessage,
                            Exception throwable)
        {
            if (wsdlFunction == null)
                throw new ArgumentNullException("wsdlFunction cannot be null");

            // Create the event
            WitsmlAccessEvent @event = new WitsmlAccessEvent(this,
                                                            wsdlFunction,
                                                            witsmlType,
                                                            requestTime,
                                                            request,
                                                            response,
                                                            statusCode,
                                                            serverMessage,
                                                            throwable);

            foreach (WeakReference reference in accessListeners)
            {
                WitsmlAccessListener listener = reference.Target as WitsmlAccessListener;
                if (listener == null)
                    accessListeners.Remove(reference);
                //TODO - what does this do
                //else
                //    listener.accessPerformed(@event);
            }
        }

        /// <summary>
        ///  Return timeout used by this server.
        /// </summary>
        /// <returns>Timeout used by this server in milliseconds. >= 0.</returns>
        public int getTimeout()
        {
            return accessor.getTimeout();
        }

        /// <summary>
        /// Set timeout that should be used by this server. If not specified,
        /// the default timeout is 90000 (90 seconds). Remote calls taking longer
        /// than this will cause a WitsmlServerException.
        /// </summary>
        /// <param name="timeout">Timeout to use by this server in milliseconds. > 0.</param>
        public void setTimeout(int timeout)
        {
            if (timeout <= 0)
                throw new ArgumentException("Invalid timeout: " + timeout);
            accessor.setTimeout(timeout);
        }

        /// <summary>
        /// Return URL of this WITSML server.
        /// </summary>
        /// <returns>URL of this WITSML server. Never null.</returns>
        public string getUrl()
        {
            return accessor.getUrl();
        }

        /// <summary>
        /// Return user name of this WITSML connection.
        /// </summary>
        /// <returns>User name of this WITSML connection. Never null.</returns>
        public String getUserName()
        {
            return accessor.getUsername();
        }

        /// <summary>
        /// Return the password of this WITSML connection.
        /// </summary>
        /// <returns>The password of this WITSML connection. Never null.</returns>
        public String getPassword()
        {
            return accessor.getPassword();
        }

        /// <summary>
        /// Return the client capabilities.
        /// </summary>
        /// <returns>Client capabilities. Never null.</returns>
        public Capabilities getClientCapabilities()
        {
            return clientCapabilities;
        }

        /// <summary>
        /// Get WITSML version of this WITSML server.
        /// </summary>
        /// <returns>WITSML version of this WITSML server. Never null.</returns>
        public WitsmlVersion getVersion()
        {
            return version;
        }

        /// <summary>
        /// Return WITSML version as reported by the remote server.
        /// </summary>
        /// <returns>WITSML version as reported by the remote WITSML server
        /// using the GetVersion call.
        /// </returns>
        public String getServerVersion()
        {

            String wsdlFunction = "WMLS_GetVersion";
            long requestTime = DateTime.Now.Ticks;

            try
            {
                String version = accessor.getVersion();
                notify(wsdlFunction, null, requestTime, null, version, null, null, null);
                return version;
            }
            catch (Exception exception)
            {
                notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
                throw new WitsmlServerException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Return the capabilities as reported by the server.
        /// </summary>
        /// <returns> The capabilities as reported by the server. Never null.</returns>
        public Capabilities getServerCapabilities()
        {

            String wsdlFunction = "WMLS_GetCap";
            long requestTime = DateTime.Now.Ticks;

            try
            {
                WitsmlResponse response = accessor.getServerCapabilities();
                String responseXml = response.getResponse();

                notify(wsdlFunction, null, requestTime, null, responseXml,
                       response.getStatusCode(),
                       response.getServerMessage(),
                       null);

                Capabilities capabilities = new Capabilities(version, responseXml);
                return capabilities;
            }
            catch (WitsmlParseException exception)
            {
                notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
                throw new WitsmlServerException(exception.Message, exception);
            }
            catch (Exception /* RemoteException */ exception)
            {
                notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
                throw new WitsmlServerException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Return the associated status message of the specified status code.
        /// </summary>
        /// <param name="statusCode">Status code to get message for.</param>
        /// <returns>The associated message, or null if not found.</returns>
        public String getStatusMessage(int statusCode)
        { //throws WitsmlServerException {

            String wsdlFunction = "WMLS_GetBaseMsg";
            long requestTime = DateTime.Now.Ticks; // System.currentTimeMillis();

            try
            {
                String message = accessor.getStatusMessage((short)statusCode);
                notify(wsdlFunction, null, requestTime, null, message, null, null, null);
                return message;
            }
            catch (Exception exception)
            { //RemoteException exception) {
                notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
                throw new WitsmlServerException(exception.Message, exception);
            }
        }

        /// <summary>
        ///  Return the WITSML type for the specified clazz.
        /// </summary>
        /// <param name="clazz">Class to check WITSML type for.</param>
        /// <returns></returns>
        private static String getWitsmlType(Type clazz)
        {
            try
            {
                //TODO - this is a private field, partial trust wont work on this... 
                var field = clazz.GetField("WITSML_TYPE", BindingFlags.Static | BindingFlags.NonPublic);
                //Field field = clazz.getDeclaredField("WITSML_TYPE");
                // field.setAccessible(true);
                return (String)field.GetValue(null);//.get(null);
            }
            catch (NotSupportedException exception)
            { // NoSuchFieldException exception) {
                //Debug.Assert(false : "Invalid WitsmlObject class: " + clazz;
                return null;
            }
            catch (AccessViolationException exception)
            {// IllegalAccessException exception) {
                //Debug.Assert(false : "Invalid WitsmlObject class: " + clazz;
                return null;
            }
        }

        /// <summary>
        /// Return all instances of the specified class from this WITSML server.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <returns>Requested instances. Never null.</returns>
        public List<T> get<T>(WitsmlQuery witsmlQuery) where T : WitsmlObject
        {
            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            return get<T>(witsmlQuery, null, null, new String[0]);
        }

        /// <summary>
        /// Return all children of the given class from the specified parent instance.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <param name="parent">Parent instance. May be null, to indicate root-level.</param>
        /// <returns>Requested instances. Never null.</returns>
        public List<T> get<T>(WitsmlQuery witsmlQuery, WitsmlObject parent) where T : WitsmlObject
        {
            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            return get<T>(witsmlQuery, null, parent);
        }

        /// <summary>
        /// Return all children of the given class from the specified parent instance.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <param name="parentIds">Id of parent(s). Closest parent first. Null to indicate root-level.</param>
        /// <returns>Requested instances. Never null.</returns>
        public List<T> get<T>(WitsmlQuery witsmlQuery, params String[] parentIds) where T : WitsmlObject
        {
            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            return get<T>(witsmlQuery, null, null, parentIds);
        }

        /// <summary>
        /// Return instance of the given class with the given ID from this server
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="baseClass">Class of children to return. Non-null.</param>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <param name="id">ID of instance to get. Non-null.</param>
        /// <returns>The requested instance, or null if not found.</returns>
        public T getOne<T>(T baseClass, WitsmlQuery witsmlQuery, String id) where T : WitsmlObject
        {
            if (baseClass == null)
                throw new ArgumentNullException("baseClass cannot be null");

            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            if (id == null)
                throw new ArgumentNullException("id cannot be null");

            List<T> objects = get<T>(witsmlQuery, id, null, new String[0]);

            if (objects.Count > 1)
                throw new WitsmlServerException("Multiple instances with ID = " + id, null);

            return objects.Count == 0 ? default(T) : objects[0];
        }

        /// <summary>
        /// Return child of the given class and parent with the given ID from this server.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery"> Query to apply. Non-null.</param>
        /// <param name="id">ID of instance to get. Non-null.</param>
        /// <param name="parent">Parent instance of child to get. Non-null</param>
        /// <returns>The requested instance, or null if not found.</returns>
        public T getOne<T>(WitsmlQuery witsmlQuery, String id, WitsmlObject parent) where T : WitsmlObject
        {
            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            if (id == null)
                throw new ArgumentNullException("id cannot be null");

            if (parent == null)
                throw new ArgumentNullException("parent cannot be null");

            List<T> objects = get<T>(witsmlQuery, id, parent);

            if (objects.Count > 1)
                throw new WitsmlServerException("Multiple instances with ID = " + id, null);

            return objects.Count == 0 ? default(T) : objects[0];
        }

        /// <summary>
        /// Return child of the given class and parent with the given ID from this server.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <param name="id">ID of instance to get. Non-null.</param>
        /// <param name="parentIds">Parent IDs, closest first. Non-null</param>
        /// <returns>The requested instance, or null if not found.</returns>
        public T getOne<T>(WitsmlQuery witsmlQuery, String id, params String[] parentIds) where T : WitsmlObject
        {
            if (witsmlQuery == null)
                throw new ArgumentNullException("witsmlQuery cannot be null");

            if (id == null)
                throw new ArgumentNullException("id cannot be null");

            if (parentIds == null)
                throw new ArgumentNullException("parentIds cannot be null");

            List<T> objects = get<T>(witsmlQuery, id, null, parentIds);

            if (objects != null && objects.Count > 1)
                throw new WitsmlServerException("Multiple instances with ID = " + id, null);

            return objects == null || objects.Count > 0 ? default(T) : objects[0];
        }


        /// <summary>
        /// Return the actual (i.e. version specific) class of the given WITSML type.
        /// </summary>
        /// <param name="version">Version to consider. Non-null.</param>
        /// <param name="type">WITSML type to find class of. Non-null.</param>
        /// <returns>The actual class. Never null.</returns>
        private static Type getActualClass(WitsmlVersion version, String type)
        {
            string nspace = "v" + version.getVersion().Replace(".", "");
            List<Type> AvailableClasses = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace.Contains(nspace)).ToList();
            Type fountClass = AvailableClasses.Where(x => x.Name.ToLower().Contains(type.ToLower())).FirstOrDefault() ;

            return fountClass;
        }

        /// <summary>
        /// Get ancestor IDs from the specified instance and up the hierarchy.
        /// </summary>
        /// <param name="instance">Instance to start with. Non-null.</param>
        /// <returns>IDs starting with ID of given instance and upwards.</returns>
        private static String[] getIds(WitsmlObject instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance cannot be null");

            List<String> ids = new List<String>();

            WitsmlObject p = instance;
            while (p != null)
            {
                ids.Add(p.getId());
                p = p.getParent();
            }

            return ids.ToArray();
        }

        /// <summary>
        /// Return instances from this WITSML server.
        /// </summary>
        /// <typeparam name="T">Type of the Class to return. Non-null.</typeparam>
        /// <param name="witsmlQuery">Query to apply. Non-null.</param>
        /// <param name="id">ID of instance to get, or null to indicate all.</param>
        /// <param name="parent">arent object. Null to indicate root-level or if  parent IDs are specified instead.</param>
        /// <param name="parentIds"> ID of parent(s). Closest parent first. Null to indicate root-level or if parent instance is specified instead.</param>
        /// <returns>List of Requested instances. Never null.</returns>
        private List<T> get<T>(WitsmlQuery witsmlQuery,
                                String id, WitsmlObject parent,
                                params String[] parentIds) where T : WitsmlObject
        {
            

            if (witsmlQuery == null)
                throw new ArgumentException("witsmlQuery cannot be null");

            if (parent != null && parentIds != null && parentIds.Length > 0)
                throw new ArgumentException("Both parent and parentIds can't be set");

            String wsdlFunction = "WMLS_GetFromStore";

            // Prepare the return structure
            List<T> instances = new List<T>();

            String actualId = id != null ? id : "";

            String[] parentId;
            if (parent != null)
                parentId = getIds(parent);
            else if (parentIds != null)
                parentId = parentIds;
            else
                parentId = new String[] { "" };

            long requestTime = DateTime.Now.Ticks;

            String queryXml = "";
            String responseXml = null;

            String type = WitsmlServer.getWitsmlType(typeof(T));
           
            var actualClass = getActualClass(version, type);

            try
            {
                var getQueryMethod = actualClass.GetMethod("getQuery", BindingFlags.NonPublic | BindingFlags.Static);
                queryXml = (string)getQueryMethod.Invoke(null, new object[] { actualId, parentId });
                //// Find the getQuery() method
                //Method getQueryMethod = actualClass.getDeclaredMethod("getQuery",
                //                                                      String.class,
                //                                                      String[].class);
                //getQueryMethod.setAccessible(true);

                //// This is the complete query for the class
                //queryXml = (String) getQueryMethod.invoke(null, actualId, parentId);

                //// Apply nodifications as specified by the WitsmlQuery instance
                
                queryXml = witsmlQuery.apply(queryXml);

                // Send the query to the WITSML server and pick the response
                WitsmlResponse response = accessor.get(type, queryXml);
                responseXml = response.getResponse();


                notify(wsdlFunction, type, requestTime, queryXml, responseXml,
                       response.getStatusCode(),
                       response.getServerMessage(),
                       null);

                // If nothing was returned we leave here
                if (responseXml == null || responseXml.Length == 0)
                    return instances;

                //
                // Build DOM document
                //
                //SAXBuilder builder = new SAXBuilder();
                XDocument document = XDocument.Load(new StringReader(responseXml));
                //Document document = XDocument.Load(new StringReader(responseXml));
                XElement rootElement = document.Root;
                //Element rootElement = document.getRootElement();

                //
                // Find the newInstance() method
                //
                MethodInfo newInstanceMethod = actualClass.GetMethod("newInstance", BindingFlags.NonPublic | BindingFlags.Static); //.getDeclaredMethod("newInstance",
               
#if DEBUG
                if (newInstanceMethod == null)
                    throw new NotImplementedException("newInstance method is not implemented for the classe " + actualClass.Name);
#endif
                
                //WitsmlServer.class,
                // WitsmlObject.class,
                // Element.class);
                //newInstanceMethod.setAccessible(true);

                //
                // Loop over the elements and create instances accordingly
                //
                var elements = rootElement.Elements(rootElement.Name.Namespace + type); //.getChildren(type, rootElement.getNamespace());
                foreach (Object element in elements)
                {
                    WitsmlObject instance = (WitsmlObject)newInstanceMethod.Invoke(null, new object[] { this, parent, element }); //.invoke(null, this, parent, element);
                    instances.Add(instance as T); // baseClass.cast(instance));
                }

                return instances;
            }
            catch (MissingMemberException exception)
            {// NoSuchMethodException exception) {
                // Programmer error
                //Debug.Assert(false : "Method not found: " + actualClass;
                return null;
            }
            catch (AccessViolationException exception)
            {// IllegalAccessException exception) {
                // Programmer error
                //Debug.Assert(false : "Unable to invoke: " + actualClass;
                return null;
            }
            catch (TargetInvocationException exception)
            {// InvocationTargetException exception) {
                // Wrapped exception from the invocation such as WitsmlParseException

                //TODO re-implement this
                notify(wsdlFunction, type, requestTime, queryXml, responseXml, null, null,
                       exception.InnerException); //.getCause());
                String message = "Exception thrown by " + actualClass + ".newInstance()";
                throw new WitsmlServerException(message, exception.InnerException); //.getCause());
            }
            //catch ( RemoteException exception) {
            //    // Connection problems.
            //    //TODO
            //    //notify(wsdlFunction, type, requestTime, queryXml, responseXml, null, null,
            //    //       exception);
            //    String message = "Unable to connect to WITSML server: " + this;
            //    throw new WitsmlServerException(message, exception);
            //}
            catch (IOException exception)
            {
                // Unable to read response XML

                //TODO
                //notify(wsdlFunction, type, requestTime, queryXml, responseXml, null, null,
                //       exception);
                String message = "Unable to read response XML: " + responseXml;
                throw new WitsmlServerException(message, exception);
            }
            //catch ( JDOMException exception) {
            //    // Unable to parse response XML
            //    notify(wsdlFunction, type, requestTime, queryXml, responseXml, null, null,
            //           exception);
            //    String message = "Unable to parse response XML: " + responseXml;
            //    throw new WitsmlServerException(message, exception);
            //}

        }

        /// <summary>
        /// Update the specified WITSML instance by making a new fetch from the server.
        /// </summary>
        /// <param name="witsmlObject">Instance to update. Non-null.</param>
        /// <param name="witsmlQuery"> Query to apply. Non-null.</param>
        public void update(WitsmlObject witsmlObject, WitsmlQuery witsmlQuery)
        {
            if (witsmlObject == null)
                throw new ArgumentException("witsmlObject cannot be null");

            if (witsmlQuery == null)
                throw new ArgumentException("witsmlQuery cannot be null");

            String wsdlFunction = "WMLS_GetFromStore";
            long requestTime = DateTime.Now.Ticks;
            String witsmlType = witsmlObject.getWitsmlType();

            String queryXml = null;
            String responseXml = null;

            try
            {
                // Find the getQuery() method
                //Method getQueryMethod = witsmlObject.getClass().getDeclaredMethod("getQuery",
                //                                                                  String.class,
                //                                                                  String[].class);
                //getQueryMethod.setAccessible(true);
                MethodInfo getQueryMethod = witsmlObject.GetType().GetMethod("getQuery");

                // This is the complete query for the class
                //        String id = witsmlObject.getId() != null ? witsmlObject.getId() : "";
                //queryXml = (String) getQueryMethod.invoke(null, id,
                //                                          new String[] {witsmlObject.getParentId()});
                queryXml = getQueryMethod.Invoke(null, new object[] { witsmlObject.getParentId() }) as string;

                // Apply nodifications as specified by the WitsmlQuery instance
                queryXml = witsmlQuery.apply(queryXml);

                // Call server and capture the response
                WitsmlResponse response = accessor.get(witsmlType, queryXml);
                responseXml = response.getResponse();

                notify(wsdlFunction, witsmlType, requestTime, queryXml, responseXml,
                       response.getStatusCode(),
                       response.getServerMessage(),
                       null);
                

                //
                // Build DOM document from response
                //
                //SAXBuilder builder = new SAXBuilder();
                //Document document = XDocument.Load(new StringReader(responseXml));
                XDocument document = XDocument.Load(new StringReader(responseXml));

                XElement root = document.Root;//.getRootElement();
                XElement element = root.Element(root.Name.Namespace + witsmlObject.getWitsmlType());//.Element(witsmlObject.getWitsmlType(),
                //root.getNamespace());

                //
                // Find the update() method on the target class
                //
                if (element != null)
                {
                    MethodInfo updateMethod = witsmlObject.GetType().GetMethod("update");//.getClass().getDeclaredMethod("update",

                    //Element.class);
                    //updateMethod.setAccessible(true);

                    // Call update on the instance
                    updateMethod.Invoke(witsmlObject, new object[] { element });
                }
            }
            catch (MissingMethodException exception)
            { // NoSuchMethodException exception) {
                // Programming error
                // exception.printStackTrace();
                Console.Write(exception.StackTrace);
                //Debug.Assert(false : "update() not found: " + witsmlObject.getClass();
            }
            catch (AccessViolationException exception)
            {// IllegalAccessException exception) {
                // Programming error
                //exception.printStackTrace();
                Console.Write(exception.StackTrace);
                //Debug.Assert(false : "Unable to invoke update(): " + witsmlObject.getClass();
            }
            catch (TargetInvocationException exception)
            { // InvocationTargetException exception) {
                // Wrapped exception from the invocation such as WitsmlParseException
                notify(wsdlFunction, witsmlType, requestTime, queryXml, responseXml, null, null,
                       exception);
                String message = "Exception thrown on reflective call on: " + witsmlObject;
                throw new WitsmlServerException(message, exception.InnerException); //.getCause());
            }
            //catch (RemoteException exception) {
            //    // Connection problems.
            //    notify(wsdlFunction, witsmlType, requestTime, queryXml, responseXml, null, null,
            //           exception);
            //    String message = "Unable to connect to WITSML server at " + getUrl();
            //    throw new WitsmlServerException(message, exception);
            //}
            catch (IOException exception)
            {
                // Unable to read response XML
                notify(wsdlFunction, witsmlType, requestTime, queryXml, responseXml, null, null,
                       exception);
                String message = "Unable to read response XML: " + responseXml;
                throw new WitsmlServerException(message, exception);
            }
            //catch (JDOMException exception) {
            //    // Unable to parse response XML
            //    notify(wsdlFunction, witsmlType, requestTime, queryXml, responseXml, null, null,
            //           exception);
            //    String message = "Unable to parse response XML: " + responseXml;
            //    throw new WitsmlServerException(message, exception);
            //}
        }

        /**
         * Delete the specified instance from the WITSML server.
         *
         * @param instance  Instance to delete. Non-null.
         * @throws ArgumentException If instance is null.
         * @throws WitsmlServerException  If the server access failed for some reason.
         */
        public void delete(WitsmlObject instance)
        { //throws WitsmlServerException {

            if (instance == null)
                throw new ArgumentException("instance cannot be null");

            String wsdlFunction = "WMLS_DeleteFromStore";

            long requestTime = DateTime.Now.Ticks;// System.currentTimeMillis();

            String queryXml = "";
            String responseXml = null;

            String type = instance.getWitsmlType();
            //Class<? extends WitsmlObject> 
            var actualClass = getActualClass(version, type);

            String id = instance.getId();
            String parentId = instance.getParentId() != null ? instance.getParentId() : "";

            try
            {
                // Find the getQuery() method
                // Method getQueryMethod = instance.getClass().getDeclaredMethod("getQuery",
                //                                                             String.class,
                //                                                           String[].class);
                MethodInfo getQueryMethod = instance.GetType().GetMethod("getQuery"); //
                //getQueryMethod.setAccessible(true);

                // This is the complete query for the class
                queryXml = (String)getQueryMethod.Invoke(null, new Object[] { id, parentId });// (null, id, new String[] {parentId});

                // Filter the ID part only
                WitsmlQuery query = new WitsmlQuery();
                query.includeElement("name");
                queryXml = query.apply(queryXml);

                Console.WriteLine(queryXml); // System.out.println(queryXml);

                // Send the query to the WITSML server and pick the response
                WitsmlResponse response = accessor.delete(type, queryXml);

                notify(wsdlFunction, type, requestTime, queryXml, null,
                       response.getStatusCode(),
                       response.getServerMessage(),
                       null);
            }
            catch (MissingMethodException exception)
            { // NoSuchMethodException exception) {
                // Programmer error
                //Debug.Assert(false : "Method not found: " + instance;
            }
            catch (AccessViolationException exception)
            {// IllegalAccessException exception) {
                // Programmer error
                //Debug.Assert(false : "Unable to invoke: " + instance;
            }
            catch (TargetInvocationException exception)
            { // InvocationTargetException exception) {
                // Wrapped exception from the invocation such as WitsmlParseException
                notify(wsdlFunction, type, requestTime, queryXml, null, null, null,
                       exception.InnerException); //.getCause());
                String message = "Exception thrown by " + actualClass + ".newInstance()";
                throw new WitsmlServerException(message, exception.InnerException);//.getCause());
            }
            //catch (RemoteException exception) {
            //    // Connection problems.
            //    notify(wsdlFunction, type, requestTime, queryXml, null, null, null,
            //           exception);
            //    String message = "Unable to connect to WITSML server: " + this;
            //    throw new WitsmlServerException(message, exception);
            //}
            catch (IOException exception)
            {
                // Unable to read response XML
                notify(wsdlFunction, type, requestTime, queryXml, null, null, null,
                       exception);
                String message = "Unable to read response XML: " + responseXml;
                throw new WitsmlServerException(message, exception);
            }
        }


        /** {@inheritDoc} */

        public override String ToString()
        {
            return accessor.getUrl().ToString();
        }
    }
}