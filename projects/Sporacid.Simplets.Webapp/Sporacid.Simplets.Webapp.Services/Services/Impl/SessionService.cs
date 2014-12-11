namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Aspects.Logging;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Trace]
    [RoutePrefix("api/v1")]
    public class SessionService : BaseService, ISessionService
    {
        private readonly IAuthenticationModule authenticationModule;

        public SessionService(IAuthenticationModule authenticationModule)
        {
            this.authenticationModule = authenticationModule;
        }

        /// <summary>
        /// Creates a session, for the given credentials, in the system.
        /// If the credentials are wrong, an exception will be thrown.
        /// </summary>
        /// <param name="sessionCredentials">The user's credentials.</param>
        /// <returns>A session token.</returns>
        [HttpPost]
        [Route("session")]
        public SessionToken Create(SessionCredentials sessionCredentials)
        {
            // Authenticate the user (will throw if user is not authenticated).
            this.authenticationModule.Authenticate(sessionCredentials);

            // Generate a session token.
            // var sessionToken = this.sessionTokenGenerator.Generate();

            // Cache a new session, accessible only if you have the generated token.

            // Generate a new session token.
            return null;
        }

        /// <summary>
        /// Invalidates the session associated with the given session token.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="ipv4Address">
        /// The ipv4 address of the http request. Must not be sent by the client, but interpreted by the
        /// server, to avoid forgery.
        /// </param>
        [HttpDelete]
        [Route("session/{sessionKey}")]
        public void Invalidate(String sessionKey, String ipv4Address)
        {
        }
    }
}