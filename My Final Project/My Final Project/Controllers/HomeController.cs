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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BitLink.Controllers
{
    public class HomeController : Controller
    {
        //private readonly samplecontext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SampleContext context, ILogger<HomeController> logger)
        {
            //_context = context;
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

        ////Register the user
        //public IActionResult Registration()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Registration(Person person)
        //{
        //    var newId = await _context.Persons.AnyAsync() ? await _context.Persons.MaxAsync(p => p.Id) + 1 : 1;
        //    await _context.Persons.AddAsync(person with { Id = newId });
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        ////If user is registered, he can login
        //public async Task<IActionResult> Login(Admin admin)
        //{
        //    //var hashedPassword = LoginExtensions.HashPassword(admin.Pass);
        //    //var dbAdmin = _context.Admin.FirstOrDefault();
        //    //if (dbAdmin == null) return RedirectToAction("Login");
        //    //await LoginExtensions.SignIn(HttpContext, dbAdmin);
        //    //if (!string.IsNullOrWhiteSpace(admin.ReturnUrl) && Url.IsLocalUrl(admin.ReturnUrl))
        //    //    return Redirect(admin.ReturnUrl);
        //    return RedirectToAction("Index");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}   