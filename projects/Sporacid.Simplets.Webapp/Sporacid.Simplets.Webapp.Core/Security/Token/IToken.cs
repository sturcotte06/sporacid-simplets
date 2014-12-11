namespace Sporacid.Simplets.Webapp.Core.Security.Token
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IToken
    {
        /// <summary>
        /// The token's key.
        /// </summary>
        String Key { get; }

        /// <summary>
        /// Utc time at which the token was generated.
        /// </summary>
        DateTime EmittedAt { get; }

        /// <summary>
        /// The time span for which the token is valid.
        /// </summary>
        TimeSpan ValidFor { get; }
    }
}