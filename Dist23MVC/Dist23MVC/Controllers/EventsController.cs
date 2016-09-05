using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dist23MVC.Models;
using System.IO;

namespace Dist23MVC.Controllers
{
    public class EventsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Events
        public ActionResult EventsIndex()
        {
            List<EventViewModel> evmList = new List<EventViewModel>();
            using (Dist23Data db = new Dist23Data())
            {
                var OtherCatList = db.OtherDistEvents.Where(x => x.DistKey == GlobalVariables.DistKey).ToList();
                foreach (var OtherDKey in OtherCatList)
                {
                    int dKey = OtherDKey.ShowDistKey;
                    var EventCatList = db.EventCat.Where(x => x.DistKey == dKey).ToList();
                    foreach (var ecat in EventCatList)
                    {
                        EventViewModel evm = new EventViewModel();
                        evm.EventCatName = ecat.EventCatName;
                        var EventsList = db.Events.Where(x => x.EventCat == ecat.pKey);
                        foreach (var eve in EventsList)
                        {
                            evm.Events.Add(eve);
                        }
                        if (db.Events.Where(x => x.EventCat == ecat.pKey).Any())
                            evmList.Add(evm);
                    }
                }


            }
            return View(evmList);
        }

        // GET: Events/Create
        public ActionResult EventsCreate()
        {
            if (!Helpers.LoginHelpers.isLoggedIn())
                return RedirectToAction("Login", "Login");
            //SelectList EventCatList = new SelectList(db.EventCat.Where(x => x.DistKey == GlobalVariables.DistKey).ToList(), "pKey", "EventCatName", db.EventCat);
            //ViewData["EventCatList"] = EventCatList;
            BuildEventCatList();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventsCreate(Events events)
        {
            if (Session["currFile"] != null)
            {
                events.Eventlink = Session["currFile"].ToString();
            }
            events.DistKey = GlobalVariables.DistKey;
            db.Events.Add(events);
            db.SaveChanges();
            Session["currEventKey"] = events.pKey;
            return RedirectToAction("EventsEdit/" + Session["currEventKey"].ToString());
        }
        // GET: Events/Edit/5
        public ActionResult EventsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["currEventKey"] = id;
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            if (Session["currFile"] != null)
            {
                events.Eventlink = Session["currFile"].ToString();
            }
            BuildEventCatList();
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventsEdit(Events events)
        {
            if (Session["currFile"] != null)
            {
                events.Eventlink = Session["currFile"].ToString();
            }
            events.DistKey = GlobalVariables.DistKey;
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EventsIndex");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult EventsDelete(int? id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
            db.SaveChanges();
            try
            {
                DeleteFlyer(events.Eventlink);
            }
            catch
            {

            }
            return RedirectToAction("EventsIndex");
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult UploadEdit(int id)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    System.IO.FileInfo fi = new FileInfo(file.FileName);
                    var ext = fi.Extension;
                    var fileName = BuildEventFileName(ext,id);
                    var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                    file.SaveAs(path);
                    Session["currFile"] = "../upload/" + fileName;
                }
            }

            return RedirectToAction("EventsEdit/" + Session["currEventKey"].ToString());
        }

        [HttpPost]
        public void UploadCreate()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    System.IO.FileInfo fi = new FileInfo(file.FileName);
                    var ext = fi.Extension;
                    var fileName = BuildEventFileName(ext, -1);
                    var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                    file.SaveAs(path);
                    Session["currFile"] = "~/upload/" + fileName;
                }
            }
        }

        private void BuildEventCatList()
        {
            var catList = db.EventCat.Where(x => x.DistKey == GlobalVariables.DistKey || x.DistKey == 999).ToList();
            //.Select(x => new SelectListItem
            //{
            //    Value = x.pKey.ToString(),
            //    Text = x.EventCatName,
            //});
            ViewData["EventCatList"] = new SelectList(catList,"pKey","EventCatName");
        }

        private string BuildEventFileName(string ext, int id = -1)
        {
            int nextKey = 0;
            if (id < 0)
            {
                clsDataGetter dg = new clsDataGetter(db.Database.Connection.ConnectionString);
                nextKey = dg.GetScalarInteger("SELECT MAX(pKey) FROM Events WHERE DistKey=" + GlobalVariables.DistNumber);
                nextKey += 1;
            }
            else
            {
                nextKey = id;
            }
            return "event_" + GlobalVariables.DistNumber + "_" + nextKey.ToString() + ext;
        }

        private void DeleteFlyer(string path)
        {
            if (path.Length == 0)
                return;
            if (path.Contains("http"))
                return;
            path = path.Replace("..", "~");
            string filePath = Server.MapPath(path);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch
                {

                }
            }

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
