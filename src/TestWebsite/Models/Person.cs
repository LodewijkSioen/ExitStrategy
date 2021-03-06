﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExitStrategy.TestWebsite.Models
{
    public enum Sex
    {
        Unknown,
        Male,
        Female,
        Other
    }

    public class Person
    {
        public Person()
        {
            BirthDate = DateTime.Now;
            Gender = Sex.Unknown;
        }

        [ScaffoldColumn(false)]
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        [Required, DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
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
                FirstName = "George",
                LastName = "Harrison",
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
        [DisplayName("First Name"), Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name"), Required]
        public string LastName { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime BirthDate { get; set; }
        [DisplayName("")]
        public Link EditLink { get; set; }
    }
}