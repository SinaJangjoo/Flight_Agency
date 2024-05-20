using FlightTest.Data;
using FlightTest.Models;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    [Authorize(Policy = "AdminsPolicy")]
    public class AirlineController : Controller
    {
        private DBFlightTest db;
        public AirlineController(DBFlightTest _db) => db = _db;
        public IActionResult Management()
        {
            ViewData["Airlines"] = db.Airlines.ToList();
            return View();
        }

        public IActionResult InsertAirline(AirlineViewModel model)
        {
            Airline airline = new Airline();
            airline.Name = model.name;
            if (model.logoImage != null)
            {
                string ext = System.IO.Path.GetExtension(model.logoImage.FileName).ToLower();
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    if (model.logoImage.Length <= 5 * Math.Pow(1024, 2))
                    {
                        byte[] b = new byte[model.logoImage.Length];
                        model.logoImage.OpenReadStream().Read(b, 0, b.Length);
                        MemoryStream memoryStream = new MemoryStream(b);
                        Image image = Image.FromStream(memoryStream);
                        Bitmap bitmap = new Bitmap(image, 200, 200);
                        MemoryStream thumbnailFile = new MemoryStream();
                        bitmap.Save(thumbnailFile, ImageFormat.Jpeg);
                        byte[] b2 = thumbnailFile.ToArray();
                        airline.LogoImage = b2;
                    }
                }
            }
            db.Add(airline);
            var status = db.SaveChanges();
            if (status > 0)
                TempData["success"] = "ایرلاین با موفقیت ثبت شد";
            else
                TempData["error"] = "! خطا در ثبت ایرلاین";

            return RedirectToAction("Management", "Airline");
        }
        public IActionResult CheckName(string name)
        {
            if (db.Airlines.Any(x => x.Name == name))
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }


        [HttpDelete]
        public IActionResult DeleteAirline(int Id)
        {
            Airline airline = db.Airlines.Find(Id);
            db.Remove(airline);
            var status = db.SaveChanges();
            if (status > 0)
            {
                TempData["success"] = "ایرلاین با موفقیت حذف شد";
                return Json(new { success = true });
            }
            else
            {
                TempData["error"] = "! خطا در حذف ایرلاین";
                return Json(new { success = false });
            }
        }
    }
}
