using System;
using System.Collections.Generic;

namespace TouristAgency.Models
{
    public partial class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int Stars { get; set; }
        public string ContactPerson { get; set; }
        public string HotelPhotoLink { get; set; }
    }
}
