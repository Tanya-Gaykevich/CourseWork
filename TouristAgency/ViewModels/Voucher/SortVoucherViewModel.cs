using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Voucher
{
    public class SortVoucherViewModel
    {
        public EntityServices.VoucherService.SortState NameSort { get; set; }
        public EntityServices.VoucherService.SortState FrequencySort { get; set; }
        public EntityServices.VoucherService.SortState Current { get; set; }
        public SortVoucherViewModel(EntityServices.VoucherService.SortState sortState)
        {
            NameSort = sortState == EntityServices.VoucherService.SortState.NameAsc ? EntityServices.VoucherService.SortState.NameDesc : EntityServices.VoucherService.SortState.NameAsc;
            FrequencySort = sortState == EntityServices.VoucherService.SortState.OrderFrequencyAsc ? EntityServices.VoucherService.SortState.OrderFrequencyDesc : EntityServices.VoucherService.SortState.OrderFrequencyAsc;
            Current = sortState;
        }
    }
}
