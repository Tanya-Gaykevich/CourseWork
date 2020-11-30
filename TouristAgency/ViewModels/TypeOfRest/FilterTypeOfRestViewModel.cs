using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.TypeOfRest
{
    public class FilterTypeOfRestViewModel
    {
        public string SelectedName { get; set; }

        public FilterTypeOfRestViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
