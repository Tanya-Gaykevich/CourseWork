using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Service
{
    public class IndexServiceViewModel
    {
        public IEnumerable<Models.Service> Services { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterServiceViewModel FilterServiceViewModel { get; set; }
        public SortServiceViewModel SortServiceViewModel { get; set; }
    }
}
