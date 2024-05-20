using FlightTest.Areas.Identity.Data;
using FlightTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FlightTest.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager,
                                 SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult LoginRegister() => View();
        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.emailAddress);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = model.emailAddress,
                    UserName = model.emailAddress,
                };
                var status = await userManager.CreateAsync(user, model.password);
                await userManager.AddToRoleAsync(user, "customers");
                if (status.Succeeded)
                {
                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    string address = Url.Action("ConfirmEmailToken", "Account", new { id = user.Id, token = token }, "https");
                    string emailBody = $"<div style=\"float:none;margin:auto;width:60%;\">" + $"<div style=\"width: 100%; float:left; box-sizing:border-box;\">" +
                        $"<img style = \"width:100%;height:auto;display:block\" src = \"https://i.ibb.co/P1MQPwK/Bg1.jpg\" alt = \"اجوکمپ\' tabindex = \"0\" >" +
                        $"</div>" +
                        $"<div style = \"background-color: lemonchiffon;direction:rtl;text-align:right;font-family:tahoma!important;line-height:1.5;margin-top:100px;padding: 20px;\" >" +
                        $"<br>" +
                        $"<h3> کاربر عزیز, {user.Email }</h3>" +
                        $"<p> سلام </p>" +
                        $"</div>" +
                        $"<div style = \"direction:rtl;text-align:right;font-family:tahoma!important;line-height:1.5;padding: 20px;\" >درخواستی برای تغییر رمزعبور ارسال شده اگر این درخواست از طرف شما نبوده این ایمیل را نادیده بگیرید در غیر این صورت" +
                        $".<br><br> برای تغییر رمزعبور خود لطفا روی دکمه‌ی زیر کلیک نمایید:" +
                        $"</div>" +
                        $"<center>" +
                        $"<a href ={address} style = \"background-color:#d74c00;border:1px solid #d74c00;border-radius:6px;color:white!important;display:inline-block;font-family:tahoma!important;font-size:16px!important;padding:10px;margin:15px 0;text-decoration:none\" target = \"_blank\" >" +
                        $"تایید ایمیل</a>" +
                        $"</center> " + $"</div>";
                    MailMessage mailMessage = new MailMessage("sinajangjoomvc@gmail.com", user.Email);
                    mailMessage.Subject = "Confirm Your Email";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = emailBody;
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("sinajangjoomvc@gmail.com", "qhkmumfgxwkniumd");
                    try
                    {
                        smtpClient.Send(mailMessage);
                        TempData["msg"] = "sent";
                    }
                    catch (Exception ex)
                    {
                        TempData["msg"] = "not sent";
                    }
                    TempData["msg"] = "حساب شما با موفقیت ایجاد شد، ایمیل تایید حساب برای شما ارسال گردید";
                }
                else
                {
                    TempData["msg"] = "ایجاد حساب با خطا مواجه شد";

                }
            }
            else
            {
                TempData["msg"] = "این حساب کاربری قبلا ایجاد شده";

            }
            return RedirectToAction("LoginRegister", "Account");
        }

        public async Task<IActionResult> ConfirmEmailToken(string id, string token)
        {
            var user = await userManager.FindByIdAsync(id);
            var status = await userManager.ConfirmEmailAsync(user, token);
            if (status.Succeeded)
            {
                TempData["msg"] = "حساب کاربری با موفقیت تایید شد";
            }
            else
            {

                TempData["msg"] = "خطا در تایید حساب کاربری";
            }
            return RedirectToAction(nameof(LoginRegister));
        }


        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.emailAddress);
            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.password, model.rememberMe, true);
                if (result.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(user, "admins"))
                    {
                        TempData["msg"] = "شما با موفقیت وارد سایت شدید";
                        return RedirectToAction("Index", "Admin");
                    }
                    TempData["msg"] = "شما با موفقیت وارد سایت شدید";
                    return RedirectToAction("index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    TempData["isLockOut"] = true;
                    TempData["msg"] = "تعداد دفعات ورود رمز اشتباه بیش از حد مجاز است ، حساب شما موقتا بسته شد. زمان انتظار: ";
                }
                else
                {
                    TempData["msg"] = "نام کاربری یا رمز عبور اشتباه است";

                }
            }
            else
            {
                TempData["msg"] = "نام کاربری یا رمز عبور اشتباه است";

            }
            return RedirectToAction("LoginRegister", "Account");
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CheckEmail(string emailAddress)
        {
            ApplicationUser user = await userManager.FindByNameAsync(emailAddress);
            if (user == null)
                return Json(true);
            else return Json(false);
        }
        public IActionResult notFound() => View();

    }
}
