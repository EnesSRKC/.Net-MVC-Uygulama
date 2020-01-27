using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TelefonRehber.Models.EntityFramework;
using TelefonRehber.ViewModels;

namespace TelefonRehber.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class PersonelIslemController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Admin/PersonelIslem
        public ActionResult Index(bool? WasDeleted)
        {
            var model = db.TBL_PERSONEL.Include(m => m.TBL_DEPARTMAN).Include(m => m.TBL_PERSONEL2).ToList();
            //Silme işleminin durumu görünümde mesaj olarak gösterilmektedir.
            ViewBag.WasDeleted = WasDeleted;
            return View(model);
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = db.TBL_PERSONEL.Include(m => m.TBL_DEPARTMAN).Include(m => m.TBL_PERSONEL2).FirstOrDefault(m => m.ID == id);
            if (model == null)
                return HttpNotFound();


            return View(model);
        }

        public ActionResult Ekle()
        {
            var model = new PersonelViewModel()
            {
                Departmanlar = db.TBL_DEPARTMAN.ToList(),
                Personel = new TBL_PERSONEL()
            };

            //Veritabanındaki tablodan birden fazla sütunu SelectList'e eklemek için anonymous type liste oluşturuluyor
            var listPersonel = db.TBL_PERSONEL.Select(x => new
            {
                ID = x.ID,
                AdSoyad = x.AD + " " + x.SOYAD
            }).ToList();

            ViewBag.PersonelListe = new SelectList(listPersonel, "ID", "AdSoyad");
            return View("Kaydet",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(TBL_PERSONEL personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelViewModel()
                {
                    Departmanlar = db.TBL_DEPARTMAN.ToList(),
                    Personel = personel
                };

                //Veritabanındaki tablodan birden fazla sütunu SelectList'e eklemek için anonymous type liste oluşturuluyor
                var listPersonel = db.TBL_PERSONEL.Select(x => new
                {
                    ID = x.ID,
                    AdSoyad = x.AD + " " + x.SOYAD
                }).ToList();

                ViewBag.PersonelListe = new SelectList(listPersonel, "ID", "AdSoyad");
                return View("Kaydet", model);
            }
            if (personel.ID == 0)
                PersonelEkle(personel);
            else
                PersonelGuncelle(personel);

            return RedirectToAction("Index","PersonelIslem", new { area = "Admin"});
        }

        private void PersonelEkle(TBL_PERSONEL personel)
        {
            db.TBL_PERSONEL.Add(personel);
            db.SaveChanges();
        }

        private void PersonelGuncelle(TBL_PERSONEL personel)
        {
            db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelViewModel()
            {
                Departmanlar = db.TBL_DEPARTMAN.ToList(),
                Personel = db.TBL_PERSONEL.Find(id)
            };

            //Veritabanındaki tablodan birden fazla sütunu SelectList'e eklemek için anonymous type liste oluşturuluyor
            var listPersonel = db.TBL_PERSONEL.Select(x => new
            {
                ID = x.ID,
                AdSoyad = x.AD + " " + x.SOYAD
            }).ToList();

            ViewBag.PersonelListe = new SelectList(listPersonel, "ID", "AdSoyad");

            return View("Kaydet", model);
        }

        public ActionResult Sil(int id)
        {
            bool WasDeleted = true;
            var personel = db.TBL_PERSONEL.Find(id);
            if (personel == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            //Eğer silinmek istenen personel başka bir personelin yöneticisi değilse silme işlemi yapılıyor
            if (db.TBL_PERSONEL.Where(m => m.YONETICIID == id).Count() == 0)
            {
                db.TBL_PERSONEL.Remove(personel);
                db.SaveChanges();

                //Index action'ında hata mesajı göstermek için WasDeleted değişkenine değer atanıyor
                WasDeleted = true;
            }
            else
            {
                WasDeleted = false;
                return RedirectToAction("Index", "PersonelIslem", new { area = "Admin", WasDeleted = WasDeleted });
            }

            return RedirectToAction("Index", "PersonelIslem", new { area = "Admin", WasDeleted = WasDeleted });

        }

    }
}