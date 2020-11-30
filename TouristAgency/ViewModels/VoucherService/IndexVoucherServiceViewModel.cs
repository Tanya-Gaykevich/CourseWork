using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.VoucherService
{
    public class IndexVoucherServiceViewModel
    {
        public int VoucherId { get; set; }
        public IEnumerable<Models.Service> Services { get; set; }
        public Service.FilterServiceViewModel FilterServiceViewModel { get; set; }
        public Service.SortServiceViewModel SortServiceViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
