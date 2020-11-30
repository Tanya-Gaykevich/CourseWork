using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Voucher
{
    public class FilterVoucherViewModel
    {
        public string SelectedName { get; set; }
        public string SelectedCountry { get; set; }
        public DateTime? SelectedStartDate { get; set; }
        public DateTime? SelectedEndDate { get; set; }

        public int? SelectedTypeOfRestId { get; set; }
        public SelectList TypesOfRest { get; set; }

        public FilterVoucherViewModel(string selectedName, string selectedCountry, DateTime? selectedStartDate, DateTime? selectedEndDate,
            int? selectedTypeOfRestId, List<Models.TypeOfRest> typesOfRest)
        {
            SelectedName = selectedName;
            SelectedCountry = selectedCountry;
            SelectedStartDate = selectedStartDate;
            SelectedEndDate = selectedEndDate;

            typesOfRest.Insert(0, new Models.TypeOfRest { Id = 0, Name = "All" });
            SelectedTypeOfRestId = selectedTypeOfRestId;
            TypesOfRest = new SelectList(typesOfRest, "Id", "Name", selectedTypeOfRestId);
        }
    }
}
