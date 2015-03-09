namespace Sporacid.Simplets.Webapp.Services.Services.Administration
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
    [Module("Administration")]
    [Contextual("context")]
    [ContractClass(typeof (ContextAdministrationServiceContract))]
    public interface IContextAdministrationService
    {
        /// <summary>
        /// Creates a context in the system.
        /// Creating a context will automatically give all rights on the context to the principal creating the context.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the security context already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The id of the created context entity.</returns>
        [RequiredClaims(Claims.Admin)]
        Int32 CreateContext(String context);

        /// <summary>
        /// Binds a role to a principal in a given context.
        /// Every previous claims of the principal on this context will be removed beforehand.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <param name="role">The role name.</param>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="EntityNotFoundException{Context}">
        /// If the context does not exist.
        /// </exception>
        /// <exception cref="EntityNotFoundException{RoleTemplate}">
        /// If the role does not exist.
        /// </exception>
        /// <exception cref="EntityNotFoundException{Principal}">
        /// If the principal's identity does not exist.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while binding the role to the principal in the given context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void BindRoleToPrincipal(String context, String role, String identity);

        /// <summary>
        /// Remove all claims of a principal on a given context.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="EntityNotFoundException{Context}">
        /// If the context does not exist.
        /// </exception>
        /// <exception cref="EntityNotFoundException{Principal}">
        /// If the principal's identity does not exist.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while removing all claims of the principal in the given context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        [RequiredClaims(Claims.Admin | Claims.DeleteAll)]
        void RemoveAllClaimsFromPrincipal(String context, String identity);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IContextAdministrationService))]
    internal abstract class ContextAdministrationServiceContract : IContextAdministrationService
    {
        public Int32 CreateContext(String context)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_CreateContext_RequiresContext);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.ContextAdministrationService_CreateContext_EnsuresPositiveContextId);

            // Dummy return.
            return default(Int32);
        }

        public void BindRoleToPrincipal(String context, String role, String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(role), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresRole);
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresIdentity);
        }

        public void RemoveAllClaimsFromPrincipal(String context, String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_RemoveAllClaimsFromPrincipal_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.ContextAdministrationService_RemoveAllClaimsFromPrincipal_RequiresIdentity);
        }
    }
}