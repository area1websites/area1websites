using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dist23MVC.Models
{
    public class EventViewModel
    {
        public string EventCatName { get; set; }
        public List<Events> Events { get; set; }
        public EventViewModel()
        {
            Events = new List<Events>();
        }
    }
}