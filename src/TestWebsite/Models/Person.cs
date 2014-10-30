﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [ScaffoldColumn(false)]
        [Key, ReadOnly(true)]
        public int Id { get; set; }
        [Required, DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Gender { get; set; }
        public int Age { get; set; }
        public bool IsDeceased { get; set; }

        public static IEnumerable<Person> GetBeatles() 
        {
            yield return new Person
            {
                Id = 1,
                FirstName = "Ringo",
                LastName = "Starr",

            };
        }
    }

    public class PersonListItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("")]
        public Link EditLink { get; set; }
    }
}