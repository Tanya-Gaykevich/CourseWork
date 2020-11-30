using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        [DisplayName("Middle name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DisplayName("Female")]
        public bool Gender { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number must be in range from 1 to 2147483646")]
        public int Phone { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        [DisplayName("Passport data")]
        public string PassportData { get; set; }
        [Required]
        [Range(0, 101, ErrorMessage = "Number must be in range from 1 to 100")]
        public int Discount { get; set; }

    }
}
