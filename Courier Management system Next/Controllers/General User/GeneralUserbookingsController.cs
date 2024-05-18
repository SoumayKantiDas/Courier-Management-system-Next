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
    public class GeneralUserbookingsController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: GeneralUserbookings
        public ActionResult Index()
        {
            int? userId = Session["UserId"] as int?;
            // Retrieve role ID from session
            int? roleId = Session["UserTypeId"] as int?;

            var bookings = db.bookings
            .Where(b => b.SiteUserid == userId)
              .Include(b => b.AreaInfo)
                .Include(b => b.AreaInfo1)
                 .Include(b => b.BookingDelivareyType)
                .Include(b => b.deliveryMan)
                .Include(b => b.siteuser)
            .ToList();

            return View(bookings);
        }

        // GET: GeneralUserbookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

       

        // POST: GeneralUserbookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booking booking = db.bookings.Find(id);
            db.bookings.Remove(booking);
            db.SaveChanges();
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
