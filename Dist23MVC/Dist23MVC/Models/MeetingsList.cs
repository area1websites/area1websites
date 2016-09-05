using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class MeetingsList
    {
        [Key]
        public int pKey { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string type { get; set; }
        public string topic { get; set; }
        public string GroupName { get; set; }
        public string location { get; set; }
        public int locationID { get; set; }
        public string city { get; set; }
        public string MapURL { get; set; }
        public string EmbedURL { get; set; }
        public int DistKey { get; set; }
        public string Keylist { get; set; }
    }
}