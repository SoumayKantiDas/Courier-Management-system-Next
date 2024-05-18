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
            int? SiteUserid = Session["SiteUserid"] as int?;
            // Retrieve role ID from session
            int? userTypeId = Session["UserTypeId"] as int?;

            var bookings = db.bookings
            .Where(b => b.SiteUserid == SiteUserid)
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

        public ActionResult GetCourierByTrackingNumberSeachbar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCourierByTrackingNumberSeachbar(string trackingNumber)
        {
            // Check if the email is not empty (you can add further validation if needed)
            if (!string.IsNullOrEmpty(trackingNumber))
            {
                // Store the email in the session
                Session["TrackingNumber"] = trackingNumber;
                return RedirectToAction("GetCourierByTrackingNumber", "Bookings");
            }

            // Redirect back to the Getsession action
            return RedirectToAction("GetCourierByTrackingNumberSeachbar");
        }
        public ActionResult GetCourierByTrackingNumber()
        {

            string trackingNumber = Session["TrackingNumber"] as string;
            var Courier = db.bookings.Where(u => u.TrackingNumber == trackingNumber);

            return View(Courier.ToList());


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
