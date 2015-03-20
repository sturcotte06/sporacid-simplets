namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Impl
{
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Web.Http.Controllers;
    using System.Web.Http.Services;
    using Autofac.Integration.WebApi;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Tools.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationFilter : IAutofacAuthorizationFilter
    {
        private readonly IAuthorizationModule authorizationModule;
        private readonly ClaimsByActionDictionary claimsByAction;

        public AuthorizationFilter(IAuthorizationModule authorizationModule, ClaimsByActionDictionary claimsByAction)
        {
            this.authorizationModule = authorizationModule;
            this.claimsByAction = claimsByAction;
        }

        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public void OnAuthorization(HttpActionContext actionContext)
        {
            var serviceType = actionContext.ControllerContext.Controller.GetType();
            var serviceMethod = GetMethodInfoFromActionContext(actionContext);

            // Get the module attribute. This is a required key of the authorization system.
            var moduleAttr = serviceType.GetAllCustomAttributes<ModuleAttribute>().FirstOrDefault();
            if (moduleAttr == null)
            {
                throw new NotAuthorizedException(ExceptionStrings.Services_Security_NotConfiguredAction);
            }

            // Get the required claims attribute. This is a required key of the authorization system.
            Claims claims;
            var serviceActionName = this.claimsByAction.CreateKey(moduleAttr, serviceMethod);
            if (!this.claimsByAction.TryGetValue(serviceActionName, out claims))
            {
                throw new NotAuthorizedException(ExceptionStrings.Services_Security_NotConfiguredAction);
            }

            // Get the context attribute. This is a required key of the authorization system.
            // Context can be fixed, or dynamic. Handle both cases.
            var fixedCtxAttr = serviceType.GetAllCustomAttributes<FixedContextAttribute>().FirstOrDefault();
            if (fixedCtxAttr != null)
            {
                // Fixed context. The context is constant and is not part of the url.
                this.authorizationModule.Authorize(Thread.CurrentPrincipal, claims, moduleAttr.Name, fixedCtxAttr.Name);
            }
            else
            {
                // Dynamic context. The context is given in the url, so get its value.
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

                this.authorizationModule.Authorize(Thread.CurrentPrincipal, claims, moduleAttr.Name, context.ToString());
            }
        }

        /// <summary>
        /// Returns the controller method info that is to be executed.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>The controller method info that is to be executed.</returns>
        private static MethodInfo GetMethodInfoFromActionContext(HttpActionContext actionContext)
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