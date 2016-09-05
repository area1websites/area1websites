using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class News
    {
        [Key]
        public int pKey { get; set; }
        public string Header { get; set; }
        public string NewsText { get; set; }
        public string NewsLink { get; set; }
        public string LinkText { get; set; }
        public int? EventKey { get; set; }
        public int? ListOrder { get; set; }
        public int DistKey { get; set; }

        public News()
        {
            DistKey = GlobalVariables.DistKey;
        }
    }
}