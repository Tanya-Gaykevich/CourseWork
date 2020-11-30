using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Voucher
{
    public class DetailsViewModel
    {
        public Models.Voucher Voucher { get; set; }
        public IEnumerable<Models.Service> Services { get; set; }
    }
}
