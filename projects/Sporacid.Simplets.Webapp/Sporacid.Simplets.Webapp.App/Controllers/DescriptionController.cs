namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System.Web.Mvc;

    public class DescriptionController : Controller
    {
        public ActionResult Api()
        {
            return this.View("Description/_ApiDescription");
        }

        public ActionResult Entities()
        {
            return this.View("Description/_EntitiesDescription");
        }
    }
}