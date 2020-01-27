using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehber.Models.EntityFramework;

namespace TelefonRehber.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class DepartmanIslemController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Admin/Departman
        public ActionResult Index(bool? WasDeleted)
        {
            var model = db.TBL_DEPARTMAN.ToList();

            //Silme işleminin durumu görünümde mesaj olarak gösterilmektedir.
            ViewBag.WasDeleted = WasDeleted;
            return View(model);
        }


        public ActionResult Ekle()
        {
            return View("Kaydet", new TBL_DEPARTMAN());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(TBL_DEPARTMAN departman)
        {
            if (!ModelState.IsValid)
                return View("Kaydet");
            //Eğer id değeri 0 geliyorsa ekleme işlemi, 0'dan farklı geliyorsa güncelleme işlemi yapılıyor
            if (departman.ID == 0)
                YeniDepartmanEkle(departman);
            else
                DepartmanGuncelle(departman);

            return RedirectToAction("Index", "DepartmanIslem");
        }

        private void YeniDepartmanEkle(TBL_DEPARTMAN departman)
        {
            db.TBL_DEPARTMAN.Add(departman);
            db.SaveChanges();
        }
        private void DepartmanGuncelle(TBL_DEPARTMAN departman)
        {
            db.Entry(departman).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.TBL_DEPARTMAN.Find(id);
            if (model == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


            return View("Kaydet", model);
        }


        public ActionResult Sil(int id)
        {
            bool WasDeleted = true;
            var departman = db.TBL_DEPARTMAN.Find(id);
            if (departman == null)
                return HttpNotFound();

            if (db.TBL_PERSONEL.Where(x => x.DEPARTMANID == id).Count() == 0)
            {
                db.TBL_DEPARTMAN.Remove(departman);
                db.SaveChanges();
                WasDeleted = true;
            }
            else
            {
                WasDeleted = false;
                return RedirectToAction("Index","DepartmanIslem", new { WasDeleted = WasDeleted });
            }

            return RedirectToAction("Index", "DepartmanIslem", new { WasDeleted = WasDeleted});
        }
    }
}