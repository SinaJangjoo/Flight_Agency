using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class PassengerReservation
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int PassengerId { get; set; }

        public virtual Passenger Passenger { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
