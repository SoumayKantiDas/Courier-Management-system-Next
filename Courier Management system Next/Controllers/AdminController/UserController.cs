using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Courier_Management_system_Next.Models;

namespace Courier_Management_system_Next.Controllers.AdminController
{
    public class UserController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: User
        public ActionResult Index()
        {
            {// Retrieve user ID from session
                int? SiteUserid = Session["SiteUserid"] as int?;
                // Retrieve role ID from session
                int? userTypeId = Session["UserTypeId"] as int?;
                var siteusers = db.siteusers
              .Where(s => s.SiteUserid == SiteUserid) // Filter by user ID
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
