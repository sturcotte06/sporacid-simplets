namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.DirectoryServices.AccountManagement;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Token;
    using Sporacid.Simplets.Webapp.Core.Security.Token.Factories;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosAuthenticationModule : IAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Kerberos};
        private readonly String kerberosDomainControllerName;
        private readonly ICache<IToken, ITokenAndPrincipal> tokenCache;
        private readonly ITokenFactory tokenFactory;

        public KerberosAuthenticationModule(ICache<IToken, ITokenAndPrincipal> tokenCache, ITokenFactory tokenFactory, String kerberosDomainControllerName)
        {
            this.tokenCache = tokenCache;
            this.tokenFactory = tokenFactory;
            this.kerberosDomainControllerName = kerberosDomainControllerName;
        }

        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongCredentialsException">If user does not exist or the password does not match.</exception>
        public ITokenAndPrincipal Authenticate(ICredentials credentials)
        {
            using (var context = new PrincipalContext(ContextType.Domain, this.kerberosDomainControllerName))
            {
                try
                {
                    // Username and password for authentication.
                    if (!context.ValidateCredentials(credentials.Identity, credentials.Password))
                    {
                        throw new WrongCredentialsException();
                    }
                }
                catch (PrincipalServerDownException ex)
                {
                    throw new SecurityException("Unable to contact kerberos server.", ex);
                }
            }

            // User authenticated.
            var token = this.tokenFactory.Generate();
            var principal = new Principal(credentials.Identity, AuthenticationScheme.Kerberos, AuthorizationLevel.User);
            var tokenAndPrincipal = new TokenAndPrincipal(token, principal);

            // Cache the token with the principals. 
            // If the user specify token authentication, we can speed up its response.
            this.tokenCache.Put(token, tokenAndPrincipal);

            return tokenAndPrincipal;
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