using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Groups
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public string GroupName { get; set; }
        public string GroupNumber { get; set; }
        public int? ContactID { get; set; }
        public bool isDistrict { get; set; }
    }
}