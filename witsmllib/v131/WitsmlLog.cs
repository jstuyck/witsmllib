using System;
using witsmllib.util;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace witsmllib.v131
{	
	sealed class WitsmlLog : witsmllib.WitsmlLog
	{

		/// <summary>
		/// Create a new WITSML log instance.
		/// </summary>
		/// <param name="server">Server this instance lives within. Non-null.</param>
		/// <param name="id">ID of instance. May be null.</param>
		/// <param name="name">Name of instance. May be null.</param>
		/// <param name="parent">Parent of instance. May be null.</param>
		/// <param name="parentId">ID of parent instance. May be null.</param>
		private WitsmlLog(WitsmlServer server, String id, String name,
						  WitsmlObject parent, String parentId)
		
			:base(server, id, name, parent, parentId)
			{}

		
		/// <summary>
		/// Factory method for this type.
		/// </summary>
		/// <param name="server">Server the new instance lives within. Non-null.</param>
		/// <param name="parent">Parent instance. May be null.</param>
		/// <param name="element">XML element to create instance from. Non-null.</param>
		/// <returns>New WITSML log instance. Never null.</returns>
		static WitsmlObject newInstance(WitsmlServer server, WitsmlObject parent,
										XElement element)
		{
			if (server == null)
				throw new ArgumentNullException("server cannot be null");
			if (element == null)
				throw new ArgumentNullException("element cannot be null");			

			String id = element.Attribute("uid").Value;
			String parentId = element.Attribute("uidWellbore").Value;
			String name = element.Element(element.Name.Namespace +"name").Value.Trim(); //, element.getNamespace());

			WitsmlLog log = new WitsmlLog(server, id, name, parent, parentId);
			log.update(element);

			return log;
		}

		/// <summary>
		/// Return complete XML query for this type.
		/// </summary>
		/// <param name="logId">ID of instance to get. May be empty to indicate all. Non-null.</param>
		/// <param name="parentId">Parent IDs. Closest first. May be empty if instances are accessed from the root. Non-null.</param>
		/// <returns>XML query. Never null.</returns>
		static String getQuery(String logId, params String[] parentId)
		{
			if (logId == null)
				throw new ArgumentNullException("id cannot be null");
			if (parentId == null)
				throw new ArgumentNullException("parentId cannot be null");

			string uidWell;
			string uidWellbore;                    
			string mnemonic;
			string startIndex = "";
			
			if (parentId.Length == 4)
			{
				uidWell = parentId[3];
				uidWellbore = parentId[2];
				logId = parentId[1];
				mnemonic = parentId[0];
			   
			}
			else
			{
				uidWellbore = parentId.Length > 0 ? parentId[0] : "";
				uidWell = parentId.Length > 1 ? parentId[1] : "";
				mnemonic = "";
			}


			String query = "<logs xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
						   "  <log uidWell = \"" + uidWell + "\"" +
						   "       uidWellbore = \"" + uidWellbore + "\"" +
						   "       uid = \"" + logId + "\">" +
						   "    <name/>" +
						   "    <nameWell/>" +
						   "    <nameWellbore/>" +
						   "    <objectGrowing/>" +
						   "    <dataRowCount/>" +
						   "    <serviceCompany/>" +
						   "    <runNumber/>" +
						   "    <bhaRunNumber/>" +
						   "    <pass/>" +
						   "    <creationDate/>" +
						   "    <description/>" +
						   "    <indexType/>" +
						   "    <startIndex uom=\"" + WitsmlServer.distUom + "\">" + startIndex + "</startIndex>" +
						   "    <endIndex uom=\"" + WitsmlServer.distUom + "\"/>" +
						   "    <indexCurve/>" +
						   "    <startDateTimeIndex/>" +
						   "    <endDateTimeIndex/>" +
						   "    <stepIncrement/>" +
						   "    <direction/>" +
						   "    <nullValue/>" +
						   "    <logParam/>" +
						   WitsmlLogCurve.getQuery() +
						   "    <logData>" +
						   "      <data/>" +
						   "    </logData>" +
						   WitsmlCommonData.getQuery() +
						   "  </log>" +
						   "</logs>";


			//String query = "<logs xmlns=\"" + WitsmlVersion.VERSION_1_3_1.getNamespace() + "\">" +
			//           "  <log uidWell = \"" + uidWell + "\"" +                      
			//           "       uidWellbore = \"" + uidWellbore + "\"" +
			//           "       uid = \"" + logId + "\">" +
			//            "    <name/>" +
			//            "    <nameWell/>" +
			//            "    <nameWellbore/>" +
			//            "    <objectGrowing/>" +
			//            "    <dataRowCount/>" +
			//            "    <serviceCompany/>" +
			//            "    <runNumber/>" +
			//            "    <bhaRunNumber/>" +
			//            "    <pass/>" +
			//            "    <creationDate/>" +
			//            "    <description/>" +
			//            "    <indexType/>" +
			//             "    <indexCurve/>" +
			//            "    <startDateTimeIndex/>" +
			//            "    <endDateTimeIndex/>" +
			//            "    <stepIncrement/>" +
			//            "    <direction/>" +
			//            "    <nullValue/>" +
			//            "    <logParam/>" +
			//           WitsmlLogCurve.getQuery(mnemonic) +
			//           WitsmlCommonData.getQuery() +
			//           "  </log>" +
			//           "</logs>";          

			return query;
		}

		/// <summary>
		/// Parse the specified DOM element and instantiate the properties of this instance.
		/// </summary>
		/// <param name="element">element  XML element to parse. Non-null.</param>
		void update(XElement element)
		{ 
			if (element == null)
				throw new ArgumentNullException("element cannot be null");

			// Remove current bulk data
			foreach (witsmllib.WitsmlLogCurve curve in curves)
				((WitsmlLogCurve)curve).clear();

			// Common data
			XElement commonDataElement = element.Element(element.Name.Namespace + "commonData");//, element.getNamespace());
			if (commonDataElement != null)
				commonData = new WitsmlCommonData(commonDataElement);

			// Index
			indexType = XmlUtil.update(element, "indexType", indexType);
			if (indexType != null)
			{
				if (indexType.ToLower().Contains("time"))
				{
					startIndex = getIndex(XmlUtil.update(element, "startDateTimeIndex", (String)null));
					endIndex = getIndex(XmlUtil.update(element, "endDateTimeIndex", (String)null));
				}
				else
				{
					startIndex = getIndex(XmlUtil.update(element, "startIndex", (String)null));
					endIndex = getIndex(XmlUtil.update(element, "endIndex", (String)null));
				}
			}

			indexCurveName = XmlUtil.update(element, "indexCurve", indexCurveName);
			_isGrowing = XmlUtil.update(element, "objectGrowing", _isGrowing);
			nRows = XmlUtil.update(element, "dataRowCount", nRows);
			serviceCompany = XmlUtil.update(element, "serviceCompany", serviceCompany);
			runNumber = XmlUtil.update(element, "runNumber", runNumber);
			bhaRunNumber = XmlUtil.update(element, "bhaRunNumber", bhaRunNumber);
			pass = XmlUtil.update(element, "pass", pass);
			creationTime = XmlUtil.update(element, "creationDate", creationTime);
			description = XmlUtil.update(element, "description", description);
			direction = XmlUtil.update(element, "direction", direction);
			stepIncrement = XmlUtil.update(element, "stepIncrement", stepIncrement);
			indexUnit = XmlUtil.update(element, "indexUnits", indexUnit);
			noValue = XmlUtil.update(element, "nullValue", noValue);
			unitNamingSystem = XmlUtil.update(element, "uomNamingSystem", unitNamingSystem);
			comment = XmlUtil.update(element, "otherData", comment);

			// Curve data
			var logCurveInfoElements = element.Elements(element.Name.Namespace + "logCurveInfo");//, element.getNamespace());
			foreach (Object e in logCurveInfoElements)
			{
				XElement logCurveInfoElement = (XElement)e;

				String curveName = XmlUtil.update(logCurveInfoElement, "mnemonic", (String)null);
				Int32? curveNo = XmlUtil.update(logCurveInfoElement, "columnIndex", (Int32?)null);

				WitsmlLogCurve curve = (WitsmlLogCurve)findCurve(curveName);
				if (curve == null)
				{
					curve = new WitsmlLogCurve(this, curveName, curveNo.Value );
					curves.Add(curve);
				}

				curve.update(logCurveInfoElement);
			}

			// Bulk data
			XElement logDataElement = element.Element(element.Name.Namespace +"logData"); //, element.getNamespace());
			if (logDataElement != null)
			{
				var dataElements = logDataElement.Elements(element.Name.Namespace + "data");//, element.getNamespace());

				//for (var j = dataElements.iterator(); j.hasNext(); )
				foreach (var j in dataElements)
				{
					XElement dataElement = (XElement)j;//.next();

					String valueString = dataElement.Value.Trim(); //.getTextTrim();

					String[] tokens = valueString.Split(dataDelimiter);//, -1);
					for (int i = 0; i < tokens.Length; i++)
					{
						String token = tokens[i];
						if (token.Equals(noValue))
							token = "";

						WitsmlLogCurve curve = (witsmllib.v131.WitsmlLogCurve)findCurve(i + 1);
						curve.addValue(token);
					}

					// Handle missing log values
					for (int i = tokens.Length; i < getNCurves(); i++)
					{
						WitsmlLogCurve curve = (witsmllib.v131.WitsmlLogCurve)findCurve(i + 1);
						curve.addValue(null);
					}
				}
			}
			else //We didn't get the Curves, we have to ask again, because server does not support multi log and logData request at the same time.
			{            
				if (this.curves.Where(x => x.getValues().Count() == 0).Count() > 0)
				{
					witsmllib.WitsmlLog newLogObject = base.getWitsmlServer().getOne<witsmllib.WitsmlLog>(this, new WitsmlQuery(), this.getId());
					foreach (var item in newLogObject.getCurves())
					{
						int index = item.getCurveNo();
						if (this.curves[index - 1].getValues().Count() == 0)
						{
							this.curves[index - 1] = item;                            
						}
					}
				}

				if (this.curves.Where(x => x.getValues().Count() == 0).Count() > 0)
					throw new Exception("Count not get all Curve Object for this curve");
																			
			}
			unitConvert();
		}
	}
}
