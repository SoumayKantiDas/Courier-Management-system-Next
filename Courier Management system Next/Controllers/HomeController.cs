using Courier_Management_system_Next.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;

using System.EnterpriseServices;
using System.Security.Principal;
using System.Xml.Linq;

namespace Courier_Management_system_Next.Controllers
{
    public class HomeController : Controller
    {
        private CourierManagementsystemNextdbEntities db = new CourierManagementsystemNextdbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = db.siteusers.SingleOrDefault(u => u.username == username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username. Please enter a valid username.");
                return View();
            }

            if (user.password != password)
            {
                ModelState.AddModelError("", "Invalid password. Please enter a valid password.");
                return View();
            }

           
            else
            {

                // If login is successful
                Session["SiteUserid"] = user.SiteUserid;
                Session["Username"] = user.username;

                switch (user.UserTypeId)
                {
                    case 1:
                        Session["UserTypeId"] = 1;
                        return RedirectToAction("Index", "DashBord");
                    case 2:
                        Session["UserTypeId"] = 2;
                        return RedirectToAction("Index", "GeneralUserbookings");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
        }


        public ActionResult Register()
        {
            ViewBag.role_id = new SelectList(db.UserTypes, "UserTypeId", "Usertype", 2); // Set default role_id to 3
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "SiteUserid,username,email,UserTypeId,password,address")] siteuser siteuser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.siteusers.Any(u => u.username == siteuser.username) || db.siteusers.Any(u => u.email == siteuser.email))
                    {
                        ModelState.AddModelError("", "Username or email already exists. Goto Login.");
                        ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype", siteuser.UserTypeId);
                        return View(siteuser);
                    }

                    siteuser.status = true;
                    siteuser.UserTypeId = 2;

                    db.siteusers.Add(siteuser);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            // If we got this far, something failed; re-display form
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "Usertype", siteuser.UserTypeId);
            return View(siteuser);
        }


        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Clear session variables or authentication cookies
            Session.Clear(); // Clear all session variables
            FormsAuthentication.SignOut(); // Sign out from Forms Authentication (if used)

            // Redirect to the login page or any other desired page
            return RedirectToAction("Login", "Home");
        }

    }
}