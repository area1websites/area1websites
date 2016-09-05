using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class CommLinks
    {
        [Key]
        public int pKey { get; set; }
        public int DistKey { get; set; }
        public int CommKey { get; set; }
        public string LinkTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string LinkHeader { get; set; }
        public string LinkText { get; set; }
        public string LinkURL { get; set; }

    }
}