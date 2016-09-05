using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class VolunteerList
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public int CommKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime VolDate { get; set; }
    }
}