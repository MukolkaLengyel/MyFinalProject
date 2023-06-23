using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BitLink.Logic;
using BitLink.UserInfoDao;
using Microsoft.AspNetCore.Authorization;

namespace BitLink.Controllers;

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
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();
    public IActionResult MainPage() => View();

    public async Task<IActionResult> LogOut()
    {
        _logger.LogInformation("Logout");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
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
        await _context.Persons.AddAsync(person with {Id = newId});
        await _context.SaveChangesAsync();
        return RedirectToAction("MainPage");
    }

    //Login the user
    public IActionResult Login() => View(new Person(HttpContext.Request.Query["ReturnUrl"]!));

    ///With Persons Role
    [HttpPost]
    public async Task<IActionResult> Login(Person person)
    {
        //InvalidCastException: Unable to cast object of type 'System.String' to type 'System.Int32'.
        var dbPerson = _context.Persons
            .FirstOrDefault(p =>
                p.Username == person.Username && p.Password == person.Password);
        if (dbPerson == null) return RedirectToAction("MainPage");
        await HttpContext.SignInAsync(new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new(ClaimTypes.Name, dbPerson.Username),
                    new(ClaimTypes.Role, dbPerson.Password)
                }, CookieAuthenticationDefaults.AuthenticationScheme)));
        return !string.IsNullOrWhiteSpace(person.ReturnUrl) && Url.IsLocalUrl(person.ReturnUrl)
            ? Redirect(person.ReturnUrl)
            : RedirectToAction("MainPage");
    }

    // logout the user
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
        {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}