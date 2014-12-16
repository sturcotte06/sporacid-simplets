namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/{context:alpha}/administration")]
    public class ContextAdministrationService : BaseService, IContextAdministrationService
    {
        private readonly IMembreService membreService;
        private readonly IRepository<Int32, Core.Security.Database.Principal> principalRepository;
        private readonly IRepository<Int32, Core.Security.Database.RoleTemplate> roleTemplateRepository;

        public ContextAdministrationService(IMembreService membreService, IRepository<Int32, Core.Security.Database.Principal> principalRepository,
            IRepository<Int32, Core.Security.Database.RoleTemplate> roleTemplateRepository)
        {
            this.principalRepository = principalRepository;
            this.roleTemplateRepository = roleTemplateRepository;
            this.membreService = membreService;
        }

        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="memberId">The member id.</param>
        [HttpPost]
        [Route("bind/{role:alpha}/to/{membreId:int}")]
        public void BindRole(String context, String role, int memberId)
        {
            var membre = this.membreService.Get(memberId);
            var principalEntity = this.principalRepository.GetUnique(p => p.Identity == membre.CodeUniversel);

            // Remove all claims from the principal for this context.
            var principalClaims = principalEntity.PrincipalResourceClaims;
            principalClaims.ForEach(prc =>
            {
                if (String.Equals(prc.Resource.Module.Name, context, StringComparison.CurrentCultureIgnoreCase))
                {
                    principalClaims.Remove(prc);
                }
            });

            // Get a role template.
            var roleTemplate = this.roleTemplateRepository.GetUnique(
                r => String.Equals(r.Name, role, StringComparison.CurrentCultureIgnoreCase));

            // Bind all claims of the template to the user.
            roleTemplate.RoleTemplateModuleClaims.ForEach(rtmc => rtmc.Module.Resources.ForEach(r => principalClaims.Add(new PrincipalResourceClaim
            {
                ClaimId = rtmc.ClaimId,
                PrincipalId = principalEntity.Id,
                ResourceId = r.Id
            })));

            this.principalRepository.Update(principalEntity);
        }
    }
}