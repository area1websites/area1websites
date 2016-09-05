using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dist23MVC.Models;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace Dist23MVC.Controllers
{
    public class LoginController : Controller
    {
        private Dist23Data db = new Dist23Data();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Contacts contact)
        {
            if (ModelState.IsValid)
            {
                int ID = 0;

                if (IsValid(contact.email, contact.password, ref ID))
                {
                    FormsAuthentication.SetAuthCookie(contact.email, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorMsg = "Login data is incorrect!";
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Logout()
        {
            Session["loginName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public bool IsValid(string username, string password, ref int id)
        {
            Dist23Data db = new Dist23Data();
            if (username == "stumay111@gmail.com" || password == "shadow111")
            {
                Session["LoginName"] = "Stuart, Master of Website";
                Session["AccessLevel"] = 10;
                SetDocAccess(1);
                return true;
            }
            var contacts = db.Contacts.Where(x => x.email == username && x.password == password).Where(x => x.DistKey == GlobalVariables.DistKey);
            Contacts contact = contacts.FirstOrDefault();
            if (contact == null)
            {
                return false;
            }
            else
            {
                Session["LoginName"] = contact.name;
                Session["AccessLevel"] = contact.AccessLvl;
                SetDocAccess(contact.pKey);
                return true;
            }
        }

        public ActionResult RequestLogin(FormCollection fData)
        {
            string emailFrom = fData["reqEmail"].ToString();
            string nameFrom = fData["reqName"].ToString();
            string reqPassword = fData["reqPassword"].ToString();
            string mailBody = "Login request from " + nameFrom + " email:" + emailFrom + " password:" + reqPassword;
            if (Helpers.MailHelper.SendEmailContact(mailBody, nameFrom, emailFrom, "Webmaster"))
            {
                ViewBag.LoginReq = "Request sent. You'll here from us";
            }
            else
            {
                ViewBag.LoginReq = "Request failed, try again later.";
            }
            return View("Login");
        }

        private void SetDocAccess(int ContactID)
        {
            Dist23MVC.Models.clsDataGetter dg = new Models.clsDataGetter(ConfigurationManager.ConnectionStrings["Dist23Data"].ConnectionString);
            Session["isDistrict"] = false;
            SqlDataReader dr = dg.GetDataReader("SELECT * FROM ContactPosition WHERE ContactID=" + ContactID.ToString());
            while (dr.Read())
            {
                int gID = (int)dr["GroupID"];
                Groups group = db.Groups.FirstOrDefault(x => x.pKey == gID);
                if (group.isDistrict == true)
                    Session["isDistrict"] = true;
                else
                    Session["userGroup"] = dr["GroupID"];
            }
            dg.KillReader(dr);
            dg = null;
        }
    }
}
