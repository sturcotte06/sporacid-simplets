namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Http.Services;
    using Ninject;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Tools.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        // private readonly IAuthorizationModule authorizationModule;
        // private readonly Dictionary<String, Claims> claimsByAction;
        // 
        // public AuthorizationFilter(IAuthorizationModule authorizationModule)
        // {
        //     this.authorizationModule = authorizationModule;
        //     this.claimsByAction = new Dictionary<String, Claims>();
        //     this.Initialize(Assembly.GetExecutingAssembly(), "Sporacid.Simplets.Webapp.Services.Services");
        // }

        private readonly Dictionary<String, Claims> claimsByAction;
        private readonly IKernel kernel;

        public AuthorizationFilter(IKernel kernel, String[] endpointsNamespaces)
        {
            this.kernel = kernel;
            this.claimsByAction = new Dictionary<String, Claims>();
            this.Initialize(Assembly.GetExecutingAssembly(), endpointsNamespaces);
        }

        /// <summary>
        /// Executes the authorization filter to synchronize.
        /// </summary>
        /// <returns>
        /// The authorization filter to synchronize.
        /// </returns>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token associated with the filter.</param>
        /// <param name="continuation">The continuation.</param>
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var serviceType = actionContext.ControllerContext.Controller.GetType();
            var serviceMethod = this.GetMethodInfoFromActionContext(actionContext);

            // Get the module attribute. This is a required key of the authorization system.
            var moduleAttr = serviceType.GetAllCustomAttributes<ModuleAttribute>().FirstOrDefault();
            if (moduleAttr == null)
            {
                throw new NotAuthorizedException(ExceptionStrings.Services_Security_NotConfiguredAction);
            }

            // Get the required claims attribute. This is a required key of the authorization system.
            Claims claims;
            var serviceActionName = this.GetServiceActionName(moduleAttr, serviceMethod);
            if (!this.claimsByAction.TryGetValue(serviceActionName, out claims))
            {
                throw new NotAuthorizedException(ExceptionStrings.Services_Security_NotConfiguredAction);
            }

            // Get an authorization module.
            var authorizationModule = this.kernel.Get<IAuthorizationModule>();

            // Get the context attribute. This is a required key of the authorization system.
            // Context can be fixed, or dynamic. Handle both cases.
            var fixedCtxAttr = serviceType.GetAllCustomAttributes<FixedContextAttribute>().FirstOrDefault();
            if (fixedCtxAttr != null)
            {
                // Fixed context.
                authorizationModule.Authorize(Thread.CurrentPrincipal, claims, moduleAttr.Name, fixedCtxAttr.Name);
            }
            else
            {
                // Dynamic context, get its value.
                var contextualAttr = serviceType.GetAllCustomAttributes<ContextualAttribute>().FirstOrDefault();
                if (contextualAttr == null)
                {
                    throw new NotAuthorizedException(ExceptionStrings.Services_Security_NotConfiguredAction);
                }

                var context = actionContext.Request.GetRouteData().Values[contextualAttr.ContextParameterName];
                if (context == null)
                {
                    throw new NotAuthorizedException(ExceptionStrings.Services_Security_NoContextualActionContext);
                }

                authorizationModule.Authorize(Thread.CurrentPrincipal, claims, moduleAttr.Name, context.ToString());
            }

            return continuation();
        }

        /// <summary>
        /// Gets or sets a value indicating whether more than one instance of the indicated attribute can be specified for a single
        /// program element.
        /// </summary>
        /// <returns>
        /// true if more than one instance is allowed to be specified; otherwise, false. The default is false.
        /// </returns>
        public bool AllowMultiple
        {
            get { return false; }
        }

        /// <summary>
        /// </summary>
        /// <param name="moduleAttr"></param>
        /// <param name="serviceMethod"></param>
        /// <returns></returns>
        private String GetServiceActionName(ModuleAttribute moduleAttr, MethodInfo serviceMethod)
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
                        var actionName = this.GetServiceActionName(moduleAttr, endpointTypeMethod);
                        this.claimsByAction.Add(actionName, requiredClaimsAttr.RequiredClaims);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the controller method info that is to be executed.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>The controller method info that is to be executed.</returns>
        private MethodInfo GetMethodInfoFromActionContext(HttpActionContext actionContext)
        {
            ReflectedHttpActionDescriptor reflectedActionDescriptor = null;

            // Check whether the ActionDescriptor is wrapped in a decorator or not.
            var wrapper = actionContext.ActionDescriptor as IDecorator<HttpActionDescriptor>;
            while (wrapper != null)
            {
                var castedWrapper = wrapper.Inner as IDecorator<HttpActionDescriptor>;
                if (castedWrapper == null)
                {
                    reflectedActionDescriptor = wrapper.Inner as ReflectedHttpActionDescriptor;
                }

                wrapper = castedWrapper;
            }

            if (reflectedActionDescriptor == null)
            {
                throw new NotAuthorizedException("Unable to get claims required by reflection. Cannot authorize.");
            }

            return reflectedActionDescriptor.MethodInfo;
        }
    }
}