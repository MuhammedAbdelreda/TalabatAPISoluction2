﻿using System.ComponentModel.DataAnnotations;

namespace Talabat
{
    public class Address
    {
        public int Id {  get; set; }

        public string FirstName { get; set; }

        public string LastName {  get; set; }

        public string Country { get; set; }

        public string City {  get; set; }

        public string Street { get; set; }
        [Required]
        public string AppUserId {  get; set; } //foreignKey

        public AppUser User { get; set; } //one to one relationship
    }
}