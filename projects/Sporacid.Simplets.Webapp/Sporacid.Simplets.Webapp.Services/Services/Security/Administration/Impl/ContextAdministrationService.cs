namespace Sporacid.Simplets.Webapp.Services.Services.Security.Administration.Impl
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Core.Security.Database.Repositories;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Impl;
    using Sporacid.Simplets.Webapp.Tools.Collections;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{context:alpha}/administration")]
    public class ContextAdministrationController : BaseSecureService, IContextAdministrationService
    {
        private readonly ISecurityRepository<Int32, Context> contextRepository;
        private readonly ISecurityRepository<Int32, Principal> principalRepository;
        private readonly ISecurityRepository<Int32, RoleTemplate> roleTemplateRepository;

        public ContextAdministrationController(ISecurityRepository<Int32, Principal> principalRepository, ISecurityRepository<Int32, RoleTemplate> roleTemplateRepository,
            ISecurityRepository<Int32, Context> contextRepository)
        {
            this.principalRepository = principalRepository;
            this.roleTemplateRepository = roleTemplateRepository;
            this.contextRepository = contextRepository;
        }

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
        public Int32 Create(String context, String owner)
        {
            // Cannot add the same context twice.
            if (this.contextRepository.Has(context2 => context2.Name == context))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ContextDuplicate, context));
            }

            // Create the context.
            var contextEntity = new Context {Name = context};
            this.contextRepository.Add(contextEntity);

            // Bind admin role to the owner on the newly created context.
            this.BindRoleToPrincipal(context, SecurityConfig.Role.Administrateur.ToString(), owner);

            return contextEntity.Id;
        }

        /// <summary>
        /// Binds a role to a principal in a given context.
        /// Every previous claims of the principal on this context will be removed beforehand.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <param name="role">The role name.</param>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="EntityNotFoundException{TEntity}">
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
        [HttpPut, Route("bind/{role:alpha}/to/{identity}")]
        [InvalidateCacheOutput("GetAllClaimsOnContext", typeof (ContextController))]
        public void BindRoleToPrincipal(String context, String role, String identity)
        {
            // Remove all claims from this user. A tad slow because we need to query the context and principal twice.
            this.RemoveAllClaimsFromPrincipal(context, identity);

            // Get all required entities.
            var contextEntity = this.contextRepository
                .GetUnique(context2 => context2.Name == context);
            var roleTemplateEntity = this.roleTemplateRepository
                .GetUnique(roleTemplate => roleTemplate.Name == role);
            var principalEntity = this.principalRepository
                .GetUnique(principal => principal.Identity == identity);

            // Apply the role template on this context.
            roleTemplateEntity.RoleTemplateModuleClaims.ForEach(roleTemplateModuleClaims => principalEntity.PrincipalModuleContextClaims.Add(new PrincipalModuleContextClaims
            {
                Principal = principalEntity,
                ContextId = contextEntity.Id,
                ModuleId = roleTemplateModuleClaims.ModuleId,
                Claims = roleTemplateModuleClaims.Claims
            }));

            // Update the principal.
            this.principalRepository.Update(principalEntity);
        }

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
        [HttpDelete, Route("unbind-claims-from/{identity}")]
        [InvalidateCacheOutput("GetAllClaimsOnContext", typeof (ContextController))]
        public void RemoveAllClaimsFromPrincipal(String context, String identity)
        {
            var principalEntity = this.principalRepository
                .GetUnique(principal => principal.Identity == identity);

            // Remove all claims from the principal for this context.
            var principalClaimsOnContext = principalEntity.PrincipalModuleContextClaims
                .Where(pc => pc.Context.Name == context)
                .ToList();
            principalClaimsOnContext.ForEach(pc => principalEntity.PrincipalModuleContextClaims.Remove(pc));

            // Update the principal.
            this.principalRepository.Update(principalEntity);
        }

        /// <summary>
        /// Merges the claims of a given principal on a given context with the claims given by a role.
        /// For example, if a principal has the "read" claim on "context1" and the role "role1" has the
        /// "create" and "read" claims, then the principal would end up with "read" and "create" on "context1".
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
        [InvalidateCacheOutput("GetAllClaimsOnContext", typeof (ContextController))]
        public void MergeClaimsOfPrincipalWithRole(String context, String role, String identity)
        {
            var contextEntity = this.contextRepository
                .GetUnique(context2 => context2.Name == context);
            var roleTemplateEntity = this.roleTemplateRepository
                .GetUnique(roleTemplate => roleTemplate.Name == role);
            var principalEntity = this.principalRepository
                .GetUnique(principal => principal.Identity == identity);

            var principalContextClaims = principalEntity.PrincipalModuleContextClaims
                .Where(pmcc => pmcc.Context.Name == context)
                .ToList();

            // Foreach claims of the role template.
            roleTemplateEntity.RoleTemplateModuleClaims.ForEach(roleTemplateModuleClaims =>
            {
                var principalModuleContextClaims = principalContextClaims
                    .FirstOrDefault(pmcc => pmcc.Module.Name == roleTemplateModuleClaims.Module.Name);

                // Check if the claims exists on principal.
                if (principalModuleContextClaims != null)
                {
                    // Claims already exists, merge the claims.
                    principalModuleContextClaims.Claims |= roleTemplateModuleClaims.Claims;
                }
                else
                {
                    // Claims do not exist, add them.
                    principalEntity.PrincipalModuleContextClaims.Add(new PrincipalModuleContextClaims
                    {
                        Principal = principalEntity,
                        ContextId = contextEntity.Id,
                        ModuleId = roleTemplateModuleClaims.ModuleId,
                        Claims = roleTemplateModuleClaims.Claims
                    });
                }
            });

            // Update the all claims.
            this.principalRepository.Update(principalEntity);
        }
    }
}