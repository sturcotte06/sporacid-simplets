namespace Sporacid.Simplets.Webapp.Core.Security.Token.Impl
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationToken : IToken
    {
        /// <summary>
        /// The token's key.
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// Utc time at which the token was generated.
        /// </summary>
        public DateTime EmittedAt { get; internal set; }

        /// <summary>
        /// The time span for which the token is valid.
        /// </summary>
        public TimeSpan ValidFor { get; internal set; }
    }
}