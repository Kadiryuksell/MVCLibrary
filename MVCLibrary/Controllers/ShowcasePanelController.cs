using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;
using MVCLibrary.Models.Class;

namespace MVCLibrary.Controllers
{
    public class ShowcasePanelController : Controller
    {
        // GET: ShowcasePanel
        LibraryEntities Db = new LibraryEntities();
        [HttpGet]
        public ActionResult ShowcasePanel()
        {
            TableModels tableModels = new TableModels();
            tableModels.bookModel = Db.Books.ToList();
            tableModels.aboutModel = Db.About.ToList();

            return View(tableModels);
        }

        [HttpPost]
        public ActionResult ShowcasePanel(Contact contact)
        {
            Db.Contact.Add(contact);
            Db.SaveChanges();
            return RedirectToAction("ShowcasePanel");
        }
    }
}