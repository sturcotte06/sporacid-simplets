namespace Sporacid.Simplets.Webapp.Services.Services.Security.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Core.Security.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{context}")]
    public class ContextController : BaseSecureService, IContextService
    {
        private readonly ISecurityRepository<Int32, Context> contextRepository;

        public ContextController(ISecurityRepository<Int32, Context> contextRepository)
        {
            this.contextRepository = contextRepository;
        }

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
        [HttpGet, Route("claims")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<KeyValuePair<String, Claims>> GetAllClaimsOnContext(String context)
        {
            var identity = HttpContext.Current.User.Identity.Name;
            return this.contextRepository
                .GetUnique(context2 => context2.Name == context)
                .PrincipalModuleContextClaims
                .Where(pmcc => pmcc.Context.Name == context && pmcc.Principal.Identity == identity)
                .ToDictionary(pmcc => pmcc.Module.Name, pmcc => (Claims) pmcc.Claims);
        }
    }
}