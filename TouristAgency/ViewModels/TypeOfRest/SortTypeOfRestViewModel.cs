using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.TypeOfRest
{
    public class SortTypeOfRestViewModel
    {
        public EntityServices.TypeOfRestService.SortState NameSort { get; set; }
        public EntityServices.TypeOfRestService.SortState Current { get; set; }
        public SortTypeOfRestViewModel(EntityServices.TypeOfRestService.SortState sortState)
        {
            NameSort = sortState == EntityServices.TypeOfRestService.SortState.NameAsc ? EntityServices.TypeOfRestService.SortState.NameDesc : EntityServices.TypeOfRestService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
