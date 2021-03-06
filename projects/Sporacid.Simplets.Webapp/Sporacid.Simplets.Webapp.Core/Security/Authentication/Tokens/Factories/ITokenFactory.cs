﻿namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ITokenFactory
    {
        /// <summary>
        /// Generates a random session token.
        /// </summary>
        /// <returns>A session token.</returns>
        IToken Generate();
    }
}