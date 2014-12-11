namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System.Net;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using ICredentials = Sporacid.Simplets.Webapp.Core.Models.Sessions.ICredentials;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IAuthenticationModule
    {
        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongUsernameException">If user does not exist.</exception>
        /// <exception cref="WrongPasswordException">If the password does not match.</exception>
        IPrincipal Authenticate(ICredentials credentials);

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        bool IsSupported(AuthenticationScheme scheme);

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        AuthenticationScheme[] Supports();
    }
}