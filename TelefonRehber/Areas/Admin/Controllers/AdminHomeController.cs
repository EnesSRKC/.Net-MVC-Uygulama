using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TelefonRehber.Models.EntityFramework;

namespace TelefonRehber.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class AdminHomeController : Controller
    {
        DbTelefonRehberEntities db = new DbTelefonRehberEntities();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return RedirectToAction("Index","PersonelIslem",new { area = "Admin"});
        }
    }
}