using System;
using System.Collections.Generic;

namespace TouristAgency.Models
{
    public partial class TypeOfRest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Limitation { get; set; }
    }
}
