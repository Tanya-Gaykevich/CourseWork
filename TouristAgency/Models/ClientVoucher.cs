using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class ClientVoucher
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Is reserved")]
        public bool ReservationMark { get; set; }
        [Required]
        [DisplayName("Is paid")]
        public bool PaymentMark { get; set; }
        [Required]
        [DisplayName("Voucher")]
        public int VoucherId { get; set; }
        [Required]
        [DisplayName("Client")]
        public int ClientId { get; set; }
        [Required]

        public Voucher Voucher { get; set; }
        public Client Client { get; set; }
    }
}
