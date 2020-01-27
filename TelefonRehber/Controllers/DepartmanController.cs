using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TelefonRehber.Models.EntityFramework;

namespace TelefonRehber.Controllers
{
    public class DepartmanController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Departman
        public ActionResult Index()
        {
            var model = db.TBL_DEPARTMAN.ToList();
            return View(model);
        }
    }
}