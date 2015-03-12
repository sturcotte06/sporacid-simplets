namespace Sporacid.Simplets.Webapp.Tools.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// Taken From: http://stackoverflow.com/questions/540749/can-a-c-sharp-class-inherit-attributes-from-its-interface
    /// <authors>tanascius</authors>
    /// <version>1.9.0</version>
    public static class ReflectionExtensions
    {
        /// <summary>Searches and returns attributes. The inheritance chain is used to find the attributes.</summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="type">The type which is searched for the attributes.</param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetAllCustomAttributes<T>(this Type type) where T : Attribute
        {
            return GetCustomAttributes(type, typeof (T), true).Select(arg => (T) arg).ToArray();
        }

        /// <summary>Searches and returns attributes.</summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="type">The type which is searched for the attributes.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attributes. Interfaces
        /// will be searched, too.
        /// </param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetCustomAttributes<T>(this Type type, bool inherit) where T : Attribute
        {
            return GetCustomAttributes(type, typeof (T), inherit).Select(arg => (T) arg).ToArray();
        }

        /// <summary>Private helper for searching attributes.</summary>
        /// <param name="type">The type which is searched for the attribute.</param>
        /// <param name="attributeType">The type of attribute to search for.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attribute. Interfaces
        /// will be searched, too.
        /// </param>
        /// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
        private static object[] GetCustomAttributes(Type type, Type attributeType, bool inherit)
        {
            if (!inherit)
            {
                return type.GetCustomAttributes(attributeType, false);
            }

            var attributeCollection = new Collection<object>();
            var baseType = type;

            do
            {
                baseType.GetCustomAttributes(attributeType, true).ForEach(attributeCollection.Add);
                baseType = baseType.BaseType;
            } while (baseType != null);

            foreach (var interfaceType in type.GetInterfaces())
            {
                GetCustomAttributes(interfaceType, attributeType, true).ForEach(attributeCollection.Add);
            }

            var attributeArray = new object[attributeCollection.Count];
            attributeCollection.CopyTo(attributeArray, 0);
            return attributeArray;
        }

        /// <summary>Searches and returns attributes. The inheritance chain is used to find the attributes.</summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="method">The method which is searched for the attributes.</param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetAllCustomAttributes<T>(this MethodBase method) where T : Attribute
        {
            return GetCustomAttributes(method, typeof (T), true).Select(arg => (T) arg).ToArray();
        }

        /// <summary>Searches and returns attributes.</summary>
        /// <typeparam name="T">The method of attribute to search for.</typeparam>
        /// <param name="method">The type which is searched for the attributes.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attributes. Interfaces
        /// will be searched, too.
        /// </param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetCustomAttributes<T>(this MethodBase method, bool inherit) where T : Attribute
        {
            return GetCustomAttributes(method, typeof (T), inherit).Select(arg => (T) arg).ToArray();
        }

        /// <summary>Private helper for searching attributes.</summary>
        /// <param name="method">The method which is searched for the attribute.</param>
        /// <param name="attributeType">The type of attribute to search for.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attribute. Interfaces
        /// will be searched, too.
        /// </param>
        /// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
        private static object[] GetCustomAttributes(MethodBase method, Type attributeType, bool inherit)
        {
            var type = method.DeclaringType;

            if (!inherit || type == null)
            {
                return method.GetCustomAttributes(attributeType, false);
            }

            var baseType = type;
            var attributes = new List<object>();

            while (baseType != null)
            {
                var baseMethod = baseType.GetMethod(method.Name, method.GetParameters().Select(mi => mi.ParameterType).ToArray());
                if (baseMethod == null)
                    break;

                baseMethod.GetCustomAttributes(attributeType, true).ForEach(attributes.Add);
                baseType = baseType.BaseType;
            }

            foreach (var interfaceType in type.GetInterfaces())
            {
                var baseMethod = interfaceType.GetMethod(method.Name, method.GetParameters().Select(mi => mi.ParameterType).ToArray());
                if (baseMethod == null)
                    continue;

                GetCustomAttributes(baseMethod, attributeType, true).ForEach(attributes.Add);
            }

            //  var attributeArray = new object[attributeCollection.Count];
            //  attributeCollection.CopyTo(attributeArray, 0);
            return attributes.ToArray();
        }
    }
}