using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class PassengerViewModel
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string latinFirstName { get; set; }
        public string latinLastName { get; set; }
        public bool isIranian { get; set; }
        public int birthCountryId { get; set; }
        public DateTime birthDate { get; set; }
        public int? passportCountryId { get; set; }
        public string nationalCode { get; set; }
        public string passportNumber { get; set; }
        public DateTime? passportExpirationDate { get; set; }
        public bool gender { get; set; }
    }
}
