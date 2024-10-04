using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;
using System.Web.Security;

namespace MVCLibrary.Controllers
{
    public class UserLoginOperationController : Controller
    {
        // GET: UserLoginOperation
        LibraryEntities db = new LibraryEntities();
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            var userInfo = db.Users.FirstOrDefault(p => p.Mail == user.Mail && p.Password == user.Password);
            
            if(userInfo == null)
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(userInfo.Mail, false);
            Session["email"] = userInfo.Mail.ToString();
            Session["photo"] = userInfo.Photo;
            Session["fullName"] = userInfo.Name + " " + userInfo.LastName;

            return RedirectToAction("Panel", "UserPanel");
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