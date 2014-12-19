namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Default")]
    [FixedContext("Systeme")]
    public interface IAnonymousService
    {
        /// <summary>
        /// Dummy method that can be called to bootstrap a new user.
        /// </summary>
        [RequiredClaims(Claims.None)]
        void NoOp();

        /// <summary>
        /// Help method to get the api help.
        /// </summary>
        [RequiredClaims(Claims.None)]
        IEnumerable<ApiMethodDescriptionDto> Help();
    }
}