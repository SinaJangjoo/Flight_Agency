using FlightTest.Areas.Identity.Data;
using FlightTest.Data;
using FlightTest.Models;
using FlightTest.Utilities;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    public class ReservationController : Controller
    {
        private readonly DBFlightTest db;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationController(DBFlightTest _db,UserManager<ApplicationUser>_userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public IActionResult Index(int id)
        {
            var flight = db.Flights.Include(x => x.Airline)
               .Include(y => y.Airplane)
               .Include(z => z.ClassType)
               .Include(b => b.FromCity)
               .Include(c => c.ToCity).SingleOrDefault(x=>x.Id==id);
            TempData["countries"] = db.Countries.ToList();
            TempData["flight"] =flight;
            return View();
        }

        public IActionResult ConfirmReservation(ReservationViewModel model, int flightId)
        {
            var flight = db.Flights.Include(x => x.Airline)
              .Include(y => y.Airplane)
              .Include(z => z.ClassType)
              .Include(b => b.FromCity)
              .Include(c => c.ToCity).SingleOrDefault(x => x.Id == flightId);
            SessionExtensions.SetObject(HttpContext.Session, "reservation", model);
            SessionExtensions.SetObject(HttpContext.Session, "flightId", flightId);
            TempData["countries"] = db.Countries.ToList();
            TempData["flight"] = flight;
            return View(model);
        }

        public async Task<IActionResult> CreateReservation(TransactionViewModel trmodel)
        {
            if (trmodel.isAccepted)
            {
            var flightId = SessionExtensions.GetObject<int>(HttpContext.Session, "flightId");
            var model = SessionExtensions.GetObject<ReservationViewModel>(HttpContext.Session, "reservation");
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            Reservation reservation = new Reservation()
            { 
                UserId = user.Id,
                FlightId = flightId,
                ReservationDateTime = DateTime.Now,
            };
            Random random = new Random();
            reservation.Transactions.Add(new Transaction()
            {
                Reservation=reservation,
                CardNumber=trmodel.cardNumber,
                IsAccepted=trmodel.isAccepted,
                TotalPaidPrice=trmodel.totalPaidPrice,
                TransactionDateTime=DateTime.Now
            });
            reservation.ReservationCode = random.Next().ToString("x");
            db.Add(reservation);
            
            foreach (var item in model.passengers)
            {
                Passenger passenger = new Passenger()
                {
                    UserId = user.Id,
                    FirstName = item.firstName,
                    LastName = item.lastName,
                    LatinFirstName = item.latinFirstName,
                    LatinLastName = item.latinLastName,
                    BirthDate = item.birthDate,
                    Gender = item.gender,
                    IsIranian = item.isIranian
                };
                if (item.isIranian)
                {
                    passenger.NationalCode = item.nationalCode;
                    passenger.BirthCountryId = 50;
                }
                else
                {
                    passenger.BirthCountryId = item.birthCountryId;
                    passenger.PassportCountryId = item.passportCountryId;
                    passenger.PassportNumber = item.passportNumber;
                    passenger.PassportExpirationDate = item.passportExpirationDate;
                }
                passenger.PassengerReservations.Add(new PassengerReservation()
                {
                    Passenger = passenger,
                    Reservation = reservation
                });
                db.Add(passenger);
            }
                TempData["reservationCode"] = reservation.ReservationCode;
            db.SaveChanges();
            return RedirectToAction("TransactionAccepted", "Transaction");
            }
            else
            {
            return RedirectToAction("TransactionFailed", "Transaction");
            }
        }
    }
}
