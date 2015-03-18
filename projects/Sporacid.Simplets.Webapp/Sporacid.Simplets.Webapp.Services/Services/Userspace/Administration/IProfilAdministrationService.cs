namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Administration
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("ProfilAdministration")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof(ProfilAdministrationServiceContract))]
    public interface IProfilAdministrationService : IService
    {
        /// <summary>
        /// Creates the base profil entity for a given principal's identity.
        /// Every available informations for the principal will be extracted and included in the profil entity.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the profil entity already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the base profil entity.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The id of the created profil entity.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreateBaseProfil(String identity);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(IProfilAdministrationService))]
    internal abstract class ProfilAdministrationServiceContract : IProfilAdministrationService
    {
        public Int32 CreateBaseProfil(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilAdministrationService_CreateBaseProfil_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.ProfilAdministrationService_CreateBaseProfil_EnsuresPositiveProfilId);

            // Dummy return.
            return default(Int32);
        }
    }
}