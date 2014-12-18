namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials;
    using IAuthenticationModule = Sporacid.Simplets.Webapp.Core.Security.Authentication.IAuthenticationModule;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationFilter : IAuthenticationFilter
    {
        [ThreadStatic] private static IToken requestToken;

        private readonly IAuthenticationModule[] supportedAuthenticationModules;
        private readonly ICredentialsExtractor[] supportedCredentialsExtractors;

        public AuthenticationFilter(IAuthenticationModule[] supportedAuthenticationModules, ICredentialsExtractor[] supportedCredentialsExtractors)
        {
            this.supportedAuthenticationModules = supportedAuthenticationModules;
            this.supportedCredentialsExtractors = supportedCredentialsExtractors;
        }

        /// <summary>
        /// The thread's request token.
        /// </summary>
        public static IToken RequestToken
        {
            get { return requestToken; }
            private set { requestToken = value; }
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
        /// Authenticates the request.
        /// </summary>
        /// <returns>
        /// A Task that will perform authentication.
        /// </returns>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // Look for credentials in the request.
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            // If there are no credentials, throw.
            if (authorization == null)
            {
                throw new SecurityException("Authorization headers required.");
            }

            // If there are credentials but the filter does not recognize the 
            // authentication scheme, do nothing.
            AuthenticationScheme scheme;
            if (!Enum.TryParse(authorization.Scheme, out scheme))
            {
                throw new SecurityException(String.Format("Scheme {0} is not supported.", authorization.Scheme));
            }

            var authenticationModule = this.supportedAuthenticationModules.FirstOrDefault(a => a.IsSupported(scheme));
            if (authenticationModule == null)
            {
                throw new SecurityException(String.Format("Scheme {0} is not supported.", scheme));
            }

            var credentialsExtractor = this.supportedCredentialsExtractors.FirstOrDefault(e => e.IsSupported(scheme));
            if (credentialsExtractor == null)
            {
                throw new SecurityException(String.Format("Credentials for scheme {0} cannot be extracted.", scheme));
            }

            var credentials = credentialsExtractor.Extract(authorization.Parameter);
            if (credentials == null)
            {
                throw new SecurityException("Credentials are in an invalid format.");
            }

            // Authenticate the principal.
            var tokenAndPrincipal = authenticationModule.Authenticate(credentials);

            // Make sure this stupid principal is set everywhere.
            HttpContext.Current.User = Thread.CurrentPrincipal = context.Principal = tokenAndPrincipal.Principal;
            RequestToken = tokenAndPrincipal.Token;

            return Task.FromResult(0);
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue(AuthenticationScheme.Kerberos.ToString());
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }

        /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
        /// <version>1.9.0</version>
        private class AddChallengeOnUnauthorizedResult : IHttpActionResult
        {
            public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
            {
                this.Challenge = challenge;
                this.InnerResult = innerResult;
            }

            private AuthenticationHeaderValue Challenge { get; set; }
            private IHttpActionResult InnerResult { get; set; }

            public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = await this.InnerResult.ExecuteAsync(cancellationToken);
                if (response.StatusCode != HttpStatusCode.Unauthorized)
                {
                    return response;
                }

                // Only add one challenge per authentication scheme.
                if (response.Headers.WwwAuthenticate.All(h => h.Scheme != this.Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(this.Challenge);
                }

                return response;
            }
        }
    }
}