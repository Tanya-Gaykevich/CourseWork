using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class HotelRoomPhotoLink
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomPhotoLink { get; set; }

        public Hotel Hotel { get; set; }
    }
}
