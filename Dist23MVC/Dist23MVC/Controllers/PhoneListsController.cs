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
    public class PhoneListsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: PhoneLists
        public ActionResult PhoneListIndex()
        {
            return View(db.VolunteerList.ToList());
        }

        // GET: PhoneLists/Details/5
        public ActionResult PhoneListDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerList volunteerList = db.VolunteerList.Find(id);
            if (volunteerList == null)
            {
                return HttpNotFound();
            }
            return View(volunteerList);
        }

        // GET: PhoneLists/Create
        public ActionResult PhoneListCreate()
        {
            return View();
        }

        // POST: PhoneLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhoneListCreate([Bind(Include = "pKey,LastName,FirstName,Phone,Email,Gender,SobDate,HomeGroup,Speaker,Jail,TwStep,Ride,PhoneList,Treatment,SpecialEvents")] VolunteerList volunteerList)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerList.Add(volunteerList);
                db.SaveChanges();
                int newKey = volunteerList.pKey;
                Session["isEditing"] = true;
                SendEmail();
                return RedirectToAction("PhoneListThanks");
            }

            return RedirectToAction("PhoneListCreate");
        }

        public ActionResult PhoneListThanks()
        {
            return View();
        }
        // GET: PhoneLists/Edit/5
        public ActionResult PhoneListEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerList volunteerList = db.VolunteerList.Find(id);
            if (volunteerList == null)
            {
                return HttpNotFound();
            }
            return View(volunteerList);
        }

        // POST: PhoneLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhoneListEdit([Bind(Include = "pKey,LastName,FirstName,Phone,Email,Gender,LastServed,SobDate,Speaker,Jail,Daphne,Fairhope,County,TwStep,Ride,PhoneList1,AttendWorkshop,Treatement,SpecialEvents,GoogleVoice,onHold")] VolunteerList volunteerList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerList);
        }

        // GET: PhoneLists/Delete/5
        public ActionResult PhoneListDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerList volunteerList = db.VolunteerList.Find(id);
            if (volunteerList == null)
            {
                return HttpNotFound();
            }
            return View(volunteerList);
        }

        // POST: PhoneLists/Delete/5
        [HttpPost, ActionName("PhoneListDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolunteerList volunteerList = db.VolunteerList.Find(id);
            db.VolunteerList.Remove(volunteerList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void SendEmail()
        {

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
