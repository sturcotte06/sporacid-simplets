namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    // [AuthenticatedAndAuthorized]
    [HandlesException]
    public abstract class BaseService : ApiController
    {
        /// <summary>
        /// The services base path.
        /// </summary>
        public const string BasePath = "v1/api";
    }
}