using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class dayorder
    {
        [Key]
        public int sOrder { get; set; }
        public string Day { get; set; }
    }
}