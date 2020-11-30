using System;
using TouristAgency.EntityServices;

namespace TouristAgency.ViewModels.Employee
{
    public class SortEmployeeViewModel
    {
        public EmployeeService.SortState LastNameSort { get; set; }
        public EmployeeService.SortState Current { get; set; }
        public SortEmployeeViewModel(EmployeeService.SortState sortState)
        {
            LastNameSort = sortState == EmployeeService.SortState.LastNameAsc ? EmployeeService.SortState.LastNameDesc : EmployeeService.SortState.LastNameAsc;
            Current = sortState;
        }
    }
}
