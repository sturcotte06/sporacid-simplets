namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Impl;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TokenAuthenticationModule : IAuthenticationModule
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Token};
        private readonly ICache<IPrincipal, IToken> principalCache;
        private readonly ICache<IToken, IPrincipal> tokenCache;

        public TokenAuthenticationModule(ICache<IToken, IPrincipal> tokenCache, ICache<IPrincipal, IToken> principalCache)
        {
            this.tokenCache = tokenCache;
            this.principalCache = principalCache;
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
            var baseToken = new AuthenticationToken {Key = credentials.Identity};

            IPrincipal principal = null;
            if (!this.tokenCache.TryWithValueDo(baseToken, p => principal = p))
            {
                throw new WrongCredentialsException();
            }

            IToken token = null;
            if (!this.principalCache.TryWithValueDo(principal, tok => token = tok))
            {
                throw new WrongCredentialsException();
            }

            return new TokenAndPrincipal(token, principal);
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public Boolean IsSupported(AuthenticationScheme scheme)
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

        /// <summary>
        /// Updates the observer.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        public void Update(ITokenAndPrincipal tokenAndPrincipal)
        {
            // Cache the token with the principals. 
            // If the user specify token authentication, we can speed up its response.
            this.tokenCache.Put(tokenAndPrincipal.Token, tokenAndPrincipal);
        }
    }
}