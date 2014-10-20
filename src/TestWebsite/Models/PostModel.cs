using System.ComponentModel.DataAnnotations;

namespace TestWebsite.Models
{
    public class PostModel
    {
        public bool Boolean { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Test { get; set; }
    }
}