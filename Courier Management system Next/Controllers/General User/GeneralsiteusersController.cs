using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Courier_Management_system_Next.Models;

namespace Courier_Management_system_Next.Controllers.General_User
{
    public class GeneralsiteusersController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: Generalsiteusers
        public ActionResult Index()
        {
            int? userId = Session["UserId"] as int?;
            var siteusers = db.siteusers
              .Where(s => s.SiteUserid == userId) // Filter by user ID
              .ToList();
            return View(siteusers.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
