using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BitLink.Logic;
using BitLink.UserInfoDao;

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
        return RedirectToAction("Index");
    }

    //Login the user
    public IActionResult Login() => View(new Person(HttpContext.Request.Query["ReturnUrl"]!));

    ///With Persons Role
    [HttpPost]
    public async Task<IActionResult> Login(Persons persons)
    {
        var dbPerson = _context.Persons
            .FirstOrDefault(person =>
                person.Username == persons.Login && person.Password == persons.Pass);

        if (dbPerson == null) return RedirectToAction("Login");
        await HttpContext.SignInAsync(new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new(ClaimTypes.Name, dbPerson.Username),
                    new(ClaimTypes.Role, dbPerson.Role)
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
        {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}