using FlightTest.Areas.Identity.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            PassengerReservations = new HashSet<PassengerReservation>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int FlightId { get; set; }
        public string ReservationCode { get; set; }
        public DateTime? ReservationDateTime { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PassengerReservation> PassengerReservations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
