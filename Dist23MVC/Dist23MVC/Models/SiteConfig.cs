using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class SiteConfig
    {
        [Key]
        public int DistKey { get; set; }
        public string DistURL { get; set; }
        public string DistStyle { get; set; }
        public string BannerTitle { get; set; }
        public string BannerSubTitle { get; set; }
        public string HotlinePh { get; set; }
        public string AltHotline { get; set; }
        public string AltHotlineMsg { get; set; }
        public string DomainName { get; set; }
    }
}