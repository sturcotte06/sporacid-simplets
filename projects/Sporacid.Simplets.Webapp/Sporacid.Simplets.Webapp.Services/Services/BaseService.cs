namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authorization;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Authenticated]
    [Authorized]
    [HandlesException]
    public abstract class BaseService : ApiController
    {
    }
}