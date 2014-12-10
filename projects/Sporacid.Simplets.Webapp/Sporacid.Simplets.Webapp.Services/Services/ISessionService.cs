namespace Sporacid.Simplets.Webapp.Services.Services
{
    using AttributeRouting.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;

    /// <summary>
    /// Interface for all session services.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ISessionService
    {
        /// <summary>
        /// Creates a session, for the given credentials, in the system.
        /// If the credentials are wrong, an exception will be thrown.
        /// </summary>
        /// <param name="sessionCredentials">The user's credentials.</param>
        /// <returns>A session token.</returns>
        [POST("")]
        SessionToken Create(SessionCredentials sessionCredentials);

        /// <summary>
        /// Invalidates the session associated with the given session token.
        /// </summary>
        /// <param name="sessionToken">The session token.</param>
        [DELETE("{sessionToken:sessionToken}")]
        void Invalidate(SessionToken sessionToken);
    }
}