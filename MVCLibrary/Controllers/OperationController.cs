using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class OperationController : Controller
    {
        // GET: Operation
        LibraryEntities db = new LibraryEntities();
        public ActionResult OperationList()
        {
            var operations = db.LibraryOperations.Where(p => p.OperationState == true).ToList();
            return View(operations);
        }
    }
}