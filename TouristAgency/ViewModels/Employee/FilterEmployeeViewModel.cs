using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Employee
{
    public class FilterEmployeeViewModel
    {
        public string SelectedSurname { get; set; }

        public SelectList Positions { get; set; }
        public int? SelectedPositionId { get; set; }

        public FilterEmployeeViewModel(string selectedSurname, List<Models.Position> positions, int? selectedPositionId)
        {
            positions.Insert(0, new Models.Position { Id = 0, Name = "All" });
            SelectedSurname = selectedSurname;
            SelectedPositionId = selectedPositionId;
            Positions = new SelectList(positions, "Id", "Name", selectedPositionId);
        }
    }
}
