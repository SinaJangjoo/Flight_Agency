using System;
using System.Threading.Tasks;
using FlightTest.Areas.Identity.Data;
using FlightTest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FlightTest.Areas.Identity.IdentityHostingStartup))]
namespace FlightTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DBFlightTest>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DBFlightTestConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DBFlightTest>();
                services.Configure<IdentityOptions>(x =>
                {
                    x.Lockout.MaxFailedAccessAttempts = 3;
                    x.Lockout.AllowedForNewUsers = true;
                    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

                    x.Password.RequireDigit = true;
                    x.Password.RequiredLength = 5;
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                });
                services.ConfigureApplicationCookie(x =>
                {
                    x.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = y =>
                        {
                            y.Response.Redirect("/Account/notFound");
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = y =>
                        {
                            y.Response.Redirect("/Account/notFound");
                            return Task.CompletedTask;
                        }
                    };
                });
                services.AddAuthorization(x =>
                {
                    x.AddPolicy("AdminsPolicy", p => p.RequireRole("admins"));

                });
            });
        }
    }
}