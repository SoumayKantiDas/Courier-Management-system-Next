using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Courier_Management_system_Next.Models;
using Rotativa;

namespace Courier_Management_system_Next.Controllers.AdminController
{
    public class siteusersController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: siteusers
        public ActionResult Index()
        {
            var siteusers = db.siteusers.Where(s => s.status==true && s.UserTypeId == 2).ToList();
            return View(siteusers);
        }


        // GET: siteusers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siteuser siteuser = db.siteusers.Find(id);
            if (siteuser == null)
            {
                return HttpNotFound();
            }
            return View(siteuser);
        }

        // GET: siteusers/Create
        public ActionResult Create()
        {
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype1");
            return View();
        }

        // POST: siteusers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteUserid,username,email,password,address,UserTypeId,status")] siteuser siteuser)
        {
            if (ModelState.IsValid)
            {
                db.siteusers.Add(siteuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype1", siteuser.UserTypeId);
            return View(siteuser);
        }

        // GET: siteusers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siteuser siteuser = db.siteusers.Find(id);
            if (siteuser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype1", siteuser.UserTypeId);
            return View(siteuser);
        }

        // POST: siteusers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteUserid,username,email,password,address,UserTypeId,status")] siteuser siteuser)
        {
            if (ModelState.IsValid)
            {
                siteuser.status=true;
                db.Entry(siteuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype1", siteuser.UserTypeId);
            return View(siteuser);
        }

        // GET: siteusers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siteuser siteuser = db.siteusers.Find(id);
            if (siteuser == null)
            {
                return HttpNotFound();
            }
            return View(siteuser);
        }

        // POST: siteusers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            siteuser siteuser = db.siteusers.Find(id);
            if (siteuser != null)
            {
                // Instead of removing, update the status bit
                siteuser.status = false;
                db.Entry(siteuser).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult GenerateUserStatusReport()
        {
            // Retrieve specific site user with their bookings from the database
            var siteUsers = db.siteusers
                              .Where(s => s.UserTypeId == 2)
                              .Include(s => s.bookings)
                              .ToList();

            // Pass the site users data to the view
            return new ViewAsPdf("GenerateUserStatusReport", siteUsers)
            {
                FileName = "UserStatusReport.pdf"
            };
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
