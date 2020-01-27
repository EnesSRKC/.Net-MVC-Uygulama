using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelefonRehber.Models.EntityFramework;

namespace TelefonRehber.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBL_KULLANICI kullanici)
        {
            var klnc = db.TBL_KULLANICI.FirstOrDefault(m => m.KULLANICIAD == kullanici.KULLANICIAD && m.SIFRE == kullanici.SIFRE);
            if (klnc != null)
            {
                FormsAuthentication.SetAuthCookie(klnc.KULLANICIAD, false);

                if (klnc.ROLE == "A")
                    return RedirectToAction("Index", "AdminHome");

                return RedirectToAction("Index","Departman", new { area = ""});
            }

            return View();
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}