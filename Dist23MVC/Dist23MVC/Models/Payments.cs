using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Payments
    {
        [Key]
        public int pKey { get; set; }
        public int EventKey { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fellowship { get; set; }
        public string Homegroup { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool isPaid { get; set; }
    }
}