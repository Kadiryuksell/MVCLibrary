using System;
using System.Collections.Generic;
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
    }
}