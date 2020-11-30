using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Service
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Length must be in range from 5 to 150", MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number must be in range from 1 to 2147483646")]
        public decimal Cost { get; set; }
    }
}
