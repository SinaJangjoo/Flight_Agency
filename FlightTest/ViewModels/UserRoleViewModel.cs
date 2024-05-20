using FlightTest.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTest.ViewModels
{
    public class UserRoleViewModel
    {
        
        public List<ApplicationUser> users { get; set; }
        public List<string> roleNames { get; set; }
        

    }
}
