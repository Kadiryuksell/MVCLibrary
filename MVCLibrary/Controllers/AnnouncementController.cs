using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;
namespace MVCLibrary.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        LibraryEntities db = new LibraryEntities();
        public ActionResult AnnouncementList()
        {
            var announcements = db.Announcement.ToList();
            return View(announcements);
        }

        [HttpGet]
        public ActionResult AnnouncementAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AnnouncementAdd(Announcement announcement)
        {
            db.Announcement.Add(announcement);
            db.SaveChanges();
            return RedirectToAction("AnnouncementList");
        }

        public ActionResult AnnouncementDelete(int id)
        {
            var announcementId = db.Announcement.Find(id);
            db.Announcement.Remove(announcementId);
            db.SaveChanges();
            return RedirectToAction("AnnouncementList");
        }

      
        public ActionResult AnnouncementBring(int id)
        {
            var announcementId = db.Announcement.Find(id);
            return View("AnnouncementBring", announcementId);
        }
      
        public ActionResult AnnouncementUpdate(Announcement announcement)
        {
            var announcementId = db.Announcement.Find(announcement.Id);
            announcementId.Content = announcement.Content;
            announcementId.Category = announcement.Category;
            announcementId.Date = announcement.Date;
            db.SaveChanges();
            return RedirectToAction("AnnouncementList");
        }

    }
}