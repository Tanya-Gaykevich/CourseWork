using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.HotelRoomPhotoLink
{
    public class IndexHotelRoomPhotoLinkViewModel
    {
        public IEnumerable<Models.HotelRoomPhotoLink> HotelRoomPhotoLinks { get; set; }
        public FilterHotelRoomPhotoLinkViewModel FilterHotelRoomPhotoLinkViewModel { get; set; }
        public SortHotelRoomPhotoLinkViewModel SortHotelRoomPhotoLinkViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
