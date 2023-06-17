using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using BitLink.Dao;
using BitLink.Logic;
using BitLink.Models;
using Person = BitLink.Dao.Person;

namespace BitLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly SampleContext Context;
        //public HomeController(SampleContext context) => Context = context; <-- Making an error situation

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void SampleDbContext(IServiceCollection services)
        {
            services.AddDbContext<SampleContext>();
            services.AddControllers();
            services.AddTransient<SampleContext>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        //Still doesen't work
        [HttpPost]
        public IActionResult Registration(Person person)
        {

            //using (var ctx = new SampleContext())
            //{
            //    ctx.Persons.Add(new Person()
            //    {
            //        Id = person.Id,
            //        FirstName = person.FirstName,
            //        LastName = person.LastName,
            //        Username = person.Username,
            //        Password = person.Password,
            //        Age = person.Age,
            //        Gender = person.Gender

            //    });

            //    ctx.SaveChanges();
            //}
            //return RedirectToAction("Index");


            using var context = new SampleContext();
            var newId = context.Persons.Max(person => person.Id) + 1;
            context.Persons.Add(person with { Id = newId });
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult MainPage()
        {
            return View();
        }

        /*public ActionResult RedirectToOtherPage()
        {
            return RedirectToAction("MainPage");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}   