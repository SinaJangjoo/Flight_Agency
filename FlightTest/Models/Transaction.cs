using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public bool IsAccepted { get; set; }
        public int TotalPaidPrice { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string CardNumber { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
