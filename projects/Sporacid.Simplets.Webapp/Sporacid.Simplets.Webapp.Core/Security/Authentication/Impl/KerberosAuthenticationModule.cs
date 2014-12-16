namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.DirectoryServices.AccountManagement;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosAuthenticationModule : BaseAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Kerberos};
        private readonly String kerberosDomainControllerName;
        private readonly ITokenFactory tokenFactory;

        public KerberosAuthenticationModule(ITokenFactory tokenFactory, String kerberosDomainControllerName)
        {
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
        public override ITokenAndPrincipal Authenticate(ICredentials credentials)
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
            var principal = new Principal(credentials.Identity, AuthenticationScheme.Kerberos);
            var tokenAndPrincipal = new TokenAndPrincipal(token, principal);

            this.NotifyAuthentication(tokenAndPrincipal);
            return tokenAndPrincipal;
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public override bool IsSupported(AuthenticationScheme scheme)
        {
            return scheme == AuthenticationScheme.Kerberos;
        }

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        public override AuthenticationScheme[] Supports()
        {
            return SupportedSchemes;
        }
    }
}