using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "آدرس ایمیل را وارد کنید")]
        public string emailAddress { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }
}
