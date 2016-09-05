using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class PaymentSpecValues
    {
        [Key]
        public int pKey { get; set; }
        public int PaymentSetupKey { get; set; }
        public string SpecialValue { get; set; }
        public decimal SpecialAmount { get; set; }
    }
}