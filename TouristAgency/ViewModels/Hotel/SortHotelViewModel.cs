using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Hotel
{
    public class SortHotelViewModel
    {
        public EntityServices.HotelService.SortState NameSort { get; set; }
        public EntityServices.HotelService.SortState Current { get; set; }
        public SortHotelViewModel(EntityServices.HotelService.SortState sortState)
        {
            NameSort = sortState == EntityServices.HotelService.SortState.NameAsc ? EntityServices.HotelService.SortState.NameDesc : EntityServices.HotelService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
