using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int AirlineId { get; set; }
        public int AirplaneId { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int ClassTypeId { get; set; }
        public string FlightNumber { get; set; }
        public int? AllowedCargoWeight { get; set; }
        public int? Capacity { get; set; }
        public int? Price { get; set; }

        public virtual Airline Airline { get; set; }
        public virtual Airplane Airplane { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual City FromCity { get; set; }
        public virtual City ToCity { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
