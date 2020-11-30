using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.TypeOfRest
{
    public class IndexTypeOfRestViewModel
    {
        public IEnumerable<Models.TypeOfRest> TypesOfRest{ get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterTypeOfRestViewModel FilterTypeOfRestViewModel { get; set; }
        public SortTypeOfRestViewModel SortTypeOfRestViewModel { get; set; }
    }
}
