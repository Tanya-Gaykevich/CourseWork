using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Service
{
    public class SortServiceViewModel
    {
        public EntityServices.ServiceService.SortState NameSort { get; set; }
        public EntityServices.ServiceService.SortState Current { get; set; }
        public SortServiceViewModel(EntityServices.ServiceService.SortState sortState)
        {
            NameSort = sortState == EntityServices.ServiceService.SortState.NameAsc ? EntityServices.ServiceService.SortState.NameDesc : EntityServices.ServiceService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
