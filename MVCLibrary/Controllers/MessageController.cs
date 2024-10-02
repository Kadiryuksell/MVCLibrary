using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        LibraryEntities db = new LibraryEntities();
        
        private List<SelectListItem> UserMailValues()
        {
            List<SelectListItem> userMailValues = db.Users
                .Select(user => new SelectListItem
                {
                    Text = user.Mail,
                    Value = user.Mail
                }).ToList();

            return userMailValues;

        }
        public ActionResult MessagePage()
        {
            var userMail = (string)Session["email"].ToString();
            var messages = db.Message.Where(x=>x.Recipient == userMail.ToString()).ToList();
            return View(messages);
        }

        public ActionResult SentMessages()
        {
            var userMail = (string)Session["email"].ToString();
            var messages = db.Message.Where(p => p.Sender == userMail.ToString()).ToList();
            return View(messages);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            ViewBag.userMail = UserMailValues();
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            var userMail = (string)Session["email"].ToString();

            if (string.IsNullOrEmpty(userMail))
            {
                return RedirectToAction("MessagePage");
            }
            message.Sender = userMail;
            message.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Message.Add(message);
            db.SaveChanges();
            return RedirectToAction("MessagePage");
        }


    }
}