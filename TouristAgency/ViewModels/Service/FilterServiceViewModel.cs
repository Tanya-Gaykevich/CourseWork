using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Service
{
    public class FilterServiceViewModel
    {
        public string SelectedName { get; set; }
       
        public FilterServiceViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}