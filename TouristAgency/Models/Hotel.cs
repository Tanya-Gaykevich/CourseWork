using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Hotel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Country { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Town { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number must be in range from 1 to 2147483646")]
        public int Phone { get; set; }
        [Required]
        [Range(0, 6, ErrorMessage = "Number of stars must be in range from 0 to 5")]
        public int Stars { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length must be in range from 5 to 100", MinimumLength = 5)]
        [DisplayName("Link of the hotel photo")]
        public string HotelPhotoLink { get; set; }
    }
}
