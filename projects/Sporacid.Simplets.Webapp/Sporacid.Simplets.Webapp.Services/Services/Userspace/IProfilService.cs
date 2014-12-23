namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    using System;
    using System.Collections.Generic;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Profils")]
    [Contextual("codeUniversel")]
    public interface IProfilService
    {
        /// <summary>
        /// Gets the profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        ProfilDto GetProfil([Required] String codeUniversel);

        /// <summary>
        /// Updates the profil entity in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void UpdateProfil([Required] String codeUniversel, [Required] ProfilDto profil);

        /// <summary>
        /// Gets all club entities from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>All club entities subscribed to.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo([Required] String codeUniversel);
    }
}