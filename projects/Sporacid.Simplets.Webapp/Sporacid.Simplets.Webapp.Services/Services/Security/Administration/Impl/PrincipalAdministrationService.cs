namespace Sporacid.Simplets.Webapp.Services.Services.Security.Administration.Impl
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Core.Security.Database.Repositories;
    using Sporacid.Simplets.Webapp.Services.Events;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAdministrationController : BaseSecureService, IPrincipalAdministrationService, IEventPublisher<PrincipalCreated, PrincipalCreatedEventArgs>
    {
        private readonly IEventBus<PrincipalCreated, PrincipalCreatedEventArgs> principalCreatedEventBus;
        private readonly ISecurityRepository<Int32, Principal> principalRepository;

        public PrincipalAdministrationController(IEventBus<PrincipalCreated, PrincipalCreatedEventArgs> principalCreatedEventBus,
            ISecurityRepository<Int32, Principal> principalRepository)
        {
            this.principalRepository = principalRepository;
            this.principalCreatedEventBus = principalCreatedEventBus;
        }

        /// <summary>
        /// Get whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while trying to get whether the principal exists.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>Whether the principal exists.</returns>
        public Boolean Exists(String identity)
        {
            return this.principalRepository.Has(principal => principal.Identity == identity);
        }

        /// <summary>
        /// Creates a principal in the system.
        /// Creating a principal will creates its base profile, its personnal security context and will give all rights
        /// to the principal on its context.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the principal already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the principal.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The created principal id.</returns>
        public Int32 Create(String identity)
        {
            // Cannot add the same context twice.
            if (this.Exists(identity))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_PrincipalDuplicate, identity));
            }

            // Add the new principal.
            var principalEntity = new Principal {Identity = identity};
            this.principalRepository.Add(principalEntity);

            // Publish a new principal created event.
            this.Publish(this.principalCreatedEventBus, new PrincipalCreatedEventArgs(identity));

            return principalEntity.Id;
        }
    }
}