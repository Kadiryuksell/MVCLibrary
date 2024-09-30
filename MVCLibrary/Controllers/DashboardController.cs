using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        LibraryEntities db = new LibraryEntities();

        private int userCount()
        {
            return db.Users.Count();
        }

        private decimal? penaltiesCount()
        {
            return db.Penalties.Sum(p => p.Money);
        }

        private int booksCount()
        {
            return db.Books.Count();
        }
        public ActionResult DashboardPage()
        {
            var borrowedBooks = db.Books.Where(p => p.State == false).Count();
         
            ViewBag.Users = userCount();
            ViewBag.Books = booksCount();
            ViewBag.Penalties = penaltiesCount();
            ViewBag.brwdBook = borrowedBooks;
            return View();
        }

        public ActionResult Gallery() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult PictureLoad(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Pictures"),Path.GetFileName(file.FileName));
                file.SaveAs(filePath);
            }

            return RedirectToAction("Gallery");
        }
    }
}