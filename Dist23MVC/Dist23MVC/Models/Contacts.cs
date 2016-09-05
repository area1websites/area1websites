using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Contacts
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public int AccessLvl { get; set; }
        public string Address { get; set; }

    }
}