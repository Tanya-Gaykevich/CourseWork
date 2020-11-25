using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Client
{
    public class FilterClientViewModel
    {
        public string SelectedSurname { get; set; }
        public bool? SelectedGender { get; set; }

        public FilterClientViewModel(string selectedSurname, bool? selectedGender)
        {
            SelectedSurname = selectedSurname;
            SelectedGender = selectedGender;
        }
    }
}
