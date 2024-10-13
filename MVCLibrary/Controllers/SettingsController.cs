using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    
    public class SettingsController : Controller
    {
        // GET: Settings
        LibraryEntities db = new LibraryEntities();
        public ActionResult SettingPage()
        {
            var admins = db.Admin.ToList();
            return View(admins);
        }

        public ActionResult AdminAdd(Admin admin)
        {
            db.Admin.Add(admin);
            db.SaveChanges();
            return RedirectToAction("SettingPage");
        }

        public ActionResult AdminDelete(int id) 
        {
            var adminID = db.Admin.Find(id);
            db.Admin.Remove(adminID);
            db.SaveChanges();
            return RedirectToAction("SettingPage");
        }

        public ActionResult AdminBring(int id)
        {
            var adminID = db.Admin.Find(id);
            return View("AdminBring", adminID);
        }
        [HttpPost]
        public ActionResult AdminUpdate(Admin admin)
        {
            var adminId = db.Admin.Find(admin.Id);
            adminId.UserName = admin.UserName;
            adminId.Password = admin.Password;
            adminId.Authorization = admin.Authorization;
            db.SaveChanges();
            return RedirectToAction("SettingPage");
        }



    }
}