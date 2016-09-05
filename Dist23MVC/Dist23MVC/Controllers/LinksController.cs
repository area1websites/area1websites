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
    public class LinksController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Links
        public ActionResult LinksIndex()
        {
            return View(db.Links.Where(x => x.DistKey == GlobalVariables.DistKey).OrderBy(x => x.ListOrder).ToList());
        }

        // GET: Links/Details/5
        public ActionResult LinksDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        // GET: Links/Create
        public ActionResult LinksCreate()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinksCreate([Bind(Include = "pKey,LinkText,linkURL,ListOrder,LinkComment,target")] Links links)
        {
            if (ModelState.IsValid)
            {
                links.DistKey = GlobalVariables.DistKey;
                db.Links.Add(links);
                db.SaveChanges();
                return RedirectToAction("LinksIndex");
            }

            return View(links);
        }

        // GET: Links/Edit/5
        public ActionResult LinksEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinksEdit([Bind(Include = "pKey,LinkText,linkURL,ListOrder,LinkComment,target")] Links links)
        {
            if (ModelState.IsValid)
            {
                links.DistKey = GlobalVariables.DistKey;
                db.Entry(links).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LinksIndex");
            }
            return View(links);
        }

        // GET: Links/Delete/5
        public ActionResult LinksDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("LinksDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Links links = db.Links.Find(id);
            db.Links.Remove(links);
            db.SaveChanges();
            return RedirectToAction("LinksIndex");
        }

        public ActionResult Calendar()
        {
            return View("Calendar");
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
