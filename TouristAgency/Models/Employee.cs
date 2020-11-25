using System;
using System.Collections.Generic;

namespace TouristAgency.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
