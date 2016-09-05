using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dist23MVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;



namespace Dist23MVC.Controllers
{
    public class MeetingsController : Controller
    {
        private Dist23Data db = new Dist23Data();
        static clsDataGetter dg;

        public ActionResult Meetings_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetMeetings().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<MeetingsList> GetMeetings()
        {
            var idParam = new SqlParameter {
                ParameterName = "DistKey",
                Value = int.Parse(Session["currDist"].ToString())
            };

            IEnumerable<MeetingsList> results = db.Database.SqlQuery<MeetingsList>(
                "sp_GetMeetings @DistKey",idParam).ToList();

            return results;
        }

        // GET: Meetings
        public ActionResult MeetingsIndex()
        {
            IEnumerable<MeetingsList> results = db.Database.SqlQuery<MeetingsList>(
                "sp_GetMeetings  @DistKey", Session["currDist"].ToString());
                //new SqlParameter("@DistKey", Session["currDist"].ToString())
                //).ToList();
            return View(results);
        }

        public ActionResult MeetingsEdit() 
        {
            if (!Dist23MVC.Helpers.LoginHelpers.isLoggedIn())
            {
                return View("../Login/Login");
            }
            List<MeetingViewModel> meetingList = new List<MeetingViewModel>();

            var mtgList = db.Meetings.Where(x => x.DistKey == GlobalVariables.DistKey).OrderBy(x => x.GroupId).ThenBy(x => x.Day).ToList();
            foreach (Meetings meeting in mtgList)
            {
                MeetingViewModel mvm = new MeetingViewModel();
                mvm.pKey = meeting.pKey;
                mvm.meeting = db.Meetings.Where(x => x.pKey == meeting.pKey).FirstOrDefault();
                if(meeting.LocationID != 0)
                    mvm.Location = db.Locations.FirstOrDefault(x => x.pKey == meeting.LocationID).Location;
                mvm.GroupName = db.Groups.FirstOrDefault(x => x.pKey == meeting.GroupId).GroupName;
                meetingList.Add(mvm);
            }
            return View(meetingList);
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetings meetings = db.Meetings.Find(id);
            if (meetings == null)
            {
                return HttpNotFound();
            }
            return View(meetings);
        }

        // GET: Meetings/Create
        public ActionResult MeetingCreate()
        {
            BuildGroupLists();
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MeetingCreate(Meetings meetings)
        {
            if (ModelState.IsValid)
            {
                meetings.DistKey = GlobalVariables.DistKey;
                db.Meetings.Add(meetings);
                db.SaveChanges();
                return RedirectToAction("MeetingsEdit");
            }

            return View(meetings);
        }

        // GET: Meetings/Edit/5
        public ActionResult MeetingEdit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetings meetings = db.Meetings.Find(id);
            if (meetings == null)
            {
                return HttpNotFound();
            }
            BuildGroupLists();
            return View(meetings);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MeetingEdit(Meetings meetings)
        {
            if (ModelState.IsValid)
            {
                meetings.DistKey = GlobalVariables.DistKey;
                db.Entry(meetings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MeetingsEdit");
            }
            return View(meetings);
        }

        // GET: Meetings/Delete/5
        public ActionResult MeetingDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetings meetings = db.Meetings.Find(id);
            if (meetings == null)
            {
                return HttpNotFound();
            }
            db.Meetings.Remove(meetings);
            db.SaveChanges();
            return RedirectToAction("MeetingsEdit");
        }

        protected void DisplayDocumentOrErrorPage(string FilePath)
        {
            if (String.IsNullOrWhiteSpace(FilePath))
            {
                //RedirectToErrorPage();
            }
            else
            {
                try
                {
                    byte[] pdfBytes = System.IO.File.ReadAllBytes(FilePath);
                    if (pdfBytes.GetLength(0) == 0)
                    {
                        throw new Exception();
                    }
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.AddHeader("Content-disposition", "attachment; filename=" +
                                       FilePath);
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(pdfBytes);
                    Response.End();
                }
                catch (Exception ex)
                {
                    //RedirectToErrorPage();
                }
            }
        }
        public void DownloadMeetings()
        {
            Dist23MVC.Helpers.PrintMeetingHelper.MakeMeetingPDF();
        }

        private void BuildGroupLists()
        {
            var GroupList = db.Groups.Where(x => x.DistKey == GlobalVariables.DistKey).Select(x => new SelectListItem
            {
                Value = x.pKey.ToString(),
                Text = x.GroupName,
            });
            ViewData["GroupID"] = GroupList;

            var LocationList = db.Locations.Where(x => x.DistKey == GlobalVariables.DistKey).Select(x => new SelectListItem
            {
                Value = x.pKey.ToString(),
                Text = x.Location,
            }).Distinct();
            ViewData["LocationID"] = LocationList;

            var Day = db.Meetings.Select(x => new SelectListItem
            {
                Value = x.Day,
                Text = x.Day,
            }).Distinct();
            ViewData["Day"] = Day;
        }


        [HttpPost]
        public ActionResult UploadSchedule()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        System.IO.FileInfo fi = new FileInfo(file.FileName);
                        var ext = fi.Extension;
                        var fileName = BuildDocFileName(ext, -1);
                        var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                        file.SaveAs(path);
                        Session["currFile"] = "~/upload/" + fileName;
                        ViewBag.UploadStatus = "File uploaded successfully";
                    }
                }
                catch
                {
                    ViewBag.UploadStatus = "File upload failed. I don't know why. Call Stuart 251 689 1725";
                }
            }
            return View("MeetingsIndex");
        }

        private string BuildDocFileName(string ext, int id = -1)
        {
            return "meetings" + GlobalVariables.DistKey.ToString() + ext;
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
