using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class City
    {
        public City()
        {
            FlightFromCities = new HashSet<Flight>();
            FlightToCities = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Flight> FlightFromCities { get; set; }
        public virtual ICollection<Flight> FlightToCities { get; set; }
    }
}
