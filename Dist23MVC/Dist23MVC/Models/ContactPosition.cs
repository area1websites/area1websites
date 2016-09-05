using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class ContactPosition
    {
        [Key]
        public int pKey { get; set; }
        public int ContactID { get; set; }
        public int PositionID { get; set; }
        public int GroupID { get; set; }
        public int DistKey { get; set; }

    }
}