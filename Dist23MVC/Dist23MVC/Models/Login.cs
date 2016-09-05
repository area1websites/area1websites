using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;


namespace Dist23MVC.Models
{
    public class Login
    {
        [Key]
        public int pKey { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int level { get; set; }
        public string Name  { get; set; }
        public int DistKey { get; set; }

    }
}