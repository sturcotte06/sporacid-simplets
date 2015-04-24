namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.DirectoryServices.AccountManagement;
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories;
    using Sporacid.Simplets.Webapp.Core.Security.Events;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosAuthenticationModule : IAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Kerberos};
        private readonly String kerberosDomainControllerName;
        private readonly IEventBus<PrincipalAuthenticated, PrincipalAuthenticatedEventArgs> principalAuthenticatedEventBus;
        private readonly ITokenFactory tokenFactory;

        public KerberosAuthenticationModule(IEventBus<PrincipalAuthenticated, PrincipalAuthenticatedEventArgs> principalAuthenticatedEventBus,
            ITokenFactory tokenFactory, String kerberosDomainControllerName)
        {
            this.principalAuthenticatedEventBus = principalAuthenticatedEventBus;
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
            try
            {
                // Create a new principal context for kerberos domain controller.
                using (var context = new PrincipalContext(ContextType.Domain, this.kerberosDomainControllerName))
                {
                    // Username and password for authentication.
                    if (!context.ValidateCredentials(credentials.Identity, credentials.Password))
                    {
                        throw new WrongCredentialsException();
                    }
                }
            }
            catch (PrincipalServerDownException ex)
            {
                throw new SecurityException(ExceptionStrings.Core_Security_KerberosDown, ex);
            }

            // User authenticated.
            // generate a new authentication token for further calls.
            var token = this.tokenFactory.Generate();

            // Generate the principal object.
            var principal = new Principal(credentials.Identity, AuthenticationScheme.Kerberos);

            // Publish an event notifying subscribers of the authentication.
            this.Publish(this.principalAuthenticatedEventBus, new PrincipalAuthenticatedEventArgs(principal, token));

            return new TokenAndPrincipal(token, principal);
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public Boolean IsSupported(AuthenticationScheme scheme)
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