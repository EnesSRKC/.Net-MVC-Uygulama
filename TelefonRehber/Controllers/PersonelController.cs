using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehber.Models.EntityFramework;
using TelefonRehber.ViewModels;

namespace TelefonRehber.Controllers
{
    public class PersonelController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.TBL_PERSONEL.Include(m => m.TBL_DEPARTMAN).Include(m => m.TBL_PERSONEL2).ToList();


            return View(model);
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var model = db.TBL_PERSONEL.Include(m => m.TBL_DEPARTMAN).Include(m => m.TBL_PERSONEL2).First(m => m.ID == id);
            if (model == null)
                return HttpNotFound();


            return View(model);
        }
    }
}