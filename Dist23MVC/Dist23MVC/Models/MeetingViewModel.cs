using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class MeetingViewModel
    {
        [Key]
        public int pKey { get; set; }
        public Meetings meeting { get; set; }
        public string GroupName { get; set; }
        public string Location { get; set; }
    }
}