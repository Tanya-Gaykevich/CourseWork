using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class ServiceVoucher
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Voucher")]
        public int VoucherId { get; set; }
        [Required]
        [DisplayName("Service")]
        public int ServiceId { get; set; }

        public Service Service { get; set; }
        public Voucher Voucher { get; set; }
    }
}

