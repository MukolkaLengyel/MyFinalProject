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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Person = BitLink.Dao.Person;
using Persons = BitLink.Dao.Persons;

namespace BitLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly SampleContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SampleContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet, LoginFilter]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Registration");
        }

        //Register the user
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Person person)
        {
            var newId = await _context.Persons.AnyAsync() ? await _context.Persons.MaxAsync(p => p.Id) + 1 : 1;
            await _context.Persons.AddAsync(person with { Id = newId });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Login the user
        public IActionResult Login()
        {
            return View(new Persons(HttpContext.Request.Query["ReturnUrl"]!));
        }


        ///With Persons Role
        [HttpPost]
        public async Task<IActionResult> Login(Persons persons)
        {
            var dbPerson = _context.Persons
                .FirstOrDefault(u =>
                u.Username == u.Username && u.Password == u.Password);

            if (dbPerson == null) return RedirectToAction("Login");
            await HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new (ClaimTypes.Name, dbPerson.Username),
                        new (ClaimTypes.Role, dbPerson.Role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme)));

            if (!string.IsNullOrWhiteSpace(persons.ReturnUrl) && Url.IsLocalUrl(persons.ReturnUrl))
            {
                return Redirect(persons.ReturnUrl);
            }
            return RedirectToAction("MainPage");
        }

        // logout the user
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}   