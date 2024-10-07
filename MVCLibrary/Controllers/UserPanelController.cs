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
            var announcements = db.Announcement.ToList();
            ViewBag.userFullName = user.Name +" " +user.LastName;
            ViewBag.userPhoto = user.Photo;
            ViewBag.userName = user.UserName;
            ViewBag.userPhone = user.PhoneNumber;
            ViewBag.School = user.School;
            ViewBag.totalUserBooks = db.LibraryOperations.Where(p => p.UserId == user.Id).Count();
            ViewBag.messages = db.Message.Where(p => p.Recipient == userMail).Count();
            ViewBag.announcements = db.Announcement.Count();

            return View(announcements);
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

        public PartialViewResult MessagePartial()
        {
            return PartialView();
        }

        public PartialViewResult UserProfilPartial()
        {
            var userEmail = (string)Session["email"];
            var userID = db.Users.Where(p => p.Mail == userEmail).Select(p => p.Id).FirstOrDefault();
            var userBring = db.Users.Find(userID);
            return PartialView("UserProfilPartial",userBring);
        }

    }
}