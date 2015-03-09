namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof(SystemAdministrationServiceContract))]
    public interface ISystemAdministrationService
    {
        /// <summary>
        /// Creates a club entity into the system.
        /// Creating a club will creates its security context and will give all rights on the context to the principal creating the club.
        /// </summary>
        /// <param name="club">The club entity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the security context already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The id of the created club entity.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreateClub(ClubDto club);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(ISystemAdministrationService))]
    abstract class SystemAdministrationServiceContract : ISystemAdministrationService
    {
        /// <summary>
        /// Adds a club entity into the system.
        /// All resources available for the club will be added to the security sub-system.
        /// </summary>
        /// <param name="club">The club entity.</param>
        /// <returns>The id of the newly created club entity.</returns>
        public Int32 CreateClub(ClubDto club)
        {
            // Preconditions.
            Contract.Requires(club != null, ContractStrings.SystemAdministrationService_CreateClub_RequiresClub);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.SystemAdministrationService_CreateClub_EnsuresPositiveClubId);

            // Dummy return.
            return default(Int32);
        }
    }
}