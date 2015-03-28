namespace Sporacid.Simplets.Webapp.Services.Services.Security.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("ContextAdministration")]
    [Contextual("context")]
    [ContractClass(typeof (ContextAdministrationServiceContract))]
    public interface IContextAdministrationService : IService
    {
        /// <summary>
        /// Creates a context in the system.
        /// Creating a context will automatically give all rights on the context to the owner principal.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <param name="owner">The context owner.</param>
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
        Int32 Create(String context, String owner);

        /// <summary>
        /// Returns all claims of the current user, by module, on the given context.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while getting all claims.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>A dictionary of all claims, by module.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<KeyValuePair<String, Claims>> GetAllClaimsOnContext(String context);

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

        /// <summary>
        /// Merges the claims of a given principal on a given context with the claims given by a role.
        /// For example, if a principal has the "read" claim on "context1" and the role "role1" has the 
        /// "create" claim, then the principal would end up with "read" and "create" on "context1".
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
        /// If something unexpected occurs while merging the role to the principal in the given context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        [RequiredClaims(Claims.Admin | Claims.UpdateAll)]
        void MergeClaimsOfPrincipalWithRole(String context, String role, String identity);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IContextAdministrationService))]
    internal abstract class ContextAdministrationServiceContract : IContextAdministrationService
    {
        public int Create(String context, String owner)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_CreateContext_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(owner), ContractStrings.ContextAdministrationService_GetAllClaimsOnContext_RequiresOwner);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.ContextAdministrationService_CreateContext_EnsuresPositiveContextId);

            // Dummy return.
            return default(Int32);
        }

        public IEnumerable<KeyValuePair<String, Claims>> GetAllClaimsOnContext(String context)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_GetAllClaimsOnContext_RequiresContext);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<String, Claims>>>() != null,
                ContractStrings.ContextAdministrationService_GetAllClaimsOnContext_EnsuresNonNullClaimsByModule);

            // Dummy return.
            return default(IEnumerable<KeyValuePair<String, Claims>>);
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

        public void MergeClaimsOfPrincipalWithRole(String context, String role, String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_MergeClaimsOfPrincipalWithRole_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(role), ContractStrings.ContextAdministrationService_MergeClaimsOfPrincipalWithRole_RequiresRole);
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.ContextAdministrationService_MergeClaimsOfPrincipalWithRole_RequiresIdentity);
        }
    }
}