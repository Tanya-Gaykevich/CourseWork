using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Employee
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
        [DisplayName("Position")]
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
