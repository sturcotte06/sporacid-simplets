namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Tools.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ClaimsByActionDictionary : Dictionary<String, Claims>
    {
        public ClaimsByActionDictionary(Assembly assembly, String[] endpointsNamespaces)
        {
            this.Initialize(assembly, endpointsNamespaces);
        }

        /// <summary>
        /// </summary>
        /// <param name="moduleAttr"></param>
        /// <param name="serviceMethod"></param>
        /// <returns></returns>
        public String CreateKey(ModuleAttribute moduleAttr, MethodInfo serviceMethod)
        {
            return String.Format("{0}.{1}", moduleAttr.Name, serviceMethod);
        }

        /// <summary>
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="endpointsNamespaces"></param>
        private void Initialize(Assembly assembly, params String[] endpointsNamespaces)
        {
            // Get all endpoint types.
            var endpointTypes = from type in assembly.GetTypes()
                where (type.IsClass || type.IsInterface) &&
                      endpointsNamespaces.Contains(type.Namespace)
                select type;

            // For each of them, cache authorization configuration.
            foreach (var endpointType in endpointTypes)
            {
                var moduleAttr = endpointType.GetAllCustomAttributes<ModuleAttribute>().FirstOrDefault();
                if (moduleAttr == null)
                {
                    continue;
                }

                var endpointTypeMethods = from method in endpointType.GetMethods()
                    select method;

                foreach (var endpointTypeMethod in endpointTypeMethods)
                {
                    var requiredClaimsAttr = endpointTypeMethod.GetCustomAttributes<RequiredClaimsAttribute>(true).FirstOrDefault();
                    if (requiredClaimsAttr != null)
                    {
                        // Endpoint actions.
                        var actionName = this.CreateKey(moduleAttr, endpointTypeMethod);
                        if (this.ContainsKey(actionName))
                        {
                            // Combine claims.
                            Claims claims;
                            this.TryGetValue(actionName, out claims);
                            this.Remove(actionName);
                            this.Add(actionName, claims | requiredClaimsAttr.RequiredClaims);
                        }
                        else
                        {
                            this.Add(actionName, requiredClaimsAttr.RequiredClaims);
                        }
                    }
                }
            }
        }
    }
}