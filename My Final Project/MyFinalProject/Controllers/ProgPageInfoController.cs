using BitLink.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BitLink.Controllers
{
    public class ProgPageInfoController : Controller
    {
        private readonly SampleContext _blogPost;
        public ProgPageInfoController(SampleContext blogPost)
        {
            blogPost = _blogPost;
        }

        // Programming
        public IActionResult CSInfo() => View();
        public IActionResult HtmlInfo() => View();
        public IActionResult JavaInfo() => View();
        public IActionResult Python() => View();
        public IActionResult AddContent() => View();

        //3D modelling
        public IActionResult Page3DInfo() => View();
        
        //Here is where you add your content
        [HttpPost]
        public IActionResult AddContent(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _blogPost.BlogPosts.Add(blogPost);
                _blogPost.SaveChanges();
                return RedirectToAction("MainPage", "Home");
            }

            return View(blogPost);
        }
    }
}
