namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;
    using Sporacid.Simplets.Webapp.Core.Security.Token;
    using Sporacid.Simplets.Webapp.Core.Security.Token.Factories;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosAuthenticationModule : IAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Kerberos};
        private readonly ICache<IToken, IPrincipal> tokenCache;
        private readonly ITokenFactory tokenFactory;

        public KerberosAuthenticationModule(ICache<IToken, IPrincipal> tokenCache, ITokenFactory tokenFactory)
        {
            this.tokenCache = tokenCache;
            this.tokenFactory = tokenFactory;
        }

        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongUsernameException">If user does not exist.</exception>
        /// <exception cref="WrongPasswordException">If the password does not match.</exception>
        public IPrincipal Authenticate(ICredentials credentials)
        {
            // Do authentication...

            var token = this.tokenFactory.Generate();
            this.tokenCache.Put(token, null);
            return null;
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public bool IsSupported(AuthenticationScheme scheme)
        {
            return scheme == AuthenticationScheme.Kerberos;
        }

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        public AuthenticationScheme[] Supports()
        {
            return SupportedSchemes;
        }
    }
}