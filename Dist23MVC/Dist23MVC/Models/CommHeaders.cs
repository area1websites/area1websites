using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dist23MVC.Models
{
    public class CommHeaders
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public string CommName { get; set; }
        [DataType(DataType.MultilineText)]
        public string CommHeader { get; set; }
        public int PositionKey { get; set; }
    }
}