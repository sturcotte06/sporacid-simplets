namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Tools.Enumerations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class RoleBootstrapper : IRoleBootstrapper
    {
        private readonly IRepository<Int32, Claim> claimRepository;
        private readonly IRepository<Int32, Module> moduleRepository;
        private readonly IRepository<Int32, RoleTemplate> roleRepository;
        private RoleModuleBindings currentRoleModuleBindings = new RoleModuleBindings();
        private List<RoleModuleBindings> roleModuleBindings = new List<RoleModuleBindings>();

        public RoleBootstrapper(IRepository<Int32, RoleTemplate> roleRepository,
            IRepository<Int32, Module> moduleRepository,
            IRepository<Int32, Claim> claimRepository)
        {
            this.roleRepository = roleRepository;
            this.moduleRepository = moduleRepository;
            this.claimRepository = claimRepository;
        }

        /// <summary>
        /// Bind claims for the module.
        /// </summary>
        /// <param name="claims">The claims to bind.</param>
        /// <returns>Chain object.</returns>
        public IRoleBootstrapper BindClaims(Claims claims)
        {
            this.currentRoleModuleBindings.Claims = claims;
            if (this.currentRoleModuleBindings.Modules != null)
            {
                this.roleModuleBindings.Add(this.currentRoleModuleBindings);
                this.currentRoleModuleBindings = new RoleModuleBindings();
            }

            return this;
        }

        /// <summary>
        /// Bootstraps a role template to the database.
        /// </summary>
        /// <param name="role">The role name to bootstrap the bindings to.</param>
        public void BootstrapTo(String role)
        {
            if (this.roleRepository.Has(r => SqlMethods.Like(r.Name, role)))
            {
                // Already bootstrapped.
                return;
            }

            var roleTemplateEntity = new RoleTemplate {Name = role};

            // Add the bootstrapped role.
            this.roleRepository.Add(roleTemplateEntity);

            var moduleEntities = this.moduleRepository.GetAll().ToList();
            var claimEntities = this.claimRepository.GetAll().ToList();

            this.roleModuleBindings.ForEach(binding =>
            {
                var bindingModuleEntities = moduleEntities.Where(m => binding.Modules.Contains(m.Name)).ToList();
                bindingModuleEntities.ForEach(bindingModuleEntity =>
                {
                    var claimFlags = binding.Claims.GetFlags().Cast<Claims>().ToList();
                    claimEntities.ForEach(claimEntity =>
                    {
                        if (claimFlags.Any(claimFlag => ((int) claimFlag) == claimEntity.Value))
                        {
                            roleTemplateEntity.RoleTemplateModuleClaims.Add(new RoleTemplateModuleClaim
                            {
                                ModuleId = bindingModuleEntity.Id,
                                ClaimId = claimEntity.Id,
                                RoleTemplate = roleTemplateEntity
                            });
                        }
                    });
                });
            });

            // Add the bootstrapped role.
            this.roleRepository.Update(roleTemplateEntity);
            this.roleModuleBindings = new List<RoleModuleBindings>();
        }

        /// <summary>
        /// Starts the bootstrap.
        /// Sets the modules for which we'll bind claims.
        /// </summary>
        /// <param name="modules">The modules on which to bind claims.</param>
        /// <returns>Chain object.</returns>
        public IRoleBootstrapper ToModules(params string[] modules)
        {
            this.currentRoleModuleBindings.Modules = modules;
            if (this.currentRoleModuleBindings.Claims != default(Claims))
            {
                this.roleModuleBindings.Add(this.currentRoleModuleBindings);
                this.currentRoleModuleBindings = new RoleModuleBindings();
            }

            return this;
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    internal class RoleModuleBindings
    {
        internal String[] Modules { get; set; }
        internal Claims Claims { get; set; }
    }
}