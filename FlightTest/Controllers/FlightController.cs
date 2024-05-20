using FlightTest.Data;
using FlightTest.Models;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    [Authorize(Policy = "AdminsPolicy")]
    public class FlightController : Controller
    {
        private DBFlightTest db;
        public FlightController(DBFlightTest _db) => db = _db;
        public IActionResult InsertFlight()
        {
            ViewData["airlines"] = db.Airlines.ToList();
            ViewData["airplanes"] = db.Airplanes.ToList();
            ViewData["cities"] = db.Cities.ToList();
            ViewData["classTypes"] = db.ClassTypes.ToList();
            return View();
        }

        public IActionResult InsertFlightConfirm(FlightViewModel model)
        {
            Flight flight = new Flight
            {
                FromCityId = model.fromCityId,
                ToCityId = model.toCityId,
                AirlineId = model.airlineId,
                AirplaneId = model.airplaneId,
                ClassTypeId = model.classTypeId,
                DepartureTime = model.departureTime,
                ArrivalTime = model.arrivalTime,
                FlightNumber = model.flightNumber,
                AllowedCargoWeight = model.allowCargoWeight,
                Capacity = model.capacity,
                Price = model.price
            };
            db.Add(flight);
           var status= db.SaveChanges();
            if (status > 0)
                TempData["success"] = "پرواز با موفقیت ثبت شد";
            else
                TempData["error"] = "! خطا در ثبت پرواز";

            return RedirectToAction("InsertFlight", "Flight");
        }

        public IActionResult UpdateDelete()
        {
            TempData["cities"] = db.Cities.ToList();
            TempData["airlines"] = db.Airlines.ToList();
            return View();
        }

        public IActionResult SearchFlight(int fromCityId, int toCityId, DateTime departureTime)
        {
            var flights = db.Flights
                .Include(x => x.Airline)
                .Include(y => y.Airplane)
                .Include(z => z.ClassType)
                .Include(b => b.FromCity)
                .Include(c => c.ToCity)
                .Where(d => d.FromCityId == fromCityId && d.ToCityId == toCityId &&
                    d.DepartureTime.Value.Date.Date == departureTime.Date.Date).ToList();
            TempData["cities"] = db.Cities.ToList();
            TempData["airlines"] = db.Airlines.ToList();
            return View("UpdateDelete", flights);

        }
        public IActionResult EditFlight(int id)
        {
            var flight = db.Flights.Include(x => x.Airline).Include(y => y.Airplane).Include(z => z.ClassType)
                .Include(b => b.FromCity).Include(c => c.ToCity).SingleOrDefault(d => d.Id == id);
            TempData["flight"] = flight;
            TempData["airlines"] = db.Airlines.ToList();
            TempData["airplanes"] = db.Airplanes.ToList();
            TempData["cities"] = db.Cities.ToList();
            TempData["classTypes"] = db.ClassTypes.ToList();
            return View();
        }

        public IActionResult EditFlightConfirm(FlightViewModel model, int id)
        {
            Flight flight = new Flight
            {
                Id = id,
                FromCityId = model.fromCityId,
                ToCityId = model.toCityId,
                AirlineId = model.airlineId,
                AirplaneId = model.airplaneId,
                ClassTypeId = model.classTypeId,
                DepartureTime = model.departureTime,
                ArrivalTime = model.arrivalTime,
                FlightNumber = model.flightNumber,
                AllowedCargoWeight = model.allowCargoWeight,
                Capacity = model.capacity,
                Price = model.price
            };
            db.Update(flight);
            var status= db.SaveChanges();
            if (status > 0)
                TempData["success"] = "پرواز با موفقیت ویرایش شد";
            else
                TempData["error"] = "! خطا در ویرایش پرواز";

            return RedirectToAction("UpdateDelete", "Flight");
        }

        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            var deleteFlight = db.Flights.Find(id);
            db.Remove(deleteFlight);
            var status = db.SaveChanges();
            if (status > 0)
            {
                TempData["success"] = "پرواز با موفقیت حذف شد";
                return Json(new { success = true });
            }
            else
            {
                TempData["error"] = "! خطا در حذف پرواز";
                return Json(new { success = false });
            }
        }

        public IActionResult FlightNumSearch(string flightNumber)
        {
            var flights = db.Flights
                .Include(x => x.Airline)
                .Include(y => y.Airplane)
                .Include(z => z.ClassType)
                .Include(b => b.FromCity)
                .Include(c => c.ToCity)
                .Where(x => x.FlightNumber.ToLower() == flightNumber.ToLower()).ToList();
            return Json(flights.Select(x => new
            {
                id = x.Id,
                flightNumber = x.FlightNumber,
                capacity = x.Capacity,
                price = x.Price,
                airlineName = x.Airline.Name,
                airlineImg = $"data:image/jpeg;base64,{Convert.ToBase64String(x.Airline.LogoImage)}",
                airplaneName = x.Airplane.Name,
                classType = x.ClassType.Name,
                fromCity = x.FromCity.Name,
                toCity = x.ToCity.Name,
                allowCargoWeight = x.AllowedCargoWeight,
                arrivalTime = x.ArrivalTime.Value,
                departureTime = x.DepartureTime.Value
            }));
        }
    }
}
