namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System.Web.Http;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/inventaire")]
    public class InventaireService : BaseService, IFournisseurService
    {
    }
}