using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dist23MVC.Models;
using Dist23MVC.Helpers;

namespace Dist23MVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(FormCollection fData)
        {
            string body = fData["msgBody"].ToString();
            string nameFrom = fData["userName"].ToString();
            string emailFrom = fData["userEmail"].ToString();
            string destination = fData["contactWho"].ToString();
            if (!MailHelper.SendEmailContact(body, nameFrom, emailFrom, destination))
            {
                ViewBag.Status = "Sorry, we couldn't send your note. Call the hotline at (251)301-6773 if you need help now!";
            }
            else
            {
                ViewBag.Status = "Mail successfully sent.";
            }
            return View("Contact");
        }



    }
}

