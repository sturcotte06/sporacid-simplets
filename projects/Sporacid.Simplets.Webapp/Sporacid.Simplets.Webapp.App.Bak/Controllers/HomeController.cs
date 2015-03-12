namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Fulls/Profil");
        }

        public ActionResult ApiHelp()
        {
            return this.View("Fulls/ApiHelp");
        }

        public ActionResult EntitiesHelp()
        {
            return this.View("Fulls/EntitiesHelp");
        }
    }
}