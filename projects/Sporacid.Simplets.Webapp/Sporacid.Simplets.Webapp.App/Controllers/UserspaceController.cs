namespace Sporacid.Simplets.Webapp.App.Controllers
{
    using System;
    using System.Web.Mvc;

    public class UserspaceController : Controller
    {
        public ActionResult Profil()
        {
            return this.PartialView("Userspace/_ProfilComplet");
        }

        public ActionResult Preferences()
        {
            throw new NotImplementedException();
        }
    }
}