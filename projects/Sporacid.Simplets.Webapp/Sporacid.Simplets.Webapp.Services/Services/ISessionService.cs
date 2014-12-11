namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using System.Web.Http.ModelBinding;
    using PostSharp.Patterns.Contracts;
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
        SessionToken Create([Required] SessionCredentials sessionCredentials);

        /// <summary>
        /// Invalidates the session associated with the given session token.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="ipv4Address">
        /// The ipv4 address of the http request. Must not be sent by the client, but interpreted by the
        /// server, to avoid forgery.
        /// </param>
        void Invalidate([Required] String sessionKey, [Required] [ModelBinder] String ipv4Address);

        // [ModelBinder(typeof (Ipv4AddressModelBinder))]
    }
}