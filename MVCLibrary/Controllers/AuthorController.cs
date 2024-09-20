using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        LibraryEntities db = new LibraryEntities();
        public ActionResult AuthorList()
        {
            var authors = db.Authors.ToList();
            return View(authors);
        }
        [HttpGet]
        public ActionResult AuthorAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthorAdd(Authors author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();

                return RedirectToAction("AuthorAdd");
            }
            
            return View();
        }

        public ActionResult AuthorDelete(int id)
        {
            if(id != 0)
            {
                var authorId = db.Authors.Find(id);
                db.Authors.Remove(authorId);
                db.SaveChanges();
                return RedirectToAction("AuthorList");
            }

            return RedirectToAction("AuthorList");
        }

        public ActionResult AuthorBring(int id) 
        {
            var authorId = db.Authors.Find(id);
            return View("AuthorBring", authorId);
        }

        public ActionResult AuthorUpdate(Authors author)
        {
            var authorId = db.Authors.Find(author.Id);
            authorId.Name = author.Name;
            authorId.LastName = author.LastName;
            authorId.Detail = author.Detail;

            db.SaveChanges();
            return RedirectToAction("AuthorList");
        }
    }
}