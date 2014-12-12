namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Tools.Strings;
    using IAuthenticationModule = Sporacid.Simplets.Webapp.Core.Security.Authentication.IAuthenticationModule;
    using ICredentials = Sporacid.Simplets.Webapp.Core.Models.Sessions.ICredentials;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private readonly IAuthenticationModule[] supportedAuthenticationModules;

        public AuthenticationFilter(IAuthenticationModule[] supportedAuthenticationModules)
        {
            this.supportedAuthenticationModules = supportedAuthenticationModules;
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

            // If there are credentials that the filter understands, try to validate them.
            // If the credentials are bad, set the error result.
            if (authorization.Parameter.IsNullOrEmpty())
            {
                throw new SecurityException("Credentials are missing.");
            }

            var credentials = ExtractCredentials(authorization.Parameter);
            if (credentials == null)
            {
                throw new SecurityException("Credentials are in an invalid format.");
            }

            context.Principal = authenticationModule.Authenticate(credentials);
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

        /// <summary>
        /// Extract the credentials
        /// </summary>
        /// <param name="authorizationParameter"></param>
        /// <returns></returns>
        private static ICredentials ExtractCredentials(String authorizationParameter)
        {
            byte[] credentialBytes;
            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            var encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding) encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;

            string decodedCredentials;
            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (decodedCredentials.IsNullOrEmpty())
            {
                return null;
            }

            var colonIndex = decodedCredentials.IndexOf(':');
            if (colonIndex == -1)
            {
                return null;
            }

            var username = decodedCredentials.Substring(0, colonIndex);
            var password = decodedCredentials.Substring(colonIndex + 1);
            return new Credentials(username, password);
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