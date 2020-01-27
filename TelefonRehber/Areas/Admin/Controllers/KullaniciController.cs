using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehber.Models.EntityFramework;
using TelefonRehber.ViewModels;

namespace TelefonRehber.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class KullaniciController : Controller
    {

        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Admin/Kullanici
        public ActionResult SifreDegistir(string username)
        {
            var kullanici = db.TBL_KULLANICI.FirstOrDefault(m => m.KULLANICIAD == username);

            if (kullanici == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            SifreDegistirViewModel model = new SifreDegistirViewModel()
            {
                ID = kullanici.ID
            };
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult SifreDegistir(SifreDegistirViewModel user)
        {
            var kullanici = db.TBL_KULLANICI.FirstOrDefault(m => m.ID == user.ID);
            if (kullanici == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            if (!ModelState.IsValid)
            {
                return View("SifreDegistir", new SifreDegistirViewModel() { ID = kullanici.ID});
            }
            else
            {
                if (user.SifreEski != kullanici.SIFRE)
                {
                    ViewBag.Mesaj = "Eski şifrenizi kontrol ediniz.";
                    return View("SifreDegistir", new SifreDegistirViewModel() { ID = kullanici.ID });
                }
                else if (user.SifreYeni != user.SifreYeniOnay)
                {
                    ViewBag.Mesaj = "Şifre tekrarı yeni şifre ile aynı olmalıdır.";
                    return View("SifreDegistir", new SifreDegistirViewModel() { ID = kullanici.ID });
                }
                else
                {
                    kullanici.SIFRE = user.SifreYeni;
                    db.SaveChanges();
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                }
            }
        }
    }
}