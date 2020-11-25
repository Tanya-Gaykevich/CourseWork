using System;
using System.Collections.Generic;

namespace TouristAgency.Models
{
    public partial class ServiceVoucher
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public int ServiceId { get; set; }

        public Service Service { get; set; }
        public Voucher Voucher { get; set; }
    }
}

