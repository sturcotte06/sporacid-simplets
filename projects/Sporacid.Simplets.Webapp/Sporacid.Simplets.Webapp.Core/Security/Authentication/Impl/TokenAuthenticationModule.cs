namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Token;
    using Sporacid.Simplets.Webapp.Core.Security.Token.Impl;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TokenAuthenticationModule : IAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Token};
        private readonly ICache<IToken, ITokenAndPrincipal> tokenCache;

        public TokenAuthenticationModule(ICache<IToken, ITokenAndPrincipal> tokenCache)
        {
            this.tokenCache = tokenCache;
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
            var authenticationToken = new AuthenticationToken {Key = credentials.Identity};
            if (!this.tokenCache.Has(authenticationToken))
            {
                throw new WrongCredentialsException();
            }

            ITokenAndPrincipal tokenAndPrincipal = null;
            this.tokenCache.WithValueDo(authenticationToken, v => tokenAndPrincipal = v);
            return tokenAndPrincipal;
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public bool IsSupported(AuthenticationScheme scheme)
        {
            return scheme == AuthenticationScheme.Token;
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