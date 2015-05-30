namespace Sporacid.Simplets.Webapp.Services.Services.Public.Impl
{
  using System;
  using System.Reflection;
  using System.Web.Http;
  using System.Linq;
  using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
  using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath)]
    public class AnonymousController : BaseSecureService, IAnonymousService
    {
        public AnonymousController()
        {
            Console.WriteLine("");
        }

        /// <summary>
        /// Dummy method that has no side effect on the system.
        /// However, the request will pass through the api's pipeline; it can therefore be used to test a principal's rights in the
        /// system.
        /// </summary>
        [HttpGet, Route("no-op")]
        public void NoOp()
        {
        }

        /// <summary>
        /// Create an empty dto object from the given entity name.
        /// </summary>
        /// <param name="entityName">Entity name without "Dto" part.</param>
        /// <returns>An empty dto object from the given entity name.</returns>
        [HttpGet, Route("empty-{entityName}")]
        [CacheOutput(ServerTimeSpan = (Int32)CacheDuration.Maximum, ClientTimeSpan = (Int32)CacheDuration.Maximum)]
        public dynamic Empty(string entityName)
        {
          var dtoEntityName = string.Format("{0}Dto", entityName);
          var dtoEntityType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name.Equals(dtoEntityName, StringComparison.InvariantCultureIgnoreCase));

          if (dtoEntityType == null)
          {
            throw new EntityNotFoundException<Object>();
          }

          return Activator.CreateInstance(dtoEntityType);
        }
    }
}