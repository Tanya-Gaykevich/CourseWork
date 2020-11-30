using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.EntityServices;

namespace TouristAgency.ViewModels.HotelRoomPhotoLink
{
    public class SortHotelRoomPhotoLinkViewModel
    {
        public HotelRoomPhotoLinkService.SortState HotelNameSort { get; set; }
        public HotelRoomPhotoLinkService.SortState Current { get; set; }
        public SortHotelRoomPhotoLinkViewModel(HotelRoomPhotoLinkService.SortState sortState)
        {
            HotelNameSort = sortState == HotelRoomPhotoLinkService.SortState.HotelNameAsc ? HotelRoomPhotoLinkService.SortState.HotelNameDesc : HotelRoomPhotoLinkService.SortState.HotelNameAsc;
            Current = sortState;
        }
    }
}
