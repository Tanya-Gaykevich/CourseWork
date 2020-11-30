using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class HotelRoomPhotoLink
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Hotel")]
        public int HotelId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Length must be in range from 1 to 10", MinimumLength = 1)]
        [DisplayName("Room number")]
        public string RoomNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length must be in range from 5 to 100", MinimumLength = 5)]
        [DisplayName("Link of the room photo")]
        public string RoomPhotoLink { get; set; }

        public Hotel Hotel { get; set; }
    }
}
