using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.ClientVoucher
{
    public class FilterClientVoucherViewModel
    {
        public string SelectedClientLastName { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsReserved { get; set; }

        public FilterClientVoucherViewModel(string selectedClientLastName, bool? isPaid, bool? isReserved)
        {
            SelectedClientLastName = selectedClientLastName;
            IsPaid = isPaid;
            IsReserved = isReserved;
        }
    }
}
