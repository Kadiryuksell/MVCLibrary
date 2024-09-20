using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        LibraryEntities db = new LibraryEntities();
        public ActionResult CategoryList()
        {
            var values = db.Categories.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Categories categories)
        {
          db.Categories.Add(categories);
          db.SaveChanges();
          return RedirectToAction("CategoryList");
        }

        public ActionResult CategoryDelete(int id) 
        {
            var categoryId = db.Categories.FirstOrDefault(c => c.Id == id);
            db.Categories.Remove(categoryId);   
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        public ActionResult CategoryBring(int id)
        {
            var categoryId = db.Categories.Find(id);
            return View("CategoryBring", categoryId);
        }
        public ActionResult CategoryUpdate(Categories category) 
        {
            var categoryId = db.Categories.Find(category.Id);
            categoryId.Name = category.Name;
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}