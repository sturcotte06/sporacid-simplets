namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RequiresAuthenticatedPrincipal]
    [RequiresAuthorizedPrincipal]
    [HandlesException]
    public abstract class BaseService : ApiController
    {
        /// <summary>
        /// The services base path.
        /// </summary>
        protected const string BasePath = "api/v1";
    }
}