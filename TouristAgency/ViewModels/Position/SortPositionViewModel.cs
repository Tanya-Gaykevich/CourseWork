using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Position
{
    public enum SortState
    {
        NameAsc,
        NameDesc
    }
    public class SortPositionViewModel
    {
        public SortState NameSort { get; set; }
        public SortState Current { get; set; }
        public SortPositionViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
