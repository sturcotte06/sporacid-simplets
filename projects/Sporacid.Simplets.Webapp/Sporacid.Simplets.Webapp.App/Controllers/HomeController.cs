namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Application");
        }
    }
}