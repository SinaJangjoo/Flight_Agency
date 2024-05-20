using FlightTest.Areas.Identity.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            PassengerReservations = new HashSet<PassengerReservation>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LatinFirstName { get; set; }
        public string LatinLastName { get; set; }
        public bool IsIranian { get; set; }
        public int BirthCountryId { get; set; }
        public DateTime BirthDate { get; set; }
        public int? PassportCountryId { get; set; }
        public string NationalCode { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportExpirationDate { get; set; }
        public bool Gender { get; set; }
        public virtual Country BirthCountry { get; set; }
        public virtual Country PassportCountry { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PassengerReservation> PassengerReservations { get; set; }
    }
}
