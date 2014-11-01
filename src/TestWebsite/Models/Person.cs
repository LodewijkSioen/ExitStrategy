using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestWebsite.Models
{
    public enum Sex
    {
        Male,
        Female,
        Other
    }

    public class Person
    {
        [ScaffoldColumn(false)]
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        [Required, DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Sex Gender { get; set; }
        public bool IsDeceased { get; set; }

        public static IEnumerable<Person> GetBeatles() 
        {
            yield return new Person
            {
                Id = 1,
                FirstName = "Ringo",
                LastName = "Starr",
                BirthDate = new DateTime(1940, 7, 6),
                Gender = Sex.Male,
                IsDeceased = false
            };
            yield return new Person
            {
                Id = 2,
                FirstName = "John",
                LastName = "Lennon",
                BirthDate = new DateTime(1940, 10, 9),
                Gender = Sex.Male,
                IsDeceased = true
            };
            yield return new Person
            {
                Id = 3,
                FirstName = "Harrison",
                LastName = "George",
                BirthDate = new DateTime(1943, 2, 24),
                Gender = Sex.Male,
                IsDeceased = true
            };
            yield return new Person
            {
                Id = 4,
                FirstName = "Paul",
                LastName = "McCartney",
                BirthDate = new DateTime(1942, 6, 18),
                Gender = Sex.Male,
                IsDeceased = false
            };
        }
    }

    public class PersonListItem
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("")]
        public Link EditLink { get; set; }
    }
}