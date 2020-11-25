using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class ClientVoucher
    {
        public int Id { get; set; }
        public bool ReservationMark { get; set; }
        public bool PaymentMark { get; set; }
        public int VoucherId { get; set; }
        public int ClientId { get; set; }

        public Voucher Voucher { get; set; }
        public Client Client { get; set; }
    }
}
