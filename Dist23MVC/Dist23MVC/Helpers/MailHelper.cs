using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Dist23MVC.Models;

namespace Dist23MVC.Helpers
{
    public static class MailHelper
    {
        const String HOST = "email-smtp.us-west-2.amazonaws.com";
        static string fromAddress = "noreply@" + GlobalVariables.DomainName;

        // Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
        // STARTTLS to encrypt the connection.
        const int PORT = 587;

        const String SMTP_USERNAME = "AKIAJNZBFYLLZHC3NPIQ";  // Replace with your SMTP username. 
        const String SMTP_PASSWORD = "AqZ2UqLaCgyLYWM+bLngTJlrluc8NH3J5+mwShqwYTKP";  // Replace with your SMTP password.

        private static SmtpClient client = new SmtpClient(HOST, PORT);
        public static bool SendEmailContact(string textBody, string nameFrom, string emailFrom, string destination,string Type = "")
        {
            client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            client.EnableSsl = true;
            string body = nameFrom + " at " + emailFrom + " wrote <br/>";
            body += textBody;
            string emailTo = "";
                emailTo = GetContactDestinationEmail(destination);
            MailMessage mail = new MailMessage(fromAddress, emailTo);
            mail.Subject = "Email from website from " + nameFrom;
            mail.IsBodyHtml = true;
            mail.Body = body;
            try
            {
                client.Send(mail);
                SendConfirm(emailFrom);
                return true;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return false;
            }
        }
        public static bool SendEmailVolunteer(string textBody, string nameFrom, string emailFrom, string destination, string Type = "")
        {
            client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            client.EnableSsl = true;
            string body = nameFrom + " at " + emailFrom + " wrote <br/>";
            body += textBody;
            string emailTo = "";
            emailTo = GetVolunteerDestinationEmail(destination);
            MailMessage mail = new MailMessage(fromAddress, emailTo);
            mail.Subject = "Email from website from " + nameFrom;
            mail.IsBodyHtml = true;
            mail.Body = body;
            try
            {
                client.Send(mail);
                SendConfirm(emailFrom);
                return true;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return false;
            }
        }


        private static string GetContactDestinationEmail(string emailTo)
        {
            string lookupString = "";
            switch (emailTo)
            {
                case "I need help":
                    lookupString = LookupEmail("Webmaster", true);
                    break;
                case "Webmaster":
                    lookupString = LookupEmail("Webmaster", true);
                    break;
                case "DCM (District Committee Member, true)":
                    lookupString = LookupEmail("DCM", true);
                    break;
                case "Public Information":
                    lookupString = LookupEmail("Public Information", true);
                    break;
                case "Treatment Chair":
                    lookupString = LookupEmail("Treatment", true);
                    break;
                case "Corrections Chair":
                case "Corrections Committee":
                    lookupString = LookupEmail("Corrections", true);
                    break;
                case "I am a professional needing information":
                case "Cooperation With The Professional Community":
                    lookupString = LookupEmail("CPC", true);
                    break;
            }
            if (lookupString == "")
                lookupString = LookupEmail("Webmaster",true);
            return lookupString;
        }

        private static string GetVolunteerDestinationEmail(string emailTo)
        {
            string lookupString = "";
            switch (emailTo)
            {
                case "I need help":
                    lookupString = LookupEmail("Webmaster", true);
                    break;
                case "Webmaster":
                    lookupString = LookupEmail("Webmaster", true);
                    break;
                case "DCM (District Committee Member, true)":
                    lookupString = LookupEmail("DCM", true);
                    break;
                case "Public Information":
                    lookupString = LookupEmail("Public Information", true);
                    break;
                case "Treatment Chair":
                    lookupString = LookupEmail("Treatment", true);
                    break;
                case "Corrections Chair":
                case "Corrections Committee":
                    lookupString = LookupEmail("Corrections", true);
                    break;
                case "I am a professional needing information":
                case "Cooperation With The Professional Community":
                    lookupString = LookupEmail("CPC", true);
                    break;
            }
            if (lookupString == "")
                lookupString = LookupEmail("Webmaster", true);
            return lookupString;
        }

        private static string LookupEmail(string type,bool isDistrict)
        {
            Dist23Data db = new Dist23Data();
            clsDataGetter dg = new clsDataGetter(db.Database.Connection.ConnectionString);
            string sql = "SELECT email FROM contacts c ";
            sql += "INNER JOIN ContactPosition cp ON cp.contactID = c.pKey ";
            sql += "INNER JOIN positions p ON p.pKey = cp.PositionID ";
            sql += "INNER JOIN groups g ON g.pKey = cp.GroupID ";
            sql += "WHERE positionName = '" + type + "' ";
            sql += "AND g.DistKey = " + GlobalVariables.DistKey;
            if (isDistrict)
                sql += " AND g.isDistrict = 1";
            string email = dg.GetScalarString(sql);
            return email;
        }

        private static void SendConfirm(string to)
        {
            MailAddress toAddr = new MailAddress(to);
            MailMessage mail = new MailMessage(new MailAddress(fromAddress), toAddr);
            mail.Subject = "Thanks for contacting AA District " + GlobalVariables.DistNumber;
            mail.Body = BuildBody("confirm");
            mail.IsBodyHtml = true;
            client.Send(mail);
        }

        private static string BuildBody(string type)
        {
            string emailStr = "";
            switch (type)
            {
                case "confirm":
                     emailStr += "<p>Thanks for contacting District " + GlobalVariables.DistNumber + " AA.</p>";
                     emailStr += "<p>Someone will get back to you as soon as possible</p><br/>";
                     emailStr += "<p>Thanks,<br>";
                     emailStr += "District " + GlobalVariables.DistNumber + " AA";
                     return emailStr;
                case "volunteer":
                    emailStr += "<p>Thanks for volunteering for service.</p>";
                    emailStr += "<p>Someone will get back to you as soon as possible</p><br/>";
                    emailStr += "<p>Thanks,<br>";
                    emailStr += "District " + GlobalVariables.DistNumber + " AA";
                    return emailStr;
            }
            return "";
        }
    }
}

//ses-smtp-user.20151206-074104
//SMTP Username:
//AKIAJNZBFYLLZHC3NPIQ
//SMTP Password:
//AqZ2UqLaCgyLYWM+bLngTJlrluc8NH3J5+mwShqwYTKP