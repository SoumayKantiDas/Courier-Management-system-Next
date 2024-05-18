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
    public class AreaInfoesController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: AreaInfoes
        public ActionResult Index()
        {
            // Fetch the list of AreaInfoes where status is true
            var activeAreas = db.AreaInfoes.Where(a => a.status == true).ToList();
            return View(activeAreas);
        }


        // GET: AreaInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaInfo areaInfo = db.AreaInfoes.Find(id);
            if (areaInfo == null)
            {
                return HttpNotFound();
            }
            return View(areaInfo);
        }

        // GET: AreaInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aAreaID,aAreaName,aCost")] AreaInfo areaInfo)
        {
            if (ModelState.IsValid)
            {
                db.AreaInfoes.Add(areaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaInfo);
        }

        // GET: AreaInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaInfo areaInfo = db.AreaInfoes.Find(id);
            if (areaInfo == null)
            {
                return HttpNotFound();
            }
            return View(areaInfo);
        }

        // POST: AreaInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aAreaID,aAreaName,aCost")] AreaInfo areaInfo)
        {
            if (ModelState.IsValid)
            {
                areaInfo.status = true;
                
                db.Entry(areaInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaInfo);
        }

       
        // GET: AreaInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaInfo areaInfo = db.AreaInfoes.Find(id);
            if (areaInfo == null)
            {
                return HttpNotFound();
            }
            return View(areaInfo);
        }

        // POST: AreaInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaInfo areaInfo = db.AreaInfoes.Find(id);
            if (areaInfo != null)
            {
                // Set the status to false instead of removing the record
                areaInfo.status = false;
                db.Entry(areaInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
