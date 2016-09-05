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
    public class DocumentsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: Documents
        public ActionResult DocIndex()
        {
            int DistGroupID = db.Groups.Where(x => x.DistKey == GlobalVariables.DistKey).FirstOrDefault(x => x.isDistrict == true).pKey;
            int GroupID = (int)Session["userGroup"];
            IEnumerable<Dist23MVC.Models.Documents> docs;
            if ((bool)Session["isDistrict"] == true)
            {
                docs = db.Documents.Where(x => x.GroupKey == DistGroupID || x.GroupKey == GroupID).ToList();
            }
            else
            {
                docs = db.Documents.Where(x => x.GroupKey == GroupID).ToList();
            }

            return View(docs);
        }


        // GET: Documents/Create
        public ActionResult DocCreate()
        {
            var GroupList = db.Groups.Where(x => x.DistKey == GlobalVariables.DistKey).Select(x => new SelectListItem
            {
                Value = x.pKey.ToString(),
                Text = x.GroupName,
            });
            ViewData["GroupID"] = GroupList;
            Documents doc = new Documents();
            doc.DistKey = GlobalVariables.DistKey;
            doc.DocDate = DateTime.Now;
            return View(doc);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocCreate(Documents documents)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(documents);
                db.SaveChanges();
                Session["currDocKey"] = documents.pKey;
                return RedirectToAction("DocEdit/" + Session["currDocKey"].ToString());
            }

            return View(documents);
        }

        // GET: Documents/Edit/5
        public ActionResult DocEdit(int? id)
        {
            Session["currDocKey"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            if (Session["currFile"] != null)
            {
                documents.DocLink = Session["currFile"].ToString();
            }
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocEdit(Documents documents)
        {
            if (Session["currFile"] != null)
            {
                documents.DocLink = Session["currFile"].ToString();
            }
            documents.DistKey = GlobalVariables.DistKey;
            if (ModelState.IsValid)
            {
                db.Entry(documents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DocIndex");
            }
            return View(documents);
        }

        // GET: Documents/Delete/5
        public ActionResult DocDelete(int? id)
        {
            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
            db.SaveChanges();
            DeleteFlyer(documents.DocLink);
            return RedirectToAction("DocIndex");
        }

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
                    var fileName = BuildDocFileName(ext, id);
                    var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                    file.SaveAs(path);
                    Session["currFile"] = "../upload/" + fileName;
                }
            }

            return RedirectToAction("DocEdit/" + Session["currDocKey"].ToString());
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
                    var fileName = BuildDocFileName(ext, -1);
                    var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                    file.SaveAs(path);
                    Session["currFile"] = "~/upload/" + fileName;
                }
            }
        }

        private string BuildDocFileName(string ext, int id = -1)
        {
            int nextKey = 0;
            if (id < 0)
            {
                clsDataGetter dg = new clsDataGetter(db.Database.Connection.ConnectionString);
                nextKey = dg.GetScalarInteger("SELECT MAX(pKey) FROM Documents WHERE DistKey=" + GlobalVariables.DistNumber);
                nextKey += 1;
            }
            else
            {
                nextKey = id;
            }
            return "document_" + GlobalVariables.DistNumber + "_" + nextKey.ToString() + ext;
        }

        private void DeleteFlyer(string path)
        {
            if (path == null)
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
