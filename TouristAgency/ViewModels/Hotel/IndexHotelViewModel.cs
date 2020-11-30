using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Hotel
{
    public class IndexHotelViewModel
    {
        public IEnumerable<Models.Hotel> Hotels { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterHotelViewModel FilterHotelViewModel { get; set; }
        public SortHotelViewModel SortHotelViewModel { get; set; }
    }
}
