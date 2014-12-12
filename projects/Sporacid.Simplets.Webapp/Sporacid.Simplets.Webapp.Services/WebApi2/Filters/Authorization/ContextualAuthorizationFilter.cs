namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authorization
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ContextualAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthorizationModule authorizationModule;

        public ContextualAuthorizationFilter(IAuthorizationModule authorizationModule)
        {
            this.authorizationModule = authorizationModule;
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
            return continuation.Invoke();
        }
    }
}