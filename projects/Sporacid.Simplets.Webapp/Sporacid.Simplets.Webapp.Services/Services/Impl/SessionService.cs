namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using AttributeRouting.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SessionService : BaseService, ISessionService
    {
        /// <summary>
        /// Creates a session, for the given credentials, in the system.
        /// If the credentials are wrong, an exception will be thrown.
        /// </summary>
        /// <param name="sessionCredentials">The user's credentials.</param>
        /// <returns>A session token.</returns>
        public SessionToken Create(SessionCredentials sessionCredentials)
        {
            return new SessionToken();
        }

        /// <summary>
        /// Invalidates the session associated with the given session token.
        /// </summary>
        /// <param name="sessionToken">The session token.</param>
        public void Invalidate(SessionToken sessionToken)
        {
        }

        [GET("")]
        public object Get()
        {
            return new SessionToken();
        }
    }
}