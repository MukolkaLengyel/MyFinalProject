using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BitLink.Controllers
{
    public class PagesController : Controller
    {
     
        public IActionResult ProgPage()
        {
            return View();
        }


        public IActionResult Page3D()
        {
            return View();
        }
    }
    
}   