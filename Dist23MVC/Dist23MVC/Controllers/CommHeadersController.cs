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
    public class CommHeadersController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: CommHeaders
        public ActionResult CommIndex()
        {
            return View(db.CommHeaders.Where(x => x.DistKey == GlobalVariables.DistKey).ToList());
        }

        // GET: CommHeaders/Details/5
        public ActionResult CommLinks(int id)
        {
            CommHeaders comm = db.CommHeaders.Where(h => h.pKey == id).FirstOrDefault();
            ViewBag.CommTitle = comm.CommName;
            ViewBag.CommDetail = comm.CommHeader;
            ViewBag.CommKey = comm.pKey;
            List<CommLinks> commLinks = db.CommLinks.Where(c => c.CommKey == id).ToList();
            if (commLinks == null)
            {
                return HttpNotFound();
            }
            return View("CommLinks",commLinks);
        }

        // GET: CommHeaders/Create
        public ActionResult CommCreate()
        {
            SelectList PositionList = new SelectList(db.Positions.Where(x => x.isCommittee == true).ToList(), "pKey", "CommitteeTitle", db.Positions);
            ViewData["PositionList"] = PositionList;
            return View();
        }

        // POST: CommHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommCreate(CommHeaders commHeaders)
        {
            commHeaders.DistKey = GlobalVariables.DistKey;
            string positionTitle = db.Positions.FirstOrDefault(x => x.pKey == commHeaders.PositionKey).CommitteeTitle;
            commHeaders.CommName = positionTitle;
            db.CommHeaders.Add(commHeaders);
            db.SaveChanges();
            return RedirectToAction("CommIndex");
        }

        // GET: CommHeaders/Edit/5
        public ActionResult CommEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHeaders commHeaders = db.CommHeaders.Find(id);
            if (commHeaders == null)
            {
                return HttpNotFound();
            }
            SelectList PositionList = new SelectList(db.Positions.ToList(), "pKey", "CommitteeTitle", db.Positions);
            ViewData["PositionList"] = PositionList;
            return View(commHeaders);
        }

        // POST: CommHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommEdit(CommHeaders commHeaders)
        {
            if (ModelState.IsValid)
            {
                commHeaders.DistKey = GlobalVariables.DistKey;
                //string positionTitle = db.Positions.FirstOrDefault(x => x.pKey == commHeaders.PositionKey).CommitteeTitle;
                //commHeaders.CommName = positionTitle;
                db.Entry(commHeaders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CommIndex");
            }
            return View(commHeaders);
        }

        // GET: CommHeaders/Delete/5
        public ActionResult CommDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHeaders commHeaders = db.CommHeaders.Find(id);
            db.CommHeaders.Remove(commHeaders);
            db.SaveChanges();
            return RedirectToAction("CommIndex",new { id = commHeaders.pKey });
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
