namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{context:alpha}/administration")]
    public class ContextAdministrationService : BaseService, IContextAdministrationService
    {
        private readonly IRepository<Int32, Context> contextRepository;
        private readonly IRepository<Int32, Principal> principalRepository;
        private readonly IRepository<Int32, RoleTemplate> roleTemplateRepository;

        public ContextAdministrationService(IRepository<Int32, Principal> principalRepository, IRepository<Int32, RoleTemplate> roleTemplateRepository,
            IRepository<Int32, Context> contextRepository)
        {
            this.principalRepository = principalRepository;
            this.roleTemplateRepository = roleTemplateRepository;
            this.contextRepository = contextRepository;
        }

        /// <summary>
        /// Creates a security context in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        [HttpPost]
        [Route("")]
        public Int32 CreateContext(String context)
        {
            // Create the context.
            var contextEntity = new Context {Name = context};
            this.contextRepository.Add(contextEntity);

            // Bind admin role to the current user on the newly create context.
            var principal = HttpContext.Current.User.Identity.Name;
            this.BindRoleToPrincipal(contextEntity.Name, SecurityConfig.Role.Administrateur.ToString(), principal);
            return contextEntity.Id;
        }

        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="identity">The principal identity.</param>
        [HttpPut]
        [Route("bind/{role:alpha}/to/{identity}")]
        public void BindRoleToPrincipal(String context, String role, String identity)
        {
            // Remove all claims from this user. A tad slow because we need to query the context and principal twice.
            this.RemoveAllClaimsFromPrincipal(context, identity);

            // Get all required entities.
            var contextEntity = this.contextRepository.GetUnique(c => SqlMethods.Like(c.Name, context));
            var roleTemplate = this.roleTemplateRepository.GetUnique(rt => SqlMethods.Like(rt.Name, role));
            var principalEntity = this.principalRepository.GetUnique(p => SqlMethods.Like(p.Identity, identity));

            // Apply the role template on this context.
            roleTemplate.RoleTemplateModuleClaims.ForEach(rtmc => principalEntity.PrincipalModuleContextClaims.Add(new PrincipalModuleContextClaims
            {
                Principal = principalEntity,
                ContextId = contextEntity.Id,
                Claims = rtmc.Claims,
                ModuleId = rtmc.ModuleId
            }));

            // Update the principal.
            this.principalRepository.Update(principalEntity);
        }

        /// <summary>
        /// Remove all claims from a user on the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="identity">The principal identity.</param>
        [HttpDelete]
        [Route("unbind-claims-from/{identity}")]
        public void RemoveAllClaimsFromPrincipal(String context, String identity)
        {
            // Get all required entities.
            var contextEntity = this.contextRepository.GetUnique(c => SqlMethods.Like(c.Name, context));
            var principalEntity = this.principalRepository.GetUnique(p => SqlMethods.Like(p.Identity, identity));

            // Remove all claims from the principal for this context.
            var principalClaimsOnContext = principalEntity.PrincipalModuleContextClaims
                .Where(pc => pc.ContextId == contextEntity.Id).ToList();
            principalClaimsOnContext.ForEach(pc => principalEntity.PrincipalModuleContextClaims.Remove(pc));

            // Update the principal.
            this.principalRepository.Update(principalEntity);
        }
    }
}