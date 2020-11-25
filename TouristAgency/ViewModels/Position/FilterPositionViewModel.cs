using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Position
{
    public class FilterPositionViewModel
    {
        public string SelectedName { get; set; }
       
        public FilterPositionViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
