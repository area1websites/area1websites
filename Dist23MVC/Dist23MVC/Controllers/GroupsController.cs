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
    public class GroupsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Groups
        public ActionResult GroupsIndex()
        {
            List<GroupViewModel> gvmList = new List<GroupViewModel>();
            var groupList = db.Groups.Where(x => x.DistKey == GlobalVariables.DistKey).ToList();
            foreach(Groups group in groupList)
            {
                GroupViewModel gvm = new GroupViewModel();
                gvm.group = group;
                gvm.pKey = group.pKey;
                ContactPosition cp = db.ContactPosition.Where(x => x.DistKey == GlobalVariables.DistKey).Where(x => x.GroupID == group.pKey).Where(x => x.PositionID == 2).FirstOrDefault();
                if (cp != null)
                {
                    int GSRID = cp.ContactID;
                    gvm.contact = db.Contacts.Where(x => x.pKey == GSRID).FirstOrDefault();
                }
                gvmList.Add(gvm);
            }
            return View(gvmList);
        }

        // GET: Groups/Details/5
        public ActionResult GroupsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups group = db.Groups.Find(id);
            GroupViewModel gvm = new GroupViewModel();
            gvm.pKey = group.pKey;
            gvm.group = group;
            ContactPosition cp = db.ContactPosition.Where(x => x.DistKey == GlobalVariables.DistKey).Where(x => x.GroupID == group.pKey).Where(x => x.PositionID == 2).FirstOrDefault();
            if (cp != null)
            {
                int GSRID = cp.ContactID;
                gvm.contact = db.Contacts.Where(x => x.pKey == GSRID).FirstOrDefault();
            }
            return View(gvm);
        }

        // GET: Groups/Create
        public ActionResult GroupsCreate()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupsCreate([Bind(Include = "pKey,DistKey,GroupName,GroupNumber,ContactID")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                groups.DistKey = GlobalVariables.DistKey;
                db.Groups.Add(groups);
                db.SaveChanges();
                return RedirectToAction("GroupsIndex");
            }

            return View(groups);
        }

        // GET: Groups/Edit/5
        public ActionResult GroupsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupsEdit([Bind(Include = "pKey,DistKey,GroupName,GroupNumber,ContactID")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GroupsIndex");
            }
            return View(groups);
        }

        // GET: Groups/Delete/5
        public ActionResult GroupsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("GroupsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groups groups = db.Groups.Find(id);
            db.Groups.Remove(groups);
            db.SaveChanges();
            return RedirectToAction("GroupsIndex");
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
