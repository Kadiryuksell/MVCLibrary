using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;
using PagedList;


namespace MVCLibrary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        LibraryEntities db = new LibraryEntities();
        public ActionResult UserList(int pageCount=1)
        {
            var users = db.Users.ToList().ToPagedList(pageCount,10);
            return View(users);
        }

        [HttpGet]
        public ActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserAdd(Users user) 
        {
            if (!ModelState.IsValid)
            {
                return View("UserAdd");
            }

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserDelete(int id) 
        {
            var userId = db.Users.Find(id);
            db.Users.Remove(userId);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserBring(int id)
        {
            var userId = db.Users.Find(id);
            return View("UserBring", userId);
        }

        public ActionResult UserUpdate(Users user)
        {
            var userId = db.Users.Find(user.Id);
            userId.School = user.School;
            userId.UserName = user.UserName;
            userId.Password = user.Password;
            userId.Name = user.Name;
            userId.LastName = user.LastName;
            userId.Mail = user.Mail;
            userId.PhoneNumber = user.PhoneNumber;

            db.SaveChanges();
            return RedirectToAction("UserList");
        }

    }
}