using FlightTest.Data;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBFlightTest db;

        public HomeController(DBFlightTest _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            ViewData["cities"] = db.Cities.ToList();
            ViewData["selectCity"] = new List<string>() { "تهران", "شیراز", "کیش", "اصفهان", "قشم" };
            return View();
        }
        public IActionResult SearchFlight(FlightViewModel model)
        {
            var flights = db.Flights.Include(x => x.Airline)
                 .Include(y => y.Airplane)
                 .Include(z => z.ClassType)
                 .Include(b => b.FromCity)
                 .Include(c => c.ToCity)
                 .Where(x => x.FromCityId == model.fromCityId &&
             x.ToCityId == model.toCityId && x.DepartureTime.Value.Date.Date == model.departureTime.Date.Date).ToList();
            HttpContext.Session.SetInt32("passengerCount", model.count);
            return View(flights);
        }

        public IActionResult GetPopularFlights(string fromCity,string toCity,DateTime date)
        {
            var flights = db.Flights.Include(x => x.Airline)
               .Include(y => y.Airplane)
               .Include(z => z.ClassType)
               .Include(b => b.FromCity)
               .Include(c => c.ToCity)
               .Where(x => x.FromCity.Name == fromCity &&
           x.ToCity.Name == toCity && x.DepartureTime.Value.Date.Date == date.Date.Date).ToList();
            return View(nameof(SearchFlight),flights);
        }

        [HttpPost]
        public IActionResult FindDestination(int id)
        {
            var allDestinations = new List<string>(){"Tehran","Shiraz","Kish","Isfahan","Qeshm","Mashhad" };
            var destinations= new List<string>() { "Tehran", "Shiraz", "Kish", "Isfahan", "Qeshm", "Mashhad" };
            destinations.Remove(destinations[id]);
            object value = new { destinations = destinations, location = allDestinations[id] };
            return Json(value);
        }
    }
}
