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
using System.Runtime.CompilerServices;
namespace witsmllib{

//import java.net.URL;
//import java.rmi.RemoteException;
//import java.util.Map;

//import javax.xml.namespace.QName;
//import javax.xml.rpc.ServiceException;

//import org.apache.axis.client.Call;
//import org.apache.axis.client.Service;
//import org.apache.axis.client.Stub;
//import org.apache.axis.description.OperationDesc;
//import org.apache.axis.description.ParameterDesc;

//import nwitsml.util.XmlUtil;

/**
 * WITSML/STORE client-side server accessor.
 *
 * This class is a WITSML specific SOAP client using the generic SOAP
 * implementation from Apache Axis. It represents the lowest level
 * communication protocol for a WitsML client application.
 *
 * Assuming a complete communication stack as follows, this component
 * fits in as indicated:
 *
 *    RIPS API   -
 *    Java API   - WitsmlStoreManager
 *    WITSML/XML - WitsmlStoreAccessor
 *    SOAP/XML   - Apache Axis
 *    ------------------------
 *    HTTP/S     -
 *    TCP/IP     -
 *
 * Usage is quite simple as WITSML/STORE defines only 4 services for
 * a read-only client:
 *
 *   WMLS_GetVersion   - Get supported WitsML version
 *   WMLS_GetBaseMsg   - Get messages from last WITSML call
 *   WMLS_GetCap       - Get server capabilities
 *   WMLS_GetFromStore - Get data from store
 *
 * These services are mapped into the following methods of this class:
 *
 *   getVersion();
 *   getMessages();
 *   getCapabilities();
 *   get(...);
 *
 * This class has been refactored from the auto-generated code produced
 * by the Apache wsdl2java tool applied on the WitsmlAPI120.wsdl file.
 *
 * This class is thread compatible.
 *
 * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
 */
using System;
using System.Runtime.CompilerServices;
using System.Net;
sealed class WitsmlStoreAccessor{ // : Stub {

    private WMLS131.WMLS service; 
    
    private string cachedEndpoint;
    private string cachedUsername;
    private string cachedPassword ;
    private int cachedTimeout;

    /** Default timeout for this accessor. */
    private static  int DEFAULT_TIMEOUT = 90000;

    /** Maximum number of characters per log message. */
    private static  int MAX_LOGLENGTH = 5000;

    /** Client capabilities instance. Non-null. */
    private  String clientCapabilitiesXml;


    internal string getPassword(){return cachedPassword; }
    internal int getTimeout() { return cachedTimeout; }
    internal void setTimeout(int timeout) { cachedTimeout = timeout; }
    internal string getUsername() { return cachedUsername; }
        
    /**
     * Create an accessor for the specified WITSML server.
     *
     * @param url                 URL to server. Non-null.
     * @param userName            User name for server login. Non-null.
     * @param password            Password for server login. Non-null.
     * @param clientCapabilitiesXml  Description of client capabilities. Non-null.
     */
    internal WitsmlStoreAccessor(string /*URL*/ url, String userName, String password, String clientCapabilitiesXml) {
        //Debug.Assert(url != null : "url cannot be null";
        //Debug.Assert(userName != null : "userName cannot be null";
        //Debug.Assert(password != null : "password cannot be null";
        //Debug.Assert(clientCapabilitiesXml != null : "clientCapabilitiesXml cannot be null";

        cachedEndpoint = url;
        cachedUsername = userName;
        cachedPassword = password;
        cachedTimeout = DEFAULT_TIMEOUT;

        this.clientCapabilitiesXml = clientCapabilitiesXml;

        service = new  WMLS131.WMLS();
        service.Url = cachedEndpoint; 
        service.Timeout = cachedTimeout; 
        service.Credentials = new NetworkCredential(cachedUsername, cachedPassword); 

        //setPortName("StoreSoapPort");
    }

    /**
     * Create a SOAP call based on the specified operations and the
     * WITSML command.
     *
     * @param operation  Operation description. Non-null.
     * @param command    WITSML call. Non-null.
     * @return           SOAP call. Never null.
     * @throws RemoteException  If operation failed somehow.
     */
    //private Call createCall(OperationDesc operation,
    //                        String command){ //throws RemoteException {
    //    //Debug.Assert(operation != null : "operation cannot be null";
    //    //Debug.Assert(command != null : "command cannot be null";

    //    Call call;
    //    try {
    //        call = (Call) service.createCall();
    //    }
    //    catch (ServiceException exception) {
    //        throw new RemoteException("WitsML connection failed", exception);
    //    }
    //    call.setTargetEndpointAddress(cachedEndpoint);
    //    call.setPortName(cachedPortName);
    //    call.setUsername(cachedUsername);
    //    call.setPassword(cachedPassword);
    //    call.setTimeout(cachedTimeout);
    //    call.setOperation(operation);
    //    call.setUseSOAPAction(true);
    //    call.setSOAPActionURI("http://www.witsml.org/action/120/Store." + command);
    //    call.setOperationName(new QName("http://www.witsml.org/message/120", command));

    //    setRequestHeaders(call);
    //    setAttachments(call);

    //    return call;
    //}

    /**
     * Make a SOAP call.
     *
     * @param call  The call to make. Non-null.
     * @param args  Call arguments.
     * @return      The result.
     * @throws RemoteException  If operation failed for some reason.
     */
    //private Object invokeCall(Call call, params Object args){ //throws RemoteException {
    //    //Debug.Assert(call != null : "call cannot be null";

    //    Object response = call.invoke(args);
    //    if (response is RemoteException) {
    //        throw (RemoteException) response;
    //    }

    //    return response;
    //}

    /**
     * Return the URL of this WITSML connection.
     *
     * @return  URL of this WITSLML connection. Never null.
     */
    internal  String getUrl() {
        return cachedEndpoint;
    }

    /**
     * Return WITSML server version.
     *
     * @return WITSML server version.
     * @throws RemoteException  If operation failed for some reason.
     */
 //   [MethodImpl(MethodImplOptions.Synchronized)] 
 internal String getVersion(){ 
     //throws RemoteException {
        // The WITSML command string

     return service.WMLS_GetVersion(); 
        //sealed String command = "WMLS_GetVersion";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "string"));
        //operation.setReturnClass(String.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call);

        //// Return the response
        //return (String) response;
    }

    /**
     * Get status message of the specified status code.
     *
     * @param statusCode  Status code to get status message of.
     * @return Associated status message.
     * @throws RemoteException  If operation failed somehow.
     */
 //   [MethodImpl(MethodImplOptions.Synchronized)] 
 internal String getStatusMessage(short statusCode){ //throws RemoteException {

        return service.WMLS_GetBaseMsg(statusCode);
 
        // The WITSML command string
        //sealed String command = "WMLS_GetBaseMsg";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.addParameter(new QName("", "ReturnValueIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "short"), short.class, ParameterDesc.IN,
        //        false, false);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "string"));
        //operation.setReturnClass(String.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call, Short.valueOf((short) statusCode));
        //extractAttachments(call);

        //// Return the response
        //return (String) response;
    }

    /**
     * Return capabilities of the WITSML server.
     *
     * @return Capabilities of the WITSML server (an XML string).
     * @throws RemoteException  If operation failed for some reason.
     */
    [MethodImpl(MethodImplOptions.Synchronized)] 
  internal WitsmlResponse getServerCapabilities(){ //throws RemoteException {

        string capabilitiesOutXml,
            message; 
        short statusCode = service.WMLS_GetCap("",out capabilitiesOutXml, out message); 
        // The WITSML command string
        //sealed String command = "WMLS_GetCap";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.addParameter(new QName("", "OptionsIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "CapabilitiesOut"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.addParameter(new QName("", "SuppMsgOut"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "short"));
        //operation.setReturnClass(short.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call, "");
        //extractAttachments(call);
        //Map<?,?> outputParameters = call.getOutputParams();

        //// Check call status
        //short statusCode = ((Short) response).shortValue();

        //// Supported options
        //// String optionsInXml = (String) outputParameters.get(new QName("", "OptionsIn"));

        //// Messge support
        //String message = (String) outputParameters.get(new QName("", "SuppMsgOut"));

        //// Capabilities
        //String capabilitiesOutXml = (String) outputParameters.get(new QName("", "CapabilitiesOut"));

        return new WitsmlResponse(capabilitiesOutXml, (int) statusCode, message);
    }

    /**
     * Make a query to the WITSML server.
     *
     * @param wmlType  Object type to return. Non-null.
     * @param queryXml Query (an XML string). Non-null.
     * @return         Result of query (an XML string).
     */
    [MethodImpl(MethodImplOptions.Synchronized)] 
 internal WitsmlResponse get(String wmlType, String queryXml){ //throws RemoteException {
        if (wmlType == null)
            throw new ArgumentException("wmlType cannot be null");
        if (queryXml == null)
            throw new ArgumentException("queryXml cannot be null");

        string responseXml, message; 
        short statusCode = service.WMLS_GetFromStore(wmlType, queryXml, "", "", out responseXml, out message); 


        //// The WITSML command string
        //sealed String command = "WMLS_GetFromStore";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.addParameter(new QName("", "WMLtypeIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "QueryIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "OptionsIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "CapabilitiesIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "XMLout"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.addParameter(new QName("", "SuppMsgOut"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "short"));
        //operation.setReturnClass(short.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call, wmlType, queryXml, "", clientCapabilitiesXml);

        //// Check call status
        //short statusCode = ((Short) response).shortValue();

        //extractAttachments(call);
        //Map<?,?> outputParameters = call.getOutputParams();

        //// Log the messages
        //String message = (String) outputParameters.get(new QName("", "SuppMsgOut"));

        //// Return the response
        //String responseXml = (String) outputParameters.get(new QName("", "XMLout"));

        return new WitsmlResponse(responseXml, (int) statusCode, message);
    }

    /**
     * Delete an instance from the WITSML server.
     *
     * @param wmlType   Object type to add. Non-null.
     * @param deleteXml XML describing the instance to delete. Non-null.
     * @return          Server response. Never null.
     */
    //[MethodImpl(MethodImplOptions.Synchronized)] 
internal WitsmlResponse delete(String wmlType, String deleteXml)
       { //throws RemoteException {
        //Debug.Assert(wmlType != null : "wmlType cannot be null";
        //Debug.Assert(deleteXml != null : "deleteXml cannot be null";
        if (wmlType == null)
            throw new ArgumentException("wmlType cannot be null");
        if (deleteXml == null)
            throw new ArgumentException("deleteXml cannot be null");

        string message; 
        short statusCode = service.WMLS_DeleteFromStore(wmlType, deleteXml, "","",out message); 


        //// The WITSML command string
        //sealed String command = "WMLS_DeleteFromStore";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.addParameter(new QName("", "WMLtypeIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "QueryIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "OptionsIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "CapabilitiesIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "SuppMsgOut"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "short"));
        //operation.setReturnClass(short.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call, wmlType, deleteXml, "", clientCapabilitiesXml);

        //// Check call status
        //short statusCode = ((Short) response).shortValue();

        //extractAttachments(call);
        //Map<?,?> outputParameters = call.getOutputParams();

        //// Log the messages
        //String message = (String) outputParameters.get(new QName("", "SuppMsgOut"));

        return new WitsmlResponse(null, (int) statusCode, message);
     }

    /**
     * Add an instance to the WITSML server.
     *
     * @param wmlType  Object type to add. Non-null.
     * @param queryXml XML describing the instance to add. Non-null.
     * @return         Server response. Never null.
     */
    //[MethodImpl(MethodImplOptions.Synchronized)] 
 WitsmlResponse add(String wmlType, String addXml){ //throws RemoteException {
        //Debug.Assert(wmlType != null : "wmlType cannot be null";
        //Debug.Assert(addXml != null : "addXml cannot be null";
        if (wmlType == null)
            throw new ArgumentException("wmlType cannot be null");
        if (addXml == null)
            throw new ArgumentException("addXml cannot be null");

        string message;
        short statusCode = service.WMLS_AddToStore(wmlType, addXml, "", "", out message); 

        // The WITSML command string
        //sealed String command = "WMLS_AddToStore";

        //// Establish the request
        //OperationDesc operation = new OperationDesc();
        //operation.setName(command);
        //operation.addParameter(new QName("", "WMLtypeIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "XMLin"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "OptionsIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "CapabilitiesIn"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.IN,
        //        false, false);
        //operation.addParameter(new QName("", "SuppMsgOut"), new QName(
        //        "http://www.w3.org/2001/XMLSchema", "string"), String.class, ParameterDesc.OUT,
        //        false, false);
        //operation.setReturnType(new QName("http://www.w3.org/2001/XMLSchema", "short"));
        //operation.setReturnClass(short.class);
        //operation.setReturnQName(new QName("", "Result"));

        //// Make the call
        //Call call = createCall(operation, command);
        //Object response = invokeCall(call, wmlType, addXml, "", clientCapabilitiesXml);

        //// Check call status
        //short statusCode = ((Short) response).shortValue();

        //extractAttachments(call);
        //Map<?,?> outputParameters = call.getOutputParams();

        //// Log the messages
        //String message = (String) outputParameters.get(new QName("", "SuppMsgOut"));

        return new WitsmlResponse(null, (int) statusCode, message);
     }
}
}