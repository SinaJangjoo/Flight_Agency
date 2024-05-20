using System;
using System.Collections.Generic;

#nullable disable

namespace FlightTest.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            PassengerBirthCountries = new HashSet<Passenger>();
            PassengerPassportCountries = new HashSet<Passenger>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Passenger> PassengerBirthCountries { get; set; }
        public virtual ICollection<Passenger> PassengerPassportCountries { get; set; }
    }
}
