using BitLink.Logic;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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
            (builder => builder.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("SampleDb")));

            var app = webApplicationBuilder.Build();

            //database
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
                .AddViewOptions(Options =>
            {
                Options.HtmlHelperOptions.ClientValidationEnabled = true;
                Options.HtmlHelperOptions.Html5DateRenderingMode = 
                    Microsoft.AspNetCore.Mvc.Rendering.Html5DateRenderingMode.CurrentCulture;
            })
                .AddDataAnnotationsLocalization()
                .AddMvcLocalization()
                .Services
                // needed for localization and validation
                .AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

            // register the database context
            builder.Services.AddDbContext<SampleContext>();
            var App = builder.Build();



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