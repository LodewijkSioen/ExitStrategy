using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebsite.Models
{
    public class ViewModel
    {
        [Display(Name = "Test displayName")]
        [Required]
        public string Test { get; set; }
        public DateTime Now { get; set; }
        public int Number { get; set; }
        [Display(Name="This is a Boolean")]
        public bool Boolean { get; set; }
        public SubModel Sub { get; set; }

        public List<string> List { get; set; }
        public static ViewModel Default {
            get
            {
                return new ViewModel
                {
                    Test = "lalalalala",
                    Now = DateTime.Now,
                    Number = 8,
                    Boolean = true,
                    Sub = new SubModel(),
                    List = new List<string> { "Zero", "One", "Two", "Three" }
                };
            }
        }
    }
}