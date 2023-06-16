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
        private readonly ILogger<HomeController> _logger;
        private readonly SampleContext _context;
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

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Person person)
        {
            var newId = _context.Persons.Max(p => p.Id) + 1;
            _context.Persons.Add(person with { Id = newId });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Tutorials()
        {
            return View();
        }

        /*public ActionResult RedirectToOtherPage()
        {
            return RedirectToAction("MainPage");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}