using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationLoginRegister.Contexts;
using WebApplicationLoginRegister.Models;

namespace WebApplicationLoginRegister
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDBContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<AppUser,IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
            var app = builder.Build();

            app.UseStaticFiles();
            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}
