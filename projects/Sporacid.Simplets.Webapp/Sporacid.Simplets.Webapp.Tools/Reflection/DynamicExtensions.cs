namespace Sporacid.Simplets.Webapp.Tools.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;

    /// <summary>
    /// Extension method library for dynamic objects.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public static class DynamicExtensions
    {
        /// <summary>
        /// Wraps an object into a dynamic object.
        /// </summary>
        /// <param name="obj">The object to wrap.</param>
        /// <returns>The dynamic object wrapper.</returns>
        public static ExpandoObject ToDynamic(this Object obj)
        {
            // Create a new expando object.
            IDictionary<String, Object> expando = new ExpandoObject();

            // For each properties of the original object, add it to the expando.
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj.GetType()))
            {
                expando.Add(property.Name, property.GetValue(obj));
            }

            return expando as ExpandoObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="toInclude"></param>
        /// <returns></returns>
        public static ExpandoObject Include(this ExpandoObject obj, Object toInclude)
        {
            // Make sure the object is an expando object. If not, wraps it into a dynamic.
            var dynamicObj = obj as ExpandoObject ?? obj.ToDynamic();
            var dynamicObjDict = (IDictionary<String, Object>) dynamicObj;

            // For each properties of the object to include, add it to the expando.
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(toInclude.GetType()))
            {
                dynamicObjDict.Add(property.Name, property.GetValue(toInclude));
            }

            return dynamicObj;
        }
    }
}