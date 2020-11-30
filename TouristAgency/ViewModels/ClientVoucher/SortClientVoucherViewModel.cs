using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.ClientVoucher
{
    public class SortClientVoucherViewModel
    {
        public EntityServices.ClientVoucherService.SortState ClientLastNameSort { get; set; }
        
        public EntityServices.ClientVoucherService.SortState Current { get; set; }
        public SortClientVoucherViewModel(EntityServices.ClientVoucherService.SortState sortState)
        {
            ClientLastNameSort = sortState == EntityServices.ClientVoucherService.SortState.ClientLastNameAsc ? EntityServices.ClientVoucherService.SortState.ClientLastNameDesc : EntityServices.ClientVoucherService.SortState.ClientLastNameAsc;
            Current = sortState;
        }
    }
}
