namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [Contextual("context")]
    [ContractClass(typeof(ContextAdministrationServiceContract))]
    public interface IContextAdministrationService
    {
        /// <summary>
        /// Creates a security context in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        [RequiredClaims(Claims.None)] // TODO can this be a DDOS possible attack?
        Int32 CreateContext(String context);

        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="identity">The principal identity.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void BindRoleToPrincipal(String context, String role, String identity);

        /// <summary>
        /// Remove all claims from a user on the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="identity">The principal identity.</param>
        [RequiredClaims(Claims.Admin | Claims.DeleteAll)]
        void RemoveAllClaimsFromPrincipal(String context, String identity);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(IContextAdministrationService))]
    abstract class ContextAdministrationServiceContract : IContextAdministrationService
    {
        /// <summary>
        /// Creates a security context in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        public Int32 CreateContext(String context)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_CreateContext_RequiresContext);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.ContextAdministrationService_CreateContext_EnsuresPositiveContextId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="identity">The principal identity.</param>
        public void BindRoleToPrincipal(String context, String role, String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(role), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresRole);
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.ContextAdministrationService_BindRoleToPrincipal_RequiresIdentity);
        }

        /// <summary>
        /// Remove all claims from a user on the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="identity">The principal identity.</param>
        public void RemoveAllClaimsFromPrincipal(String context, String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextAdministrationService_RemoveAllClaimsFromPrincipal_RequiresContext);
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.ContextAdministrationService_RemoveAllClaimsFromPrincipal_RequiresIdentity);
        }
    }
}