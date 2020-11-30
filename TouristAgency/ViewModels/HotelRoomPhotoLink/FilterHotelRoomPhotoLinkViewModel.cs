using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.HotelRoomPhotoLink
{
    public class FilterHotelRoomPhotoLinkViewModel
    {
        public string SelectedHotelName { get; set; }
        public FilterHotelRoomPhotoLinkViewModel(string selectedHotelName)
        {
            SelectedHotelName = selectedHotelName;
        }
    }
}
