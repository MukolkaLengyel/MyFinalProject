using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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
    public class RegistrationController : Controller
    {
        private readonly SampleContext _context;
        public RegistrationController(SampleContext context)
        {
            _context = context;
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
            return View();
        }
    }
}
