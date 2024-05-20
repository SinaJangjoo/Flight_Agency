using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightTest.Areas.Identity.Data;
using FlightTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightTest.Data
{
    public class DBFlightTest : IdentityDbContext<ApplicationUser>
    {
        public DBFlightTest(DbContextOptions<DBFlightTest> options)
            : base(options)
        {
        }
        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ClassType> ClassTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<PassengerReservation> PassengerReservations { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Flight>().HasOne(x => x.FromCity).WithMany(y => y.FlightFromCities).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Flight>().HasOne(x => x.ToCity).WithMany(y => y.FlightToCities).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Passenger>().HasOne(x => x.BirthCountry).WithMany(y => y.PassengerBirthCountries).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Passenger>().HasOne(x => x.PassportCountry).WithMany(y => y.PassengerPassportCountries).OnDelete(DeleteBehavior.NoAction);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
