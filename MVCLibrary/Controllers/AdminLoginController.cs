using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        LibraryEntities db = new LibraryEntities(); 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin) 
        {
            var adminLogin = db.Admin.FirstOrDefault(p => p.UserName == admin.UserName && p.Password == admin.Password);

            if (adminLogin != null) 
            {
                FormsAuthentication.SetAuthCookie(adminLogin.UserName, false);
                Session["adminUserName"] = adminLogin.UserName.ToString();
                return RedirectToAction("Index", "Main");
            }
            return View();
        }
    }
}