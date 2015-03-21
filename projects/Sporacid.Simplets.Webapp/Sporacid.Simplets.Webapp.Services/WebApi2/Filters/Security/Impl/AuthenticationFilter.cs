namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using Autofac.Integration.WebApi;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials;
    using Sporacid.Simplets.Webapp.Tools.Threading;
    using IAuthenticationModule = Sporacid.Simplets.Webapp.Core.Security.Authentication.IAuthenticationModule;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationFilter : IAutofacAuthenticationFilter
    {
        private readonly IEnumerable<IAuthenticationModule> authenticationModules;
        private readonly IEnumerable<ICredentialsExtractor> credentialsExtractors;
        private readonly IPrincipalAdministrationService principalAdministrationService;

        public AuthenticationFilter(IEnumerable<IAuthenticationModule> authenticationModules, IEnumerable<ICredentialsExtractor> credentialsExtractors,
            IPrincipalAdministrationService principalAdministrationService)
        {
            this.authenticationModules = authenticationModules;
            this.credentialsExtractors = credentialsExtractors;
            this.principalAdministrationService = principalAdministrationService;
        }

        /// <summary>
        /// Called when a request requires authentication.
        /// </summary>
        /// <param name="context">The context for the authentication.</param>
        public void OnAuthenticate(HttpAuthenticationContext context)
        {
            // Look for credentials in the request.
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            var cultureHeader = request.Headers.AcceptLanguage.FirstOrDefault();
            Thread.CurrentThread.ToCulture(cultureHeader != null ? cultureHeader.Value : ConfigurationManager.AppSettings["DefaultLanguage"]);

            // If there are no credentials, throw.
            if (authorization == null)
            {
                throw new SecurityException(ExceptionStrings.Services_Security_AuthHeaderRequired);
            }

            // If there are credentials but the filter does not recognize the authentication scheme, do nothing.
            AuthenticationScheme scheme;
            if (!Enum.TryParse(authorization.Scheme, true, out scheme))
            {
                throw new SecurityException(String.Format(ExceptionStrings.Services_Security_UnsupportedScheme, authorization.Scheme));
            }

            var authenticationModule = this.authenticationModules.FirstOrDefault(a => a.IsSupported(scheme));
            if (authenticationModule == null)
            {
                throw new SecurityException(String.Format(ExceptionStrings.Services_Security_UnsupportedScheme, scheme));
            }

            var credentialsExtractor = this.credentialsExtractors.FirstOrDefault(e => e.IsSupported(scheme));
            if (credentialsExtractor == null)
            {
                throw new SecurityException(String.Format(ExceptionStrings.Services_Security_CannotExtractScheme, scheme));
            }

            var credentials = credentialsExtractor.Extract(authorization.Parameter);
            if (credentials == null)
            {
                throw new SecurityException(ExceptionStrings.Services_Security_InvalidCredentialsFormat);
            }

            // Authenticate the principal.
            var tokenAndPrincipal = authenticationModule.Authenticate(credentials);

            // Make sure this stupid principal is set everywhere.
            Thread.CurrentPrincipal = context.Principal = tokenAndPrincipal;
        }

        /// <summary>
        /// Called when an authentication challenge is required.
        /// </summary>
        /// <param name="context">The context for the authentication challenge.</param>
        public void OnChallenge(HttpAuthenticationChallengeContext context)
        {
            var challenge = new AuthenticationHeaderValue(AuthenticationScheme.Kerberos.ToString());
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
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