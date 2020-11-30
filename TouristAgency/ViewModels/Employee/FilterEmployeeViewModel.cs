using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Employee
{
    public class FilterEmployeeViewModel
    {
        public string SelectedLastName { get; set; }
        public int? SelectedAge { get; set; }

        public SelectList Positions { get; set; }
        public int? SelectedPositionId { get; set; }

        public FilterEmployeeViewModel(string selectedLastName, int? selectedAge, List<Models.Position> positions, int? selectedPositionId)
        {
            SelectedLastName = selectedLastName;
            SelectedAge = selectedAge;

            positions.Insert(0, new Models.Position { Id = 0, Name = "All" });
            Positions = new SelectList(positions, "Id", "Name", selectedPositionId);
            SelectedPositionId = selectedPositionId;
        }
    }
}
