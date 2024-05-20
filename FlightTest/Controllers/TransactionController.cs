using FlightTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult CheckOut(int totalPrice)
        {
            TempData["totalPrice"] = totalPrice;
            return View();
        }

        public IActionResult TransactionAccepted()
        {
            ViewData["dialog"] = $"عملیات رزرو بلیط با موفقیت انجام شد ، شماره پیگیری :";
            return View();
        }
        public IActionResult TransactionFailed()
        {
            ViewData["dialog"] = "عملیات رزرو بلیط با خطا مواجه شد !";
            return View();
        }
    }
}

