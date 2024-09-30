using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class UserLoginOperationController : Controller
    {
        // GET: UserLogin
        LibraryEntities db = new LibraryEntities();
        public ActionResult Login()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user) 
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}