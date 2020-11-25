using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Position
{
    public class IndexPositionViewModel
    {
        public IEnumerable<Models.Position> Positions { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterPositionViewModel FilterPositionViewModel { get; set; }
        public SortPositionViewModel SortPositionViewModel { get; set; }
    }
}
