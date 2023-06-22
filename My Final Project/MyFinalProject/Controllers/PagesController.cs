namespace BitLink.Controllers;

public class PagesController : Controller
{
    public IActionResult ProgPage() => View();
    public IActionResult Page2D() => View();
    public IActionResult Page3D() => View();
}