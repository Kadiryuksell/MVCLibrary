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
        
        public ActionResult DashboardPage()
        {
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