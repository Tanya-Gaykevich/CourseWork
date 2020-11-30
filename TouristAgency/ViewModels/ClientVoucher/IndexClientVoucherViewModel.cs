using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.ClientVoucher
{
    public class IndexClientVoucherViewModel
    {
        public IEnumerable<Models.ClientVoucher> ClientsVouchers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterClientVoucherViewModel FilterClientVoucherViewModel { get; set; }
        public SortClientVoucherViewModel SortClientVoucherViewModel { get; set; }
    }
}
