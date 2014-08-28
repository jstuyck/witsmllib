using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using witsmllib.util;
using System.Xml.Linq;

namespace witsmllib
{
    /// <summary>
    /// Class for controlling the WITSML queries.
    /// The WitsmlQuery instance is used to select which elements that should be
    /// included in a server query and to set query constraints.
    /// </summary>
    public sealed class WitsmlQuery
    {

        /** Elements to explicitly include. */
        private List<String> includedElements = new List<String>();

        /** Elements to explicitly exclude. */
        private List<String> excludedElements = new List<String>();

        /** Constraints to apply. Each entry is element of two: element,value. */
        private List<ElementConstraint> elementConstraints = new List<ElementConstraint>();

        /** Constraints to apply. Each entry is element of two: element,value. */
        private List<AttributeConstraint> attributeConstraints = new List<AttributeConstraint>();

        /// <summary>
        /// Create a default WITSML query containing all the elements 
        /// specified by the given WITSML type and without any constraints.
        /// </summary>
        public WitsmlQuery()
        {
            // Nothing
        }

        /// <summary>
        /// Explicitly include the specified element from the query. As soon
        /// elements are explicitly included, no elements are included be default.
        /// </summary>
        /// <param name="elementName">Name of element to include. Non-null.</param>
        public void includeElement(String elementName)
        {
            if (elementName == null)
                throw new ArgumentException("elementName cannot be null");

            includedElements.Add(elementName);
        }

        /// <summary>
        /// Explicitly exclude the specified element from the query.
        /// </summary>
        /// <param name="elementName">Name of element to exclude. Non-null.</param>
        public void excludeElement(String elementName)
        {
            if (elementName == null)
                throw new ArgumentException("elementName cannot be null");

            excludedElements.Add(elementName);
        }

        /// <summary>
        /// Add the specified element constraint to this query.
        /// 
        /// The same element may be constrained more than once, resulting in a
        /// deep duplication of the element with the new constraint applied.
        /// </summary>
        /// <param name="elementName">Name of element to constrain. Non-null.</param>
        /// <param name="value">The element constraint. May be null to indicate empty.</param>
        public void addElementConstraint(String elementName, Object value)
        {
            if (elementName == null)
                throw new ArgumentException("elementName cannot be null");

            elementConstraints.Add(new ElementConstraint(elementName, value));
        }

        /// <summary>
        /// Add the specified attribute constraint to this query.
        /// </summary>
        /// <param name="elementName">Name of element to constrain. Non-null.</param>
        /// <param name="attributeName">Name of attribute of element to constrain. Non-null.</param>
        /// <param name="value">The attribute constraint. May be null it indicate empty.</param>
        public void addAttributeConstraint(String elementName, String attributeName,
                                           Object value)
        {
            if (elementName == null)
                throw new ArgumentException("elementName cannot be null");

            if (attributeName == null)
                throw new ArgumentException("attributeName cannot be null");

            attributeConstraints.Add(new AttributeConstraint(elementName, attributeName, value));
        }

        /// <summary>
        /// Find element with the specified name.
        /// </summary>
        /// <param name="root">Root element of where to start the search. Non-null.</param>
        /// <param name="name">Name of element to find. Non-null.</param>
        /// <returns></returns>
        private static XElement findElement(XElement root, String name)
        {
            if (root == null)
                throw new ArgumentException("root cannot be null");
            if (name == null)
                throw new ArgumentException("name cannot be null");
           
            foreach (XElement element in root.Descendants())
            {
                if (element.Name.Equals(name))
                    return element;
            }
            return null;
        }

        /// <summary>
        /// Check if a certain element should be included or not.
        /// </summary>
        /// <param name="element">XElement to check. Non-null.</param>
        /// <returns>True if the element should be included, false otherwise.</returns>
        private bool shouldInclude(XElement element)
        {
            if (element == null)
                throw new ArgumentException("element cannot be null");

            // XElement is included by default if none is explicitly included
            bool isDefaultIncluded = includedElements.Count == 0;// .IsEmpty();

            // XElement is included if itself or any parent is explicitly included
            bool isExplicitlyIncluded = false;
            XElement e = element;
            while (e != null)
            {
                if (includedElements.Contains(e.Name.LocalName)) //.getName()))
                    isExplicitlyIncluded = true;
                e = e.Parent; //.getParentElement();
            }

            // XElement is excluded if itself or any parent is explicitly excluded
            bool isExplicitlyExcluded = false;
            e = element;
            while (e != null)
            {
                if (excludedElements.Contains(e.Name.LocalName)) //.getName()))
                    isExplicitlyExcluded = true;
                e = e.Parent; //.getParentElement();
            }

            bool isIncluded = !isExplicitlyExcluded &&
                                 (isExplicitlyIncluded || isDefaultIncluded);
            return isIncluded;
        }

        /// <summary>
        /// Apply inclusions as specified in includedElements and
        /// excludedElements to the document rooted at the specified root
        ///  element.
        /// </summary>
        /// <param name="root">Root element of tree to apply inclusions/exclusions to. Non-null.</param>
        private void applyInclusions(XElement root)
        {
            if (root == null)
                throw new ArgumentException("root cannot be null");

            // Make a set of all elements
            HashSet<XElement> elementsToDelete = new HashSet<XElement>();
            foreach (var i in root.Descendants()) //.getDescendants(new ElementFilter()); i.hasNext(); )
                elementsToDelete.Add((XElement)i);  //.next());

            // Loop all elements, and possibly remove from the delete set
            //for (var i = root.getDescendants(new ElementFilter()); i.hasNext(); )
            foreach (XElement element in root.Descendants())
            {
                // If element should remain, include it and all its parents
                if (shouldInclude(element))
                {
                    XElement e = element;
                    while (e != null)
                    {
                        elementsToDelete.Remove(e); //.remove(e);
                        e = e.Parent; //.getParentElement();
                    }
                }
            }

            // Remove unwanted elements
            foreach (XElement element in elementsToDelete)
                element.Remove(); //.detach();
        }

        /**
         * Apply element constraints.
         *
         * @param root  Root element of tree to apply constraints to.
         *              Non-null.
         */
        private void applyElementConstraints(XElement root)
        {
            //Debug.Assert(root != null : "root cannot be null";

            // Loop over all constraints
            foreach (ElementConstraint constraint in elementConstraints)
            {
                String elementName = constraint.getElementName();

                // Find the corresponding element
                XElement element = findElement(root, elementName);
                if (element == null)
                    continue;

                String value = constraint.getText();

                // If the element has not yet been constrained, we
                // constrain it by applying the value as text
                if (element.Value.Length == 0)
                    element.Value = value; //.setText(value);

                // If the element has already been constrained., we
                // (deep-) clone the parent and apply the constraint
                // on the clone.
                else
                {
                    XElement parent = element.Parent; //.getParentElement();
                    XElement clone = new XElement(parent); // (XElement)parent.clone();
                    parent.Parent.Add(clone); //.addContent(clone);

                    XElement newElement = findElement(clone, elementName);
                    newElement.Value = value; //.setText(value);
                }
            }
        }

        /**
         * Apply element constraints.
         *
         * @param root  Root element of tree to apply constraints to.
         *              Non-null.
         */
        private void applyAttributeConstraints(XElement root)
        {
            //Debug.Assert(root != null : "root cannot be null";

            // Loop over all constraints
            foreach (AttributeConstraint constraint in attributeConstraints)
            {
                String elementName = constraint.getElementName();

                // Find the corresponding element
                XElement element = findElement(root, elementName);
                if (element == null)
                    continue;

                String attributeName = constraint.getAttributeName();

                XAttribute attribute = element.Attribute(attributeName);
                if (attribute == null)
                    continue;

                attribute.SetValue(constraint.getText());
            }
        }

        /**
         * Apply the given restrictions to the specified XML.
         *
         * @param queryXml  XML to apply restrictions to. Non-null.
         * @return          A (possibly) modified XML. null if
         *                  the parse process failed for some reason.
         */
        internal String apply(String queryXml)
        {
            //Debug.Assert(queryXml != null : "queryXml cannot be null";

            // Make a DOM tree of the XML
            //SAXBuilder builder = new SAXBuilder();

            try
            {
                XDocument document = XDocument.Load(new StringReader(queryXml));
                XElement root = document.Root;

                // Modify the DOM tree according to inclusions/exclusions and constraints
                applyInclusions(root);
                applyElementConstraints(root);
                applyAttributeConstraints(root);

                // Convert back to an XML string
                String xml = root.ToString(); // (new XMLOutputter()).outputString(root.getDocument());
                return xml;
            }
            catch (IOException exception)
            {
                // Programming error
                Console.Write(exception.StackTrace); // exception.printStackTrace();
                //Debug.Assert(false : "Unable to create XML document: " + queryXml;
                return null;
            }
            catch (Exception /*JDOMException*/ exception)
            {
                // Programming error
                //exception.printStackTrace();
                Console.Write(exception.StackTrace);
                //Debug.Assert(false : "Unable to parse XML document: " + queryXml;
                return null;
            }
        }

        /** {@inheritDoc} */

        public override String ToString()
        {
            StringBuilder s = new StringBuilder();

            s.Append("Include elements:\n");
            foreach (String element in includedElements)
                s.Append("  <" + element + "/>\n");

            s.Append("Exclude elements:\n");
            foreach (String element in excludedElements)
                s.Append("  <" + element + "/>\n");

            s.Append("Constraints:\n");
            foreach (ElementConstraint constraint in elementConstraints)
                s.Append("  " + constraint + "\n");

            foreach (AttributeConstraint constraint in attributeConstraints)
                s.Append("  " + constraint + "\n");

            return s.ToString();
        }


        /**
         * Class for representing an element constraint.
         *
         * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
         */
        private /*static*/ class ElementConstraint
        {
            /** Name of element being constrained. Non-null. */
            protected String elementName;

            /** Value to constrain element with. May be null for unconstrined. */
            protected Object value;

            /**
             * Create a new element constraint.
             *
             * @param elementName  XElement being constrained. Non-null.
             * @param value        Value to constrin element with. Null to leave
             *                     unconstrined.
             */
            internal ElementConstraint(String elementName, Object value)
            {
                //Debug.Assert(elementName != null : "elementName cannot be null";

                this.elementName = elementName;
                this.value = value;
            }

            /**
             * Return name of element being constrined.
             *
             * @return  Name of element being constrained. Never null.
             */
            internal String getElementName()
            {
                return elementName;
            }

            /**
             * Return the string representing the constrining value.
             *
             * @return  String representing the constrining value. Never null.
             */
            internal String getText()
            {
                if (value == null)
                    return "";

                if (value is DateTime)
                {
                    DateTime? time = (DateTime)value;
                    return time.ToString();
                }

                return value.ToString();
            }

            /** {@inheritDoc} */

            public override String ToString()
            {
                return "<" + elementName + ">" + getText() + "</" + elementName + ">";
            }
        }

        /**
         * Class for representing an attribute constraint.
         *
         * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
         */
        private /*static*/ class AttributeConstraint : ElementConstraint
        {

            /** Attribute being constrined. */
            private String attributeName;

            /**
             * Create a new attribute constraint.
             *
             * @param elementName   Name of element of attribute being constrained. Non-null.
             * @param attributeName Name of attribute being constrained. Non-null.
             * @param value         Value to constrin element with. Null to leave
             *                      unconstrined.
             */
            internal AttributeConstraint(String elementName, String attributeName, Object value)
                : base(elementName, value)
            {


                //Debug.Assert(elementName != null : "elementName cannot be null";
                //Debug.Assert(attributeName != null : "attributeName cannot be null";

                this.attributeName = attributeName;
            }

            /**
             * Return name of attribute being constrained.
             *
             * @return  Name of attribute being constrained. Never null.
             */
            internal String getAttributeName()
            {
                return attributeName;
            }

            /** {@inheritDoc} */

            public override String ToString()
            {
                return "<" + elementName + " " + attributeName + "=" + getText() + "/>";
            }
        }
    }
}