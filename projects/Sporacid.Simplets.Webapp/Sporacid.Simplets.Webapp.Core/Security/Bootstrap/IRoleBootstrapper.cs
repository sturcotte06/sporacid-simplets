namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof(RoleBootstrapperContract))]
    public interface IRoleBootstrapper
    {
        /// <summary>
        /// Starts the bootstrap. 
        /// Sets the modules for which we'll bind claims.
        /// </summary>
        /// <param name="modules">The modules on which to bind claims.</param>
        /// <returns>Chain object.</returns>
        IRoleBootstrapper ToModules(params String[] modules);

        /// <summary>
        /// Bind claims for the module.
        /// </summary>
        /// <param name="claims">The claims to bind.</param>
        /// <returns>Chain object.</returns>
        IRoleBootstrapper BindClaims(Claims claims);

        /// <summary>
        /// Bootstraps a role template to the database.
        /// </summary>
        /// <param name="role">The role name to bootstrap the bindings to.</param>
        void BootstrapTo(String role);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(IRoleBootstrapper))]
    internal abstract class RoleBootstrapperContract : IRoleBootstrapper
    {
        /// <summary>
        /// Starts the bootstrap. 
        /// Sets the modules for which we'll bind claims.
        /// </summary>
        /// <param name="modules">The modules on which to bind claims.</param>
        /// <returns>Chain object.</returns>
        public IRoleBootstrapper ToModules(params String[] modules)
        {
            // Preconditions.
            Contract.Requires(modules != null && Contract.ForAll(modules, module => !String.IsNullOrEmpty(module)),
                ContractStrings.RoleBootstrapper_ToModules_RequiresModules);

            // Postconditions.
            Contract.Ensures(Contract.Result<IRoleBootstrapper>() != null, ContractStrings.RoleBootstrapper_ToModules_EnsuresNonNullRoleBootstrapper);

            // Dummy return.
            return default(IRoleBootstrapper);
        }

        /// <summary>
        /// Bind claims for the module.
        /// </summary>
        /// <param name="claims">The claims to bind.</param>
        /// <returns>Chain object.</returns>
        public IRoleBootstrapper BindClaims(Claims claims)
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IRoleBootstrapper>() != null, ContractStrings.RoleBootstrapper_BindClaims_EnsuresNonNullRoleBootstrapper);

            // Dummy return.
            return default(IRoleBootstrapper);
        }

        /// <summary>
        /// Bootstraps a role template to the database.
        /// </summary>
        /// <param name="role">The role name to bootstrap the bindings to.</param>
        public void BootstrapTo(String role)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(role), ContractStrings.RoleBootstrapper_BootstrapTo_RequiresRole);
        }
    }
}