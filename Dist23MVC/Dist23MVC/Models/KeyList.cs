using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class KeyList
    {
        [Key]
        public int key_id { get; set; }
        public int keyNo { get; set; }
        public string keyDef { get; set; }
        public int DistKey { get; set; }
    }
}