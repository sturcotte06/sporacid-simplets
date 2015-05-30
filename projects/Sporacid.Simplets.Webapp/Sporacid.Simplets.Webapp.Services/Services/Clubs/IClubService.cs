namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Web;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Services.Services.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Clubs")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (ProfilServiceContract))]
    public interface IClubService : IService
    {
        /// <summary>
        /// Gets all club entities to which the current user is subscribed, from the system.
        /// </summary>
        /// <returns>All club entities subscribed to.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IClubService))]
    internal abstract class ClubServiceContract : IClubService
    {
        public IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo()
        {
            // Preconditions.
            Contract.Requires(HttpContext.Current.User != null, ContractStrings.ClubService_GetClubsSubscribedTo_RequiresAuthenticatedPrincipal);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ClubDto>>>() != null, ContractStrings.ClubService_GetClubsSubscribedTo_EnsuresNonNullClubs);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ClubDto>>);
        }
    }
}