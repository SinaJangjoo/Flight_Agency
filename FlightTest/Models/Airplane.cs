using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Airplane
    {
        public Airplane()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
