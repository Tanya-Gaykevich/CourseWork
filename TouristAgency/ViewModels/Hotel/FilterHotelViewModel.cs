using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Hotel
{
    public class FilterHotelViewModel
    {
        public string SelectedName { get; set; }

        public FilterHotelViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
