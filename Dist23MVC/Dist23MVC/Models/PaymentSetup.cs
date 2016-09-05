using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class PaymentSetup
    {
        [Key]
        public int pKey { get; set; }
        public int EventKey { get; set; }
        public Decimal? Amount { get; set; }
        public bool hasSpecial { get; set; }
        public string ButtonLink { get; set; }
    }
}