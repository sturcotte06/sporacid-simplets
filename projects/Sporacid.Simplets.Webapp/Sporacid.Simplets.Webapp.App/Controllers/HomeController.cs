namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Fulls/Userspace/Profil");
        }

        public ActionResult Membres()
        {
            return this.View("Fulls/Userspace/Membres");
        }

        public ActionResult ApiHelp()
        {
            return this.View("Fulls/Description/ApiHelp");
        }

        public ActionResult EntitiesHelp()
        {
            return this.View("Fulls/Description/EntitiesHelp");
        }
    }
}