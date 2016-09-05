using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class Positions
    {
        [Key]
        public int pKey { get; set; }
        public string PositionName { get; set; }
        public string CommitteeTitle { get; set; }
        public bool isCommittee { get; set; }
    }
}