using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Links
    {
        [Key]
        public int pKey { get; set; }
        public string LinkText { get; set; }
        public string linkURL { get; set; }
        public int ListOrder { get; set; }
        public string LinkComment { get; set; }
        public string target { get; set; }
        public int DistKey { get; set; }

    }
}