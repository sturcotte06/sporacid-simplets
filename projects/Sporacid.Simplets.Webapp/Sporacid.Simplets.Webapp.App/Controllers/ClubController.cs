namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ClubController : Controller
    {
        public ActionResult Membres()
        {
            return this.PartialView("Club/_Membres");
        }

        public ActionResult Commanditaires()
        {
            throw new NotImplementedException();
        }

        public ActionResult Evenements()
        {
            throw new NotImplementedException();
        }

        public ActionResult Meetings()
        {
            throw new NotImplementedException();
        }
    }
}