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
            return this.PartialView("Club/_CommanditairesComplets");
        }

        public ActionResult Commandites()
        {
          return this.PartialView("Club/_CommanditesCompletes");
        }

        public ActionResult Evenements()
        {
            throw new NotImplementedException();
        }

        public ActionResult Fournisseurs()
        {
            return this.PartialView("Club/_Fournisseurs");
        }

        public ActionResult Meetings()
        {
            throw new NotImplementedException();
        }
    }
}