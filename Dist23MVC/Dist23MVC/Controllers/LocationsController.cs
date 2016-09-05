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
    public class LocationsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.Where(x => x.DistKey == GlobalVariables.DistKey).ToList());
        }

        // GET: Locations/Details/5
        public ActionResult ShowLocation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations location = db.Locations.FirstOrDefault(x => x.pKey == id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View("MapLocation",location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pKey,Location,MapUIRL,EmbedURL,DistKey,Address")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                locations.DistKey = GlobalVariables.DistKey;
                db.Locations.Add(locations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locations);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations locations = db.Locations.Find(id);
            locations.DistKey = GlobalVariables.DistKey;

            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pKey,Location,MapUIRL,EmbedURL,DistKey,Address")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                locations.DistKey = GlobalVariables.DistKey;
                db.Entry(locations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locations);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            Locations locations = db.Locations.Find(id);
            db.Locations.Remove(locations);
            db.SaveChanges();
            return RedirectToAction("Index");
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
