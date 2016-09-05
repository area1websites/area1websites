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
    public class ContactsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Contacts
        public ActionResult ContactsIndex()
        {
            return View(db.Contacts.Where(x => x.DistKey == GlobalVariables.DistKey).ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult ContactsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactsViewModel contactsView = new ContactsViewModel(id);
            if (contactsView == null)
            {
                return HttpNotFound();
            }
            return View(contactsView);
        }

        // GET: Contacts/Create
        public ActionResult ContactsCreate()
        {
            if (!Helpers.LoginHelpers.isLoggedIn())
                return RedirectToAction("Login", "Login");
            //List<ContactPositionViewModel> cpvm = new List<ContactPositionViewModel>();
            //ViewData["ContactPositionViewModel"] = cpvm;
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactsCreate([Bind(Include = "pKey,DistKey,name,email,phone,password,AccessLvl,Address")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                contacts.DistKey = GlobalVariables.DistKey;
                db.Contacts.Add(contacts);
                db.SaveChanges();
                return RedirectToAction("ContactsIndex");
            }

            return View(contacts);
        }

        public ActionResult PositionCreate(int ID)
        {
            ContactPosition newPosition = new ContactPosition();
            newPosition.ContactID = ID;
            newPosition.DistKey = GlobalVariables.DistKey;
            SelectList PositionList = new SelectList(db.Positions.ToList(), "pKey", "PositionName", db.Positions);
            ViewData["PositionList"] = PositionList;
            SelectList GroupList = new SelectList(db.Groups.Where(x => x.DistKey == GlobalVariables.DistKey).ToList(), "pKey", "GroupName", db.Groups);
            ViewData["GroupList"] = GroupList;
            return View("PositionCreate", newPosition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PositionCreate(ContactPosition contactPosition)
        {
            if (ModelState.IsValid)
            {
                contactPosition.DistKey = GlobalVariables.DistKey;
                db.ContactPosition.Add(contactPosition);
                db.SaveChanges();
                return RedirectToAction("ContactsEdit",new { id = contactPosition.ContactID });
            }

            return View(contactPosition);
        }

        // GET: Contacts/Edit/5
        public ActionResult ContactsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactsViewModel cvm = new ContactsViewModel(id);
            return View(cvm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactsEdit(ContactsViewModel cvm)
        {
            Contacts contacts = cvm.contact;
            contacts.DistKey = GlobalVariables.DistKey;

            if (ModelState.IsValid)
            {
                db.Entry(contacts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ContactsIndex");
            }
            return View(contacts);
        }

        // GET: Contacts/Delete/5
        public ActionResult ContactsDelete(int? id)
        {
            Contacts contacts = db.Contacts.Find(id);
            db.Contacts.Remove(contacts);
            db.SaveChanges();
            return RedirectToAction("ContactsIndex");
        }

        public ActionResult ContactPositionDelete(int? id)
        {
            ContactPosition cp = db.ContactPosition.Find(id);
            db.ContactPosition.Remove(cp);
            db.SaveChanges();
            return RedirectToAction("ContactsDetails",new { id = cp.ContactID });
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
