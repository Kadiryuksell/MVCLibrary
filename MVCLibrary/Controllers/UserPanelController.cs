using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        public ActionResult Panel()
        {
            return View();
        }
    }
}