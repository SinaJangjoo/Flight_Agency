using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class TransactionViewModel
    {
        public int reservationId { get; set; }
        public bool isAccepted { get; set; }
        public int totalPaidPrice { get; set; }
        public DateTime transactionDateTime { get; set; }
        public string cardNumber { get; set; }
    }
}
