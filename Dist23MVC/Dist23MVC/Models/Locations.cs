using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Locations
    {
        [Key]
        public int pKey { get; set; }
        public string Location { get; set; }
        public string MapUIRL { get; set; }
        public string EmbedURL { get; set; }
        public int DistKey { get; set; }
        public string Address { get; set; }
    }
}