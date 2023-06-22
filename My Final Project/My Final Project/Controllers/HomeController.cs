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
        //[HttpPost]
        //public async Task<IActionResult> Login(Persons persons)
        //{
        //    var dbUser = _context.Persons
        //        .FirstOrDefault(u =>
        //        u.Username == u.Username && u.Password == u.Password);

        //    if (dbUser == null) return RedirectToAction("Login");
        //    await HttpContext.SignInAsync(new ClaimsPrincipal(
        //        new ClaimsIdentity(
        //            new Claim[]
        //            {
        //                new Claim(ClaimTypes.Name, dbUser.Username),
        //                new Claim(ClaimTypes.Role, dbUser.Role)
        //            }, CookieAuthenticationDefaults.AuthenticationScheme)));

        //    if (!string.IsNullOrWhiteSpace(User.ReturnUrl) && Url.IsLocalUrl(User.ReturnUrl))
        //    {
        //        return Redirect(User.ReturnUrl);
        //    }
        //    return RedirectToAction("MainPage");
        //}

        //With Admin Role
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var dbAdmin = _context.Admin.FirstOrDefault(a =>
                a.Login == admin.Login && a.Pass == admin.Pass);
            if (dbAdmin == null) return RedirectToAction("Login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(
                    new List<Claim>
                    {
                    new(ClaimTypes.Name, dbAdmin.Login),
                    new(ClaimTypes.Role, dbAdmin.Role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme)));
            if (!string.IsNullOrWhiteSpace(admin.ReturnUrl) && Url.IsLocalUrl(admin.ReturnUrl))
                return Redirect(admin.ReturnUrl);
            return RedirectToAction("Index");
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