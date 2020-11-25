using System;
using System.Collections.Generic;

namespace TouristAgency.Models
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HotelId { get; set; }
        public int TypeOfRestId { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public Hotel Hotel { get; set; }
        public TypeOfRest TypeOfRest { get; set; }
    }
}
