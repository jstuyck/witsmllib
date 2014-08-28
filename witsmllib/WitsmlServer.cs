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

//import java.io.IOException;
//import java.io.StringReader;
//import java.lang.ref.WeakReference;
//import java.lang.reflect.Field;
//import java.lang.reflect.InvocationTargetException;
//import java.lang.reflect.Method;
//import java.net.URL;
//import java.rmi.RemoteException;
//import java.util.ArrayList;
//import java.util.Collection;
//import java.util.Iterator;
//import java.util.List;
//import java.util.concurrent.CopyOnWriteArrayList;

//import org.jdom.Document;
//import org.jdom.Element;
//import org.jdom.JDOMException;
//import org.jdom.input.SAXBuilder;

/**
 * Class representing the WITSML server in the client program.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Linq;

    public enum UomBase
    {
        Metric,
        Imperial
    }
   
public sealed class WitsmlServer {

    //NEW
    protected static UomBase uomBase = UomBase.Metric; //defaul value

    /** The low level accessor. Non-null. */
    private  WitsmlStoreAccessor accessor;

    /** The client capabilities. Non-null. */
    private  Capabilities clientCapabilities;

    /** The version we will communicate. Non-null. */
    private  WitsmlVersion version;

    /** Access listeners. */
    private  List<WeakReference/*<WitsmlAccessListener>*/> accessListeners =
        new List
        //new CopyOnWriteArrayList
            <WeakReference/*<WitsmlAccessListener>*/>();

    public static UomBase UOM { get { return uomBase; } set { uomBase = value; } }
    internal static string distUom { get { return uomBase == UomBase.Metric ? "m" : "ft"; } }
    internal static string volUom { get { return uomBase == UomBase.Metric ? "m3" : "ft3"; } }
    internal static string forceUom { get { return uomBase == UomBase.Metric ? "N" : "lbf"; } }

    //add for "m/s", pa, etc - todo

    /**
     * Create a new WITSML server instance.
     * <p>
     * Note that WITSML version is explicitly set even if it also may be passed
     * through the capabilities instance. Reason for this is that the content
     * of the client capabilities instance is completely optional by the WITSML
     * standard.
     *
     * @param url                 URL to server. Non-null.
     * @param userName            User name for server login. Non-null.
     * @param password            Password for server login. Non-null.
     * @param version             Version we will communicate through. Non-null.
     * @param clientCapabilities  The client signature to pass to server. Non-null.
     * @throws ArgumentException  If any of the arguments are null.
     */
    public WitsmlServer(String url, String userName, String password,
                        WitsmlVersion version,
                        Capabilities clientCapabilities) {

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

        // Create the low level WITSML accessor instance
        accessor = new WitsmlStoreAccessor(url, userName, password, clientCapabilities.toXml());
    }

    /**
     * Add the specified access listener to this server. The listener is called
     * every time there is a remote call to the WITSML server.
     *
     * @param accessListener  Listener to add. If the listener is already added
     *                        to this server, this call has no effect. Non-null.
     * @throws ArgumentException  If accessListener is null.
     */
    public void addAccessListener(WitsmlAccessListener accessListener) {
        if (accessListener == null)
            throw new ArgumentException("accessListener cannot be null");

        // Check if it is there already
        foreach (WeakReference/*<WitsmlAccessListener>*/ reference in accessListeners)
            if (accessListener.Equals(reference.Target)) //.get()))
                return;

        accessListeners.Add(new WeakReference/*<WitsmlAccessListener>*/(accessListener));
    }

    /**
     * Remove the specified access listener from this server.
     *
     * @param accessListener  Listener to remove. Non-null. If the instance
     *                        has not been previously added as a listener
     *                        to this server, this call has no effect.
     * @throws ArgumentException  If accessListener is null.
     */
    public void removeAccessListener(WitsmlAccessListener accessListener) {
        if (accessListener == null)
            throw new ArgumentException("accessListener cannot be null");

        //for (Iterator<WeakReference/*<WitsmlAccessListener>*/> i = accessListeners.iterator();
        //     i.hasNext(); )
        foreach(var i in accessListeners)
        {
            WeakReference/*<WitsmlAccessListener>*/ reference = i as WeakReference; // i.next();
            WitsmlAccessListener listener = reference.Target as WitsmlAccessListener; // reference.get();
            if (listener.Equals(accessListener))
            {
                accessListeners.Remove(i ); // i.remove();
                break;
            }
        }
    }

    /**
     * Notify listeners about WITSML server access.
     *
     * @param wsdlFunction   The WSDL function being called. Non-null.
     * @param witsmlType     The WITSML type being accessed. Null if not applicable.
     * @param requestTime    Time of request.
     * @param request        The request string. Null if not applicable.
     * @param response       The response string. Null if not applicable or the call
     *                       failed for some reason.
     * @param statusCode     The status code from the server. Null if not applicable
     *                       for the WSDL function.
     * @param serverMessage  Message from the WITSML server. Null if not supplied or
     *                       not applicable for the WSDL function.
     * @param throwable      Exception thrown. Null if none.
     */
    private void notify(String wsdlFunction,
                        String witsmlType,
                        long requestTime,
                        String request,
                        String response,
                        Int32? statusCode,
                        String serverMessage,
                        Exception throwable) {
        //Debug.Assert(wsdlFunction != null : "wsdlFunction cannot be null";

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

        foreach (var i in accessListeners) {
            WeakReference/*<WitsmlAccessListener>*/ reference = i;
            WitsmlAccessListener listener = reference.Target as WitsmlAccessListener ;//.get();
            if (listener == null)
               accessListeners.Remove(i); // i.remove();
            //TODO - what does this do
            //else
            //    listener.accessPerformed(@event);
        }
    }

    /**
     * Return timeout used by this server.
     *
     * @return  Timeout used by this server in milliseconds. >= 0.
     */
    public Int32? getTimeout() {
        return accessor.getTimeout();
    }

    /**
     * Set timeout that should be used by this server. If not specified,
     * the default timeout is 90000 (90 seconds). Remote calls taking longer
     * than this will cause a WitsmlServerException.
     *
     * @param timeout  Timeout to use by this server in milliseconds. > 0.
     * @throws ArgumentException  If timeout is <= 0.
     */
    public void setTimeout(int timeout) {
        if (timeout <= 0)
            throw new ArgumentException("Invalid timeout: " + timeout);

        accessor.setTimeout(timeout);
    }

    /**
     * Return URL of this WITSML server.
     *
     * @return  URL of this WITSML server. Never null.
     */
    public string getUrl() {
        return accessor.getUrl();
    }

    /**
     * Return user name of this WITSML connection.
     *
     * @return  User name of this WITSML connection. Never null.
     */
    public String getUserName() {
        return accessor.getUsername();
    }

    /**
     * Return password of this WITSML connection.
     *
     * @return  Password of this WITSML connection. Never null.
     */
    public String getPassword() {
        return accessor.getPassword();
    }

    /**
     * Return the client capabilities.
     *
     * @return  Client capabilities. Never null.
     */
    public Capabilities getClientCapabilities() {
        return clientCapabilities;
    }

    /**
     * Get WITSML version of this WITSML server.
     *
     * @return  WITSML version of this WITSML server. Never null.
     */
    public WitsmlVersion getVersion() {
        return version;
    }

    /**
     * Return WITSML version as reported by the remote server.
     *
     * @return  WITSML version as reported by the remote WITSML server
     *          using the GetVersion call.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public String getServerVersion()
       { //throws WitsmlServerException {

        String wsdlFunction = "WMLS_GetVersion";
        long requestTime = DateTime.Now.Ticks; //System.currentTimeMillis();

        try {
            String version = accessor.getVersion();
            notify(wsdlFunction, null, requestTime, null, version, null, null, null);
            return version;
        }
        catch (Exception /*RemoteException */ exception) {
            notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
            throw new WitsmlServerException(exception.Message, exception);
        }
    }

    /**
     * Return the capabilities as reported by the server.
     *
     * @return  The capabilities as reported by the server. Never null.
     * @throws  WitsmlServerException  If the access failed for some reason.
     */
    public Capabilities getServerCapabilities()
       { //throws WitsmlServerException {

        String wsdlFunction = "WMLS_GetCap";
        long requestTime = DateTime.Now.Ticks; //System.currentTimeMillis();

        try {
            WitsmlResponse response = accessor.getServerCapabilities();
            String responseXml = response.getResponse();

            notify(wsdlFunction, null, requestTime, null, responseXml,
                   response.getStatusCode(),
                   response.getServerMessage(),
                   null);

            Capabilities capabilities = new Capabilities(version, responseXml);
            return capabilities;
        }
        catch (WitsmlParseException exception) {
            notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
            throw new WitsmlServerException(exception.Message, exception);
        }catch (Exception /* RemoteException */ exception) {
            notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
            throw new WitsmlServerException(exception.Message, exception);
        }
    }

    /**
     * Return the associated status message of the specified status code.
     *
     * @see WitsmlAccessEvent#getStatusCode
     * @param statusCode  Status code to get message for.
     * @return  The associated message, or null if not found.
     * @throws WitsmlServerException  If the access failed for some reason.
     */
    public String getStatusMessage(int statusCode)
       { //throws WitsmlServerException {

        String wsdlFunction = "WMLS_GetBaseMsg";
        long requestTime =DateTime.Now.Ticks; // System.currentTimeMillis();

        try {
            String message = accessor.getStatusMessage((short)statusCode);
            notify(wsdlFunction, null, requestTime, null, message, null, null, null);
            return message;
        }
        catch (Exception exception) { //RemoteException exception) {
            notify(wsdlFunction, null, requestTime, null, null, null, null, exception);
            throw new WitsmlServerException(exception.Message, exception);
        }
    }

    /**
     * Return the WITSML type for the specified clazz.
     *
     * @param clazz  Class to check WITSML type for.
     * @return       WITSML type of the specified class.
     */
    //private static <T> String getWitsmlType(Class<T> clazz) {
    private static String getWitsmlType(Type clazz){
        try {
            //TODO - this is a private field, partial trust wont work on this... 
            var field = clazz.GetField("WITSML_TYPE", BindingFlags.Static| BindingFlags.NonPublic); 
            //Field field = clazz.getDeclaredField("WITSML_TYPE");
           // field.setAccessible(true);
            return (String) field.GetValue(null);//.get(null);
        }
        catch (NotSupportedException  exception){ // NoSuchFieldException exception) {
            //Debug.Assert(false : "Invalid WitsmlObject class: " + clazz;
            return null;
        }
        catch (AccessViolationException exception){// IllegalAccessException exception) {
            //Debug.Assert(false : "Invalid WitsmlObject class: " + clazz;
            return null;
        }
    }

    /**
     * Return all instances of the specified class from this WITSML server.
     * <p>
     * Example:
     * <pre>
     *   List&lt;WitsmlWell&gt; wells = witsmlServer.get(WitsmlWell.class,
     *                                             new WitsmlQuery());
     * </pre>
     *
     * @param baseClass    Class of instances to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @return             Requested instances. Never null.
     * @throws ArgumentException  If any of the arguments are null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public List<T> get<T>(/*Class<T> baseClass, */WitsmlQuery witsmlQuery) where T:WitsmlObject 
       { //throws WitsmlServerException {
       // if (baseClass == null)
       //     throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        return get<T>(/*baseClass,*/ witsmlQuery, null, null, new String[0]);
    }

    /**
     * Return all children of the given class from the specified parent instance.
     * <p>
     * Example:
     * <pre>
     *   List&lt;WitsmlWellbore&gt; wellbores = witsmlServer.get(WitsmlWellbore.class,
     *                                                     new WitsmlQuery(),
     *                                                     well);
     * </pre>
     *
     * @param baseClass    Class of children to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @param parent       Parent instance. May be null, to indicate root-level.
     * @return             Requested instances. Never null.
     * @throws ArgumentException  If baseClass or witsmlQuery is null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public List<T> get<T>(/*Class<T> baseClass,*/ WitsmlQuery witsmlQuery, WitsmlObject parent)where T:WitsmlObject 
       { //throws WitsmlServerException {
        //if (baseClass == null)
        //    throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        return get<T>(/*baseClass,*/ witsmlQuery, null, parent);
    }

    /**
     * Return all children of the given class from the specified parent instance.
     * <p>
     * Example:
     * <pre>
     *   List&lt;WitsmlWellbore&gt; wellbores = witsmlServer.get(WitsmlWellbore.class,
     *                                                     new WitsmlQuery(),
     *                                                     "W-123");
     * </pre>
     *
     * @param baseClass    Class of children to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @param parentIds    Id of parent(s). Closest parent first. Null to indicate root-level.
     * @return             Requested instances. Never null.
     * @throws ArgumentException  If baseClass or witsmlQuery is null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public List<T> get<T>(/*Class<T> baseClass, */WitsmlQuery witsmlQuery, params String[] parentIds) where T: WitsmlObject
       { //throws WitsmlServerException {
        //if (baseClass == null)
        //    throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        return get<T>(/*baseClass,*/ witsmlQuery, null, null, parentIds);
    }

    /**
     * Return instance of the given class with the given ID from this server.
     * <p>
     * Example:
     * <pre>
     *   WitsmlWellbore wellbore = witsmlServer.getOne(WitsmlWellbore.class,
     *                                                 new WitsmlQuery(),
     *                                                 "WB-123");
     * </pre>
     *
     * @param baseClass    Class of children to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @param id           ID of instance to get. Non-null.
     * @return             The requested instance, or null if not found.
     * @throws ArgumentException  If any of the arguments are null.
     * @throws  WitsmlServerException  If the server access failed for some reason.
     */
    public T getOne<T>(T baseClass, WitsmlQuery witsmlQuery, String id) where T:WitsmlObject
       { //throws WitsmlServerException {
        if (baseClass == null)
            throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        if (id == null)
            throw new ArgumentException("id cannot be null");

        List<T> objects = get<T>(/*baseClass,*/ witsmlQuery, id, null, new String[0]);

        if (objects.Count  > 1)
            throw new WitsmlServerException("Multiple instances with ID = " + id, null);

        return objects.Count == 0? default(T) : objects[0];
    }

    /**
     * Return child of the given class and parent with the given ID from this server.
     * <p>
     * Example:
     * <pre>
     *   WitsmlWellbore wellbore = witsmlServer.getOne(WitsmlWellbore.class,
     *                                                 new WitsmlQuery(),
     *                                                 "WB-123",
     *                                                 well);
     * </pre>
     *
     * @param baseClass    Class of children to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @param id           ID of instance to get. Non-null.
     * @param parent       Parent instance of child to get. Non-null
     * @return             The requested instance, or null if not found.
     * @throws ArgumentException  If any of the arguments are null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public T getOne<T>(/*Class<T> baseClass, */WitsmlQuery witsmlQuery, String id, WitsmlObject parent) where T:WitsmlObject
       { //throws WitsmlServerException {
        //if (baseClass == null)
        //    throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        if (id == null)
            throw new ArgumentException("id cannot be null");

        if (parent == null)
            throw new ArgumentException("parent cannot be null");

        List<T> objects = get<T>(/*baseClass,*/ witsmlQuery, id, parent);

        if (objects.Count > 1)
            throw new WitsmlServerException("Multiple instances with ID = " + id, null);

        return objects.Count==0 ? default(T) : objects[0];
    }

    /**
     * Return child of the given class and parent with the given ID from this server.
     * <p>
     * Example:
     * <pre>
     *   WitsmlWellbore wellbore = witsmlServer.getOne(WitsmlWellbore.class,
     *                                                 new WitsmlQuery(),
     *                                                 "WB-123",
     *                                                 "W-123");
     * </code>
     *
     * @param baseClass    Class of children to return. Non-null.
     * @param WitsmlQuery  Query to apply. Non-null.
     * @param id           ID of instance to get. Non-null.
     * @param parentIds    Parent IDs, closest first. Non-null
     * @return             The requested instance, or null if not found.
     * @throws ArgumentException  If any of the arguments are null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public  T getOne<T>(/*Class<T> baseClass, */WitsmlQuery witsmlQuery, String id, params String[] parentIds) where T: WitsmlObject 
       { //throws WitsmlServerException {

        //if (baseClass == null)
        //    throw new ArgumentException("baseClass cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        if (id == null)
            throw new ArgumentException("id cannot be null");

        if (parentIds == null)
            throw new ArgumentException("parentIds cannot be null");

        List<T> objects = get<T>(/*baseClass,*/ witsmlQuery, id, null, parentIds);

        if (objects != null && objects.Count > 1)
            throw new WitsmlServerException("Multiple instances with ID = " + id, null);

        return objects == null || objects.Count > 0? default(T) : objects[0];
    }

    /**
     * Return the actual (i.e. version specific) class of the given WITSML type.
     *
     * @param version  Version to consider. Non-null.
     * @param type     WITSML type to find class of. Non-null.
     * @return         The actual class. Never null.
     */
  //  @SuppressWarnings("unchecked")
    //private static Class<? extends WitsmlObject> getActualClass(WitsmlVersion version, String type) {
    //    //Debug.Assert(version != null : "version cannot be null";
    //    //Debug.Assert(type != null : "type cannot be null";

    //    String className = null;

    //    try {
    //        className = WitsmlObject.class.getPackage().getName() +
    //                    "." +
    //                    "v" + version.getVersion().replace(".", "") +
    //                    "." +
    //                    "Witsml" +
    //                    type.substring(0, 1).toUpperCase() +
    //                    type.substring(1);

    //        return (Class<? extends WitsmlObject>) Class.forName(className);
    //    }
    //    catch (ClassNotFoundException exception) {
    //        //Debug.Assert(false : "Invalid type: " + className;
    //        return null;
    //    }
    //}
    private static /*object*/ Type getActualClass(WitsmlVersion version, String type) {
        //Debug.Assert(version != null : "version cannot be null";
        //Debug.Assert(type != null : "type cannot be null";

        try{
        String className = typeof(WitsmlObject).Namespace.ToString()
            + ".v" + version.getVersion().Replace(".","")
            + ".Witsml" + type.Substring(0,1).ToUpper() + type.Substring(1); 
            //return Activator.CreateInstance(Type.GetType(className));// as WitsmlObject;
        return Type.GetType(className); 

        }

        //try {
        //    className = WitsmlObject.class.getPackage().getName() +
        //                "." +
        //                "v" + version.getVersion().Replace(".", "") +
        //                "." +
        //                "Witsml" +
        //                type.substring(0, 1).toUpperCase() +
        //                type.substring(1);

        //    return (Class<? extends WitsmlObject>) Class.forName(className);
        //}
        catch (NotSupportedException exception){// ClassNotFoundException exception) {
            //Debug.Assert(false : "Invalid type: " + className;
            return null;
        }
    }

    /**
     * Get ancestor IDs from the specified instance and up the hierarchy.
     *
     * @param instance  Instance to start with. Non-null.
     * @return          IDs starting with ID of given instance and upwards.
     */
    private static String[] getIds(WitsmlObject instance) {
        //Debug.Assert(instance != null : "instance cannot be null";

        List<String> ids = new List<String>();

        WitsmlObject p = instance;
        while (p != null) {
            ids.Add(p.getId());
            p = p.getParent();
        }

        return ids.ToArray(); //new String[0]);
    }

    /**
     * Return instances from this WITSML server.
     *
     * @param baseClass  Class of instances to get. Non-null.
     * @param witsmlQuery  Query to apply. Non-null.
     * @param id           ID of instance to get, or null to indicate all.
     * @param parent       Parent object. Null to indicate root-level or if
     *                     parent IDs are specified instead.
     * @param parentIds    ID of parent(s). Closest parent first. Null to indicate
     *                     root-level or if parent instance is specified instead.
     * @throws ArgumentException  If baseClass or witsmlQuery is null, or
     *                     If bot parent and parentIds are non-null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    //private <T> List<T> get(Class<T> baseClass, 
    private List<T> get<T>(/*T baseClass, */
                            WitsmlQuery witsmlQuery,
                            String id, WitsmlObject parent,
                            params String[] parentIds) where T: WitsmlObject
       { //throws WitsmlServerException {

       // if (baseClass == null)
       //     throw new ArgumentException("baseClass cannot be null");

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
            parentId = new String[] {""};

        long requestTime = DateTime.Now.Ticks; // System.currentTimeMillis();

        String queryXml = "";
        String responseXml = null;

        String type = WitsmlServer.getWitsmlType(typeof(T));
        //Class<? extends WitsmlObject> actualClass = getActualClass(version, type);
        var actualClass = getActualClass(version, type); 

        try {

            var getQueryMethod = actualClass.GetMethod("getQuery", BindingFlags.NonPublic|BindingFlags.Static );
            queryXml = (string)getQueryMethod.Invoke(null, new object[]{actualId, parentId});
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
            if (responseXml.Length == 0)
                return instances;

            //
            // Build DOM document
            //
            //SAXBuilder builder = new SAXBuilder();
            XDocument document = XDocument.Load(new StringReader(responseXml)); 
            //Document document = XDocument.Load(new StringReader(responseXml));
            XElement rootElement = document.Root ; 
            //Element rootElement = document.getRootElement();

            //
            // Find the newInstance() method
            //
            MethodInfo newInstanceMethod = actualClass.GetMethod("newInstance",BindingFlags.NonPublic|BindingFlags.Static); //.getDeclaredMethod("newInstance",
                                                                     //WitsmlServer.class,
                                                                    // WitsmlObject.class,
                                                                    // Element.class);
            //newInstanceMethod.setAccessible(true);
            
            //
            // Loop over the elements and create instances accordingly
            //
            var elements = rootElement.Elements(rootElement.Name.Namespace + type); //.getChildren(type, rootElement.getNamespace());
            foreach (Object element in elements) {
                WitsmlObject instance = (WitsmlObject) newInstanceMethod.Invoke(null, new object[]{this, parent, element}); //.invoke(null, this, parent, element);
                instances.Add(instance as T); // baseClass.cast(instance));
            }

            return instances;
        }
        catch (MissingMemberException exception){// NoSuchMethodException exception) {
            // Programmer error
            //Debug.Assert(false : "Method not found: " + actualClass;
            return null;
        }
        catch (AccessViolationException exception){// IllegalAccessException exception) {
            // Programmer error
            //Debug.Assert(false : "Unable to invoke: " + actualClass;
            return null;
        }
        catch (TargetInvocationException exception){// InvocationTargetException exception) {
            // Wrapped exception from the invocation such as WitsmlParseException

            //TODO re-implement this
            notify(wsdlFunction, type, requestTime, queryXml, responseXml, null, null,
                   exception.InnerException ); //.getCause());
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
        catch (IOException exception) {
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

    /**
     * Update the specified WITSML instance by making a new fetch
     * from the server.
     * <p>
     * Example:
     * <pre>
     *   // Specify bulk-data
     *   WitsmlQuery query = new WitsmlQuery();
     *   query.includeElement("logData");
     *
     *   // Get bulk data for existing log
     *   witsmlWerver.update(log, query);
     * </pre>
     *
     * @param witsmlObject  Instance to update. Non-null.
     * @param witsmlQuery   Query to apply. Non-null.
     * @throws ArgumentException  If any of the arguments are null.
     * @throws WitsmlServerException  If the server access failed for some reason.
     */
    public void update(WitsmlObject witsmlObject, WitsmlQuery witsmlQuery)
       { //throws WitsmlServerException {
        if (witsmlObject == null)
            throw new ArgumentException("witsmlObject cannot be null");

        if (witsmlQuery == null)
            throw new ArgumentException("witsmlQuery cannot be null");

        String wsdlFunction = "WMLS_GetFromStore";
        long requestTime = DateTime.Now.Ticks; // System.currentTimeMillis();
        String witsmlType = witsmlObject.getWitsmlType();

        String queryXml = null;
        String responseXml = null;

        try {
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
            queryXml = getQueryMethod.Invoke(null, new object[]{witsmlObject.getParentId() }) as string; 

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
            if (element != null) {
                MethodInfo updateMethod = witsmlObject.GetType().GetMethod("update");//.getClass().getDeclaredMethod("update",
                                                                                //Element.class);
                //updateMethod.setAccessible(true);

                // Call update on the instance
                updateMethod.Invoke(witsmlObject, new object[]{element});
            }
        }
        catch (MissingMethodException exception){ // NoSuchMethodException exception) {
            // Programming error
           // exception.printStackTrace();
            Console.Write(exception.StackTrace); 
            //Debug.Assert(false : "update() not found: " + witsmlObject.getClass();
        }
        catch (AccessViolationException exception) {// IllegalAccessException exception) {
            // Programming error
            //exception.printStackTrace();
            Console.Write(exception.StackTrace); 
            //Debug.Assert(false : "Unable to invoke update(): " + witsmlObject.getClass();
        }
        catch (TargetInvocationException exception){ // InvocationTargetException exception) {
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
        catch (IOException exception) {
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

        long requestTime = DateTime.Now.Ticks ;// System.currentTimeMillis();

        String queryXml = "";
        String responseXml = null;

        String type = instance.getWitsmlType();
        //Class<? extends WitsmlObject> 
        var actualClass = getActualClass(version, type);

        String id = instance.getId();
        String parentId = instance.getParentId() != null ? instance.getParentId() : "";

        try {
            // Find the getQuery() method
           // Method getQueryMethod = instance.getClass().getDeclaredMethod("getQuery",
             //                                                             String.class,
               //                                                           String[].class);
            MethodInfo getQueryMethod = instance.GetType().GetMethod("getQuery"); //
            //getQueryMethod.setAccessible(true);

            // This is the complete query for the class
            queryXml = (String) getQueryMethod.Invoke(null, new Object[]{id, parentId});// (null, id, new String[] {parentId});

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
        catch (MissingMethodException exception){ // NoSuchMethodException exception) {
            // Programmer error
            //Debug.Assert(false : "Method not found: " + instance;
        }
        catch (AccessViolationException exception){// IllegalAccessException exception) {
            // Programmer error
            //Debug.Assert(false : "Unable to invoke: " + instance;
        }
        catch (TargetInvocationException exception){ // InvocationTargetException exception) {
            // Wrapped exception from the invocation such as WitsmlParseException
            notify(wsdlFunction, type, requestTime, queryXml, null, null, null,
                   exception.InnerException ); //.getCause());
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
        catch (IOException exception) {
            // Unable to read response XML
            notify(wsdlFunction, type, requestTime, queryXml, null, null, null,
                   exception);
            String message = "Unable to read response XML: " + responseXml;
            throw new WitsmlServerException(message, exception);
        }
    }


    /** {@inheritDoc} */
    
    public override String ToString() {
        return accessor.getUrl().ToString();
    }
}
}