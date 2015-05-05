namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ClubController : Controller
    {
        public ActionResult Membres()
        {
            return this.View("Fulls/Userspace/Membres");
        }

        public ActionResult Commanditaires()
        {
            throw new NotImplementedException();
        }
    }
}