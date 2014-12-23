namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Inscriptions")]
    [Contextual("clubName")]
    public interface IInscriptionService
    {
        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        void SubscribeToClub([Required] String clubName, [Required] String codeUniversel);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Delete)]
        void UnsubscribeFromClub([Required] String clubName, [Required] String codeUniversel);
    }
}