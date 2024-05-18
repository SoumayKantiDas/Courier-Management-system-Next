using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courier_Management_system_Next.Controllers.AdminController
{
    public class DashBordController : Controller
    {
        // GET: DashBord
        public ActionResult Index()
        {// Retrieve user ID from session
            int? SiteUserid = Session["SiteUserid"] as int?;
            // Retrieve role ID from session
            int? userTypeId = Session["UserTypeId"] as int?;

            // Perform authorization logic using the session's UserId and RoleId
            if (SiteUserid == null || userTypeId != 1)
            {
                // Authorization failed, redirect to Home/Index
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: DashBord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashBord/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBord/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBord/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBord/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBord/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBord/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
