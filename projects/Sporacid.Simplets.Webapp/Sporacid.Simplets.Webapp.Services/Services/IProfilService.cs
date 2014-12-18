namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [Contextual("context")]
    public interface IProfilService
    {
        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="profil">The profil.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void Update(String context, ProfilDto profil);
    }
}