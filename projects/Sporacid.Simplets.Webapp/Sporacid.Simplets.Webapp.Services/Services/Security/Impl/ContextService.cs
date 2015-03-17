namespace Sporacid.Simplets.Webapp.Services.Services.Security.Impl
{
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Security")]
    [FixedContext(SecurityConfig.SystemContext)]
    [RoutePrefix(BasePath + "/context")]
    public class ContextService : BaseSecureService, IContextService
    {
    }
}