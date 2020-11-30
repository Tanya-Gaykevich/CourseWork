using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Position
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Name { get; set; }
    }
}
