using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Voucher
{
    public class IndexVoucherViewModel
    {
        public IEnumerable<VoucherViewModel> Vouchers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterVoucherViewModel FilterVoucherViewModel { get; set; }
        public SortVoucherViewModel SortVoucherViewModel { get; set; }
    }
}
