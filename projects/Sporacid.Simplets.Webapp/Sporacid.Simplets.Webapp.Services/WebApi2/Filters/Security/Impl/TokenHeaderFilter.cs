namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Impl
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Autofac.Integration.WebApi;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TokenHeaderFilter : IAutofacActionFilter
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var authorization = actionExecutedContext.Request.Headers.Authorization;
            if (authorization.Scheme.EqualsIgnoreCase(AuthenticationScheme.Token.ToString()))
            {
                return;
            }

            var tokenAndPrincipal = Thread.CurrentPrincipal as ITokenAndPrincipal;
            if (tokenAndPrincipal == null)
            {
                return;
            }

            var response = HttpContext.Current.Response;
            var token = tokenAndPrincipal.Token;
            var base64Token = Convert.ToBase64String(Encoding.ASCII.GetBytes(token.Key));
            response.Headers.Add("Authorization-Token", base64Token);
            response.Headers.Add("Authorization-Token-Emitted-At", token.EmittedAt.ToString("u"));
            response.Headers.Add("Authorization-Token-Expires-At", token.EmittedAt.Add(tokenAndPrincipal.Token.ValidFor).ToString("u"));
        }
    }
}