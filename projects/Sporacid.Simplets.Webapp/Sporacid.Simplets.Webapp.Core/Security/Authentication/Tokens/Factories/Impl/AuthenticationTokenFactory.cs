namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories.Impl
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Impl;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationTokenFactory : ITokenFactory
    {
        private readonly uint keyLength;
        private readonly TimeSpan validityTimespan;

        public AuthenticationTokenFactory(TimeSpan validityTimespan, uint keyLength)
        {
            this.validityTimespan = validityTimespan;
            this.keyLength = keyLength;
        }

        /// <summary>
        /// Generates a random session token.
        /// </summary>
        /// <returns>A session token.</returns>
        public IToken Generate()
        {
            return new AuthenticationToken
            {
                Key = StringExtensions.SecureRandom(this.keyLength),
                EmittedAt = DateTime.Now,
                ValidFor = this.validityTimespan
            };
        }
    }
}