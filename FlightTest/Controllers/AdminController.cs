using FlightTest.Areas.Identity.Data;
using FlightTest.Data;
using FlightTest.Models;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class AdminController : Controller
    {
        private DBFlightTest db;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public AdminController(DBFlightTest _db, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            db = _db;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManagement()
        {
            UserRoleViewModel model = new()
            {
                users = userManager.Users.ToList(),
                roleNames = roleManager.Roles.Select(x => x.Name).ToList()
            };
            if (model.users.Count > 10)
            {
                model.users = model.users.TakeLast(10).ToList();
            }
            return View(model);
        }
        public async Task<IActionResult> SearchUser(string name)
        {
            UserRoleViewModel model = new()
            {
                users = new List<ApplicationUser>(),
                roleNames = roleManager.Roles.Select(x => x.Name).ToList()
            };
            if (name != null && name != "")
            {
                var user = await userManager.FindByNameAsync(name);
                if (user != null)
                {
                    model.users.Add(user);
                }
                else
                {
                    TempData["error"] = "کاربر مورد نظر یافت نشد";
                }
            }
            else
            {
                TempData["error"] = "مقداری وارد نشده";
            }
            return View(nameof(RoleManagement), model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string id, string roleName)
        {

            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, roleName);
            return Json(new { success = true, message = "کاربر با موفقیت به نقش اضاف شد" });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFromRole(string id, string roleName)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, roleName);
            return Json(new { success = true, message = "کاربر با موفقیت از نقش حذف گردید" });
        }
    }
}
