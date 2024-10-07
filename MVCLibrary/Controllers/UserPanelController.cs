using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        LibraryEntities db = new LibraryEntities();

        
        public ActionResult Panel()
        {
            var userMail = (string)Session["email"];
            var user = db.Users.FirstOrDefault( p=> p.Mail == userMail );
            return View(user);
        }

        [HttpPost]
        public ActionResult PanelUpdate(Users userUpdate)
        {
            var userMail = (string)Session["email"];
            var user = db.Users.FirstOrDefault(p => p.Mail == userMail);

            if (!ModelState.IsValid)
            {
                return View();
            }

            user.Password = userUpdate.Password;
            user.Name = userUpdate.Name;
            user.LastName = userUpdate.LastName;
            user.PhoneNumber = userUpdate.PhoneNumber;
            user.UserName = userUpdate.UserName;
            user.Photo = userUpdate.Photo;
            db.SaveChanges();
            return RedirectToAction("Panel");
        }

        public ActionResult MyBooks()
        {
            var userMail = (string)Session["email"];
            var userId = db.Users.Where(p=>p.Mail == userMail.ToString()).Select(x =>x.Id).FirstOrDefault();
            var values = db.LibraryOperations.Where(p =>p.UserId == userId).ToList();

            return View(values);
        }
        
        public ActionResult AnnouncementList()
        {
            var announcements = db.Announcement.ToList();
            return View(announcements);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "UserLoginOperation");
        }
    }
}