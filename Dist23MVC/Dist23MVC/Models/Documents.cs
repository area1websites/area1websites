using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Documents
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public int GroupKey { get; set; }
        public string DocTitle { get; set; }
        public string DocInfo { get; set; }
        public string DocLink { get; set; }
        public DateTime DocDate { get; set; }
    }
}