using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dist23MVC.Models;

namespace Dist23MVC.Controllers
{
    public class CommLinksController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: CommLinks/Create
        public ActionResult CommLinkCreate(int commKey)
        {
            CommLinks newCommLink = new CommLinks();
            newCommLink.CommKey = commKey;
            newCommLink.DistKey = GlobalVariables.DistKey;
            return View(newCommLink);
        }

        // POST: CommLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommLinkCreate(CommLinks commLinks)
        {
            commLinks.DistKey = GlobalVariables.DistKey;
            if (ModelState.IsValid)
            {
                db.CommLinks.Add(commLinks);
                db.SaveChanges();
                return RedirectToAction("CommLinks", "CommHeaders", new { id = commLinks.CommKey });
            }

            return View(commLinks);
        }

        // GET: CommLinks/Edit/5
        public ActionResult CommLinkEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommLinks commLinks = db.CommLinks.Find(id);
            if (commLinks == null)
            {
                return HttpNotFound();
            }
            return View("CommLinkEdit", commLinks);
        }

        // POST: CommLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommLinkEdit(CommLinks commLinks)
        {
            if (ModelState.IsValid)
            {
                commLinks.DistKey = GlobalVariables.DistKey;
                db.Entry(commLinks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CommLinks","CommHeaders",new { id = commLinks.CommKey });
            }
            return View("CommLinkEdit",commLinks);
        }

        // GET: CommLinks/Delete/5
        public ActionResult CommLinkDelete(int? id)
        {
            CommLinks commLinks = db.CommLinks.Find((int)id);
            db.CommLinks.Remove(commLinks);
            db.SaveChanges();
            return RedirectToAction("CommLinks", "CommHeaders", new { id = commLinks.CommKey });
        }

        public ActionResult Volunteer(FormCollection fData)
        {
            string emailFrom = fData["reqEmail"].ToString();
            string nameFrom = fData["reqName"].ToString();
            string reqPhone = fData["reqPhone"].ToString();
            string commTitle = fData[5].ToString();
            string commKey = fData[4].ToString();
            
            string mailBody = "Volunteer for " + commTitle + " from " + nameFrom + " email:" + emailFrom + " phone:" + reqPhone;
            if (Helpers.MailHelper.SendEmailVolunteer(mailBody, nameFrom, emailFrom, commTitle,"Volunteer"))
            {
                Session["LoginReq"] = "Thanks you for your service, you'll hear from us";
                AddVolunteer(emailFrom, nameFrom, reqPhone, commKey);
            }
            else
            {
                Session["LoginReq"] = "Request failed, try again later.";
            }
            return RedirectToAction("CommLinks", "CommHeaders",new { id = Int32.Parse(commKey) });
        }

        private void AddVolunteer(string name, string email,string phone,string commKey)
        {
            VolunteerList vol = new VolunteerList();
            vol.DistKey = (int)Session["DistKey"];
            vol.CommKey = Int32.Parse(commKey);
            vol.Name = name;
            vol.Email = email;
            vol.Phone = phone;
            vol.VolDate = DateTime.Now;
            db.VolunteerList.Add(vol);
            db.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
