namespace Sporacid.Simplets.Webapp.Services.WebApi2.Description
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;
    using System.Xml.XPath;

    /// <summary>
    /// A custom <see cref="IDocumentationProvider" /> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider
    {
        private const String TypeExpression = "/doc/members/member[@name='T:{0}']";
        private const String MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const String PropertyExpression = "/doc/members/member[@name='P:{0}']";
        private const String FieldExpression = "/doc/members/member[@name='F:{0}']";
        private const String ParameterExpression = "param[@name='{0}']";
        private readonly XPathNavigator _documentNavigator;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationProvider" /> class.
        /// </summary>
        /// <param name="documentPath">The physical path to XML document.</param>
        public XmlDocumentationProvider(String documentPath)
        {
            if (documentPath == null)
            {
                throw new ArgumentNullException("documentPath");
            }
            var xpath = new XPathDocument(documentPath);
            this._documentNavigator = xpath.CreateNavigator();
        }

        public String GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            var typeNode = this.GetTypeNode(controllerDescriptor.ControllerType);
            return GetTagValue(typeNode, "summary");
        }

        public virtual String GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var methodNode = this.GetMethodNode(actionDescriptor);
            return GetTagValue(methodNode, "summary");
        }

        public virtual String GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (reflectedParameterDescriptor != null)
            {
                var methodNode = this.GetMethodNode(reflectedParameterDescriptor.ActionDescriptor);
                if (methodNode != null)
                {
                    var parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    var parameterNode = methodNode.SelectSingleNode(String.Format(CultureInfo.InvariantCulture, ParameterExpression, parameterName));
                    if (parameterNode != null)
                    {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return null;
        }

        public String GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var methodNode = this.GetMethodNode(actionDescriptor);
            return GetTagValue(methodNode, "returns");
        }

        public String GetDocumentation(MemberInfo member)
        {
            var memberName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(member.DeclaringType), member.Name);
            var expression = member.MemberType == MemberTypes.Field ? FieldExpression : PropertyExpression;
            var selectExpression = String.Format(CultureInfo.InvariantCulture, expression, memberName);
            var propertyNode = this._documentNavigator.SelectSingleNode(selectExpression);
            return GetTagValue(propertyNode, "summary");
        }

        public String GetDocumentation(Type type)
        {
            var typeNode = this.GetTypeNode(type);
            return GetTagValue(typeNode, "summary");
        }

        private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor)
        {
            var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor != null)
            {
                var selectExpression = String.Format(CultureInfo.InvariantCulture, MethodExpression, GetMemberName(reflectedActionDescriptor.MethodInfo));
                return this._documentNavigator.SelectSingleNode(selectExpression);
            }

            return null;
        }

        private static String GetMemberName(MethodInfo method)
        {
            var name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(method.DeclaringType), method.Name);
            var parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                var parameterTypeNames = parameters.Select(param => GetTypeName(param.ParameterType)).ToArray();
                name += String.Format(CultureInfo.InvariantCulture, "({0})", String.Join(",", parameterTypeNames));
            }

            return name;
        }

        private static String GetTagValue(XPathNavigator parentNode, String tagName)
        {
            if (parentNode != null)
            {
                var node = parentNode.SelectSingleNode(tagName);
                if (node != null)
                {
                    return node.Value.Trim();
                }
            }

            return null;
        }

        private XPathNavigator GetTypeNode(Type type)
        {
            var controllerTypeName = GetTypeName(type);
            var selectExpression = String.Format(CultureInfo.InvariantCulture, TypeExpression, controllerTypeName);
            return this._documentNavigator.SelectSingleNode(selectExpression);
        }

        private static String GetTypeName(Type type)
        {
            var name = type.FullName;
            if (type.IsGenericType)
            {
                // Format the generic type name to something like: Generic{System.Int32,System.String}
                var genericType = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();
                var genericTypeName = genericType.FullName;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                var argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                name = String.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", genericTypeName, String.Join(",", argumentTypeNames));
            }
            if (type.IsNested)
            {
                // Changing the nested type name from OuterType+InnerType to OuterType.InnerType to match the XML documentation syntax.
                name = name.Replace("+", ".");
            }

            return name;
        }
    }
}