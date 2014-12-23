namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IRoleBootstrapper
    {
        /// <summary>
        /// Starts the bootstrap. 
        /// Sets the modules for which we'll bind claims.
        /// </summary>
        /// <param name="modules">The modules on which to bind claims.</param>
        /// <returns>Chain object.</returns>
        IRoleBootstrapper ToModules([NotEmpty] params String[] modules);

        /// <summary>
        /// Bind claims for the module.
        /// </summary>
        /// <param name="claims">The claims to bind.</param>
        /// <returns>Chain object.</returns>
        IRoleBootstrapper BindClaims([Required] Claims claims);

        /// <summary>
        /// Bootstraps a role template to the database.
        /// </summary>
        /// <param name="role">The role name to bootstrap the bindings to.</param>
        void BootstrapTo([Required] String role);
    }
}