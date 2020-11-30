using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristAgency.Models
{
    public partial class Voucher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Length must be in range from 5 to 50", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
        [Required]
        [DisplayName("Hotel")]
        public int HotelId { get; set; }
        [Required]
        [DisplayName("Type of rest")]
        public int TypeOfRestId { get; set; }
        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public Hotel Hotel { get; set; }
        [DisplayName("Type of rest")]
        public TypeOfRest TypeOfRest { get; set; }
    }
}
