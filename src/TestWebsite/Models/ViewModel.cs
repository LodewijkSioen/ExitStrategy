using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebsite.Models
{
    public enum Sex
    {
        Unknown,
        Male,
        Female
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Gender { get; set; }
        public int Age { get; set; }
        public bool IsDeceased { get; set; }

        public static IEnumerable<Person> GetBeatles() 
        {
            yield return new Person
            {
                FirstName = "Ringo",
                LastName = "Starr",

            };
        }
    }
}