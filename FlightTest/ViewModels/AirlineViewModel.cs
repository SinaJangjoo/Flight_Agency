using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class AirlineViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="پر کردن این فیلد اجباریست")]
        [Remote("CheckName","Airline",ErrorMessage ="این ایرلاین قبلا ثبت شده است")]
        public string name { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="پر کردن این فیلد اجباریست")]
        public IFormFile logoImage { get; set; }
    }
}
