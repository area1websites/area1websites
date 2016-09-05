using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dist23MVC.Models
{
    public class Events
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public int EventCat { get; set; }
        [DataType(DataType.MultilineText)]
        public string EventName { get; set; }
        public string Eventlink { get; set; }
        public string EventLinkText { get; set; }
        public bool? ShowRegisterLink { get; set; }
        public DateTime? EventDate { get; set; }
    }

    public class EventCat
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public string EventCatName { get; set; }
    }

    public class OtherDistEvents
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public int ShowDistKey { get; set; }
    }
}