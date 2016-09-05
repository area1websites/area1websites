using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class PaymentViewModel
    {
        [Key]
        public int PaymentKey { get; set; }
        public Payments payment { get; set; }
        public string PaypalButton { get; set; }
    }
}