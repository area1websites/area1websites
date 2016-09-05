using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class PaymentSetupViewModel
    {
        [Key]
        public int pKey { get; set; }
        public PaymentSetup paymentSetup { get; set; }
        public List<PaymentSpecValues> PaymentSpecValues { get; set; }
        public List<PaymentSpecValues> specials { get; set; }
        public PaymentSetupViewModel(int? ID)
        {
            using (Dist23Data db = new Dist23Data())
            {
                paymentSetup = db.PaymentSetup.Where(c => c.pKey == ID).FirstOrDefault();
                specials = db.PaymentSpecValues.Where(x => x.PaymentSetupKey == paymentSetup.pKey).ToList();
                pKey = paymentSetup.pKey;
                if (specials != null)
                {
                    PaymentSpecValues = new List<PaymentSpecValues>();

                    foreach (PaymentSpecValues ps in specials)
                    {
                        PaymentSpecValues.Add(ps);
                    }
                }
            }
        }
    }
}