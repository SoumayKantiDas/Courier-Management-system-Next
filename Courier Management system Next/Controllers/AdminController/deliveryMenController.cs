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
    public class deliveryMenController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: deliveryMen
        public ActionResult Index()
        {
            var activeDeliveryMen = db.deliveryMen.Where(dm => dm.status == true).ToList();
            return View(activeDeliveryMen);
        }


        // GET: deliveryMen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryMan deliveryMan = db.deliveryMen.Find(id);
            if (deliveryMan == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMan);
        }

        // GET: deliveryMen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: deliveryMen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dID,dFirstName,dLastName,dPhoneNo,dSalary,dAddress,status")] deliveryMan deliveryMan)
        {
            if (ModelState.IsValid)
            {
                db.deliveryMen.Add(deliveryMan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryMan);
        }

        public ActionResult ShipperReport()
        {
            var shippersWithBookings = db.deliveryMen.Include(s => s.bookings).ToList(); // Retrieve all shippers with their bookings
            return new ViewAsPdf("ShipperReport", shippersWithBookings)
            {
                FileName = "shippers.pdf"
            };
        }


        // GET: deliveryMen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryMan deliveryMan = db.deliveryMen.Find(id);
            if (deliveryMan == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMan);
        }

        // POST: deliveryMen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dID,dFirstName,dLastName,dPhoneNo,dSalary,dAddress,status")] deliveryMan deliveryMan)
        {
            if (ModelState.IsValid)
            {
                deliveryMan.status = true;
                db.Entry(deliveryMan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryMan);
        }

        // GET: deliveryMen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryMan deliveryMan = db.deliveryMen.Find(id);
            if (deliveryMan == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMan);
        }

        // POST: deliveryMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find the delivery man by id
            deliveryMan deliveryMan = db.deliveryMen.Find(id);

            if (deliveryMan != null)
            {
                // Set the status to false
                deliveryMan.status = false;
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
