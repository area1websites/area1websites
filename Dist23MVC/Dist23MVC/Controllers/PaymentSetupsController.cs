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
    public class PaymentSetupsController : Controller
    {
        private Dist23Data db = new Dist23Data();

        // GET: PaymentSetups
        public ActionResult PaymentSetupsIndex(int id)
        {
            return View(db.PaymentSetup.Where(x => x.EventKey == id).ToList());
        }

        // GET: PaymentSetups/Details/5
        public ActionResult PaymentSetupsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSetup paymentSetup = db.PaymentSetup.Find(id);
            if (paymentSetup == null)
            {
                return HttpNotFound();
            }
            return View(paymentSetup);
        }

        // GET: PaymentSetups/Create
        public ActionResult PaymentSetupsCreate(int id)
        {
            PaymentSetup ps = new PaymentSetup();
            ps.EventKey = id;
            return View(ps);
        }

        // POST: PaymentSetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentSetupsCreate(PaymentSetup paymentSetup)
        {
            if (ModelState.IsValid)
            {
                db.PaymentSetup.Add(paymentSetup);
                db.SaveChanges();
                Session["currSetupKey"] = paymentSetup.pKey;
                return RedirectToAction("PaymentSetupsIndex",new { id = paymentSetup.EventKey });
            }

            return View(paymentSetup);
        }

        // GET: PaymentSetups/Edit/5
        public ActionResult PaymentSetupsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSetupViewModel psvm = new PaymentSetupViewModel(id);
            Session["currSetupKey"] = id;


            return View(psvm);
        }

        // POST: PaymentSetups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentSetupsEdit(PaymentSetupViewModel psvm)
        {
            PaymentSetup paymentSetup = psvm.paymentSetup;

            if (ModelState.IsValid)
            {
                db.Entry(paymentSetup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PaymentSetupsIndex");
            }
            return View(psvm);
        }

        // GET: PaymentSetups/Delete/5
        public ActionResult PaymentSetupsDelete(int? id)
        {
            PaymentSetup paymentSetup = db.PaymentSetup.Find(id);
            db.PaymentSetup.Remove(paymentSetup);
            db.SaveChanges();
            return RedirectToAction("PaymentSetupsIndex");
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
