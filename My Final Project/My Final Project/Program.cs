using BitLink.Logic;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using BitLink.Dao;
using BitLink.Logic;

namespace BitLink
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
            var isDevelopment = webApplicationBuilder.Environment.IsDevelopment();
            // Add services to the container.
            webApplicationBuilder.Services.AddControllersWithViews();

            webApplicationBuilder.Services.AddDbContext<SampleContext>();

            webApplicationBuilder.Services.AddDbContext<SampleContext>
            (builder => builder.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));

            var app = webApplicationBuilder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}