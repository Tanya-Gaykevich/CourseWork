using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Voucher
{
    public class VoucherViewModel
    {
        public Models.Voucher Voucher { get; set; }
        public string EmployeeName { get; set; }
        public string HotelName { get; set; }
        public string HotelCountryName { get; set; }
        public string TypeOfRestName { get; set; }
        public int Frequency { get; set; }
    }
}
