namespace Sporacid.Simplets.Webapp.Services.Services
{
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RequiresAuthenticatedPrincipal]
    [RequiresAuthorizedPrincipal]
    public class BaseSecureService : BaseService
    {
    }
}