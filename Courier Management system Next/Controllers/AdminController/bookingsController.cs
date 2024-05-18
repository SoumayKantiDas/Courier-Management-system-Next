using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Courier_Management_system_Next.Models;
using Courier_Management_system_Next.Utilities;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using System.Web.UI;
using Rotativa;
using System.Data.Entity.Validation;


namespace Courier_Management_system_Next.Controllers.AdminController
{
    public class bookingsController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();

        // GET: bookings
        public ActionResult Index(int? i)
        {
            var bookings = db.bookings
                .Include(b => b.AreaInfo)
                .Include(b => b.AreaInfo1)
                .Include(b => b.BookingDelivareyType)
                .Include(b => b.deliveryMan)
                .Include(b => b.siteuser)
                .Where(b => b.AreaInfo.status == true && b.AreaInfo1.status == true)
                .ToList()
                .ToPagedList(i ?? 1, 10);

            return View(bookings);
        }

        public ActionResult GetTracingNumberAndCost(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
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
            string truckingNumber = Session["TrackingNumber"] as string;
            var Courier = db.bookings.Where(u => u.TrackingNumber == truckingNumber);

            return View(Courier.ToList());


        }

        // GET: bookings/Details/5
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

        public ActionResult Create()
        {
            ViewBag.DestAreaID = new SelectList(db.AreaInfoes, "aAreaID", "aAreaName");
            ViewBag.OriginAreaID = new SelectList(db.AreaInfoes, "aAreaID", "aAreaName");
            ViewBag.BookingTypeId = new SelectList(db.BookingDelivareyTypes, "BookingTypeId", "Type");
            ViewBag.dID = new SelectList(db.deliveryMen, "dID", "dFirstName");
            ViewBag.SiteUserid = new SelectList(db.siteusers, "SiteUserid", "username");
            return View();
        }

        // POST: bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookID,bookOriginFirstName,bookOriginLastName,bookOriginAddress,bookOriginPhoneNo,bookDestFirstName,bookDestLastName,bookDestAddress,bookDestPhoneNo,bookingProductWeight,bookDescription,BookingTypeId,bCost,SiteUserid,dID,OriginAreaID,DestAreaID,bDestCost,TrackingNumber,status,statusbit")] booking booking)
        {
            if (ModelState.IsValid)
            {
                var originArea = db.AreaInfoes.FirstOrDefault(a => a.aAreaID == booking.OriginAreaID);
                var destArea = db.AreaInfoes.FirstOrDefault(a => a.aAreaID == booking.DestAreaID);
                var bookingType = db.BookingDelivareyTypes.FirstOrDefault(bt => bt.BookingTypeId == booking.BookingTypeId);

                if (originArea != null && destArea != null && bookingType != null)
                {
                    var OriginAmmount = originArea.aCost;
                    var DestinationAmmount = destArea.aCost;
                    var BookingTypeCost = bookingType.BTCost;
                    var TotalCost = OriginAmmount + DestinationAmmount + BookingTypeCost;

                    booking.bCost = TotalCost;
                    booking.status = "Received";
                    // Generate a unique tracking number
                    booking.TrackingNumber = TrackingNumberGenerator.GenerateTrackingNumber();
                    // Ensure bookingDate is set to current date and time
                    booking.bookingDate = DateTime.Now;

                    db.bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("GetTracingNumberAndCost", new { id = booking.bookID });
                    //  return RedirectToAction("Index");
                }
                else
                {
                    // Handle cases where any of the areas or booking type is not found
                    ModelState.AddModelError("", "Invalid Origin Area, Destination Area, or Booking Type.");
                }
            }

            // Repopulate the ViewBag properties in case of an error
            ViewBag.DestAreaID = new SelectList(db.AreaInfoes, "aAreaID", "aAreaName", booking.DestAreaID);
            ViewBag.OriginAreaID = new SelectList(db.AreaInfoes, "aAreaID", "aAreaName", booking.OriginAreaID);
            ViewBag.BookingTypeId = new SelectList(db.BookingDelivareyTypes, "BookingTypeId", "Type", booking.BookingTypeId);
            ViewBag.dID = new SelectList(db.deliveryMen, "dID", "dFirstName", booking.dID);
            ViewBag.SiteUserid = new SelectList(db.siteusers, "SiteUserid", "username", booking.SiteUserid);

            return View(booking);
        }


        public ActionResult ConsignmentReport()
        {
            var consignments = db.bookings.ToList(); // Retrieve all consignments from the database
            return new ViewAsPdf("ConsignmentReport", consignments)
            {
                FileName = "Consignments.pdf"
            };
        }
        public ActionResult PickupReport()
        {
            var pickups = db.bookings.ToList();

            return new ViewAsPdf("PickupReport", pickups)
            {
                FileName = "PickupReport.pdf"
            };
        }



        // GET: bookings/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.DestAreaID = new SelectList(db.AreaInfoes.Where(a => a.status == true), "aAreaID", "aAreaName", booking.DestAreaID);
            ViewBag.OriginAreaID = new SelectList(db.AreaInfoes.Where(a => a.status == true), "aAreaID", "aAreaName", booking.OriginAreaID);
            ViewBag.BookingTypeId = new SelectList(db.BookingDelivareyTypes.Where(a => a.status == true), "BookingTypeId", "Type", booking.BookingTypeId);
            ViewBag.dID = new SelectList(db.deliveryMen.Where(a => a.status == true), "dID", "dFirstName", booking.dID);
            ViewBag.SiteUserid = new SelectList(db.siteusers.Where(a => a.status == true), "SiteUserid", "username", booking.SiteUserid);
            return View(booking);
        }

        // POST: bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookID,bookOriginFirstName,bookOriginLastName,bookOriginAddress,bookOriginPhoneNo,bookDestFirstName,bookDestLastName,bookDestAddress,bookDestPhoneNo,bookingProductWeight,bookDescription,BookingTypeId,bCost,SiteUserid,dID,OriginAreaID,DestAreaID,bDestCost,TrackingNumber,status,statusbit")] booking booking)
        {

            if (ModelState.IsValid)
            
            {
                var originArea = db.AreaInfoes.FirstOrDefault(a => a.aAreaID == booking.OriginAreaID);
                var destArea = db.AreaInfoes.FirstOrDefault(a => a.aAreaID == booking.DestAreaID);
                var bookingType = db.BookingDelivareyTypes.FirstOrDefault(bt => bt.BookingTypeId == booking.BookingTypeId);

                booking.statusbit = true;
                db.Entry(booking).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DestAreaID = new SelectList(db.AreaInfoes.Where(a => a.status == true), "aAreaID", "aAreaName", booking.DestAreaID);
            ViewBag.OriginAreaID = new SelectList(db.AreaInfoes.Where(a => a.status == true), "aAreaID", "aAreaName", booking.OriginAreaID);
            ViewBag.BookingTypeId = new SelectList(db.BookingDelivareyTypes.Where(a => a.status == true), "BookingTypeId", "Type", booking.BookingTypeId);
            ViewBag.dID = new SelectList(db.deliveryMen.Where(a => a.status == true), "dID", "dFirstName", booking.dID);
            ViewBag.SiteUserid = new SelectList(db.siteusers.Where(a => a.status == true), "SiteUserid", "username", booking.SiteUserid);
            return View(booking);
        }
        // GET: bookings/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booking booking = db.bookings.Find(id);
            if (booking != null)
            {
                booking.statusbit = false;
                db.Entry(booking).State = EntityState.Modified;
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
