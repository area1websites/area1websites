using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Dist23MVC.Models
{
    public class GroupViewModel
    {
        [Key]
        public int pKey { get; set; }
        public Groups group { get; set; }
        public Contacts contact { get; set; }
    }
}