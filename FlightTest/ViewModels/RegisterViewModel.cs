using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class RegisterViewModel
    {
        [Remote("CheckEmail", "Account",ErrorMessage ="این ایمیل وجود دارد")]
        [Required(ErrorMessage ="آدرس ایمیل را وارد کنید")]
        public string emailAddress { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string password { get; set; }
        [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید")]
        [Compare("password",ErrorMessage ="رمز های عبور وارد شده یکسان نیست")]
        public string rePassword { get; set; }

        //[Range(typeof(bool),"true","true", ErrorMessage = "You gotta tick the box!")]
        //public bool privacyAgreement { get; set; }
    }
}
