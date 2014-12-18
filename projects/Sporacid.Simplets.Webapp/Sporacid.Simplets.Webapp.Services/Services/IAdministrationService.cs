namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext("Systeme")]
    public interface IAdministrationService
    {
        /// <summary>
        /// Adds a club entity into the system.
        /// All resources available for the club will be added to the security sub-system.
        /// </summary>
        /// <param name="club">The club entity.</param>
        /// <returns>The id of the newly created club entity.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 AddClub([Required] ClubDto club);
    }
}