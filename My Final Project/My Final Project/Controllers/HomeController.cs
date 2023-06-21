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
        private readonly SampleContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SampleContext context, ILogger<HomeController> logger)
        {
            this.context = context;
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

        public IActionResult Login() => 
            View(new Admin { ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString() });

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Registration(Person person)
        {
           var newId = await context.Persons.MaxAsync(person => person.Id) +1;
            await context.Persons.AddAsync(person with { Id = newId });
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult ProgPage()
        {
            return View();
        }

        public IActionResult Page3D()
        {
            return View();
        }

        public IActionResult MainPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}   