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
    public class KeyListsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: KeyLists
        public ActionResult KeyListIndex()
        {
            return View(db.KeyList.Where(x => x.DistKey == GlobalVariables.DistKey).ToList());
        }

        // GET: KeyLists/Details/5
        public ActionResult KeyListDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyList keyList = db.KeyList.Find(id);
            if (keyList == null)
            {
                return HttpNotFound();
            }
            return View(keyList);
        }

        // GET: KeyLists/Create
        public ActionResult KeyListCreate()
        {
            return View();
        }

        // POST: KeyLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KeyListCreate(KeyList keyList)
        {
            if (ModelState.IsValid)
            {
                keyList.DistKey = GlobalVariables.DistKey;
                db.KeyList.Add(keyList);
                db.SaveChanges();
                return RedirectToAction("KeyListIndex");
            }

            return View(keyList);
        }

        // GET: KeyLists/Edit/5
        public ActionResult KeyListEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyList keyList = db.KeyList.Find(id);
            if (keyList == null)
            {
                return HttpNotFound();
            }
            return View(keyList);
        }

        // POST: KeyLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KeyListEdit(KeyList keyList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyList);
        }

        // GET: KeyLists/Delete/5
        public ActionResult Delete(int? id)
        {
            KeyList keyList = db.KeyList.Find(id);
            db.KeyList.Remove(keyList);
            db.SaveChanges();
            return RedirectToAction("KeyListIndex");
        }

        // POST: KeyLists/Delete/5
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
