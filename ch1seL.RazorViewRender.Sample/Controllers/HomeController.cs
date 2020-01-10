using System.Diagnostics;
using System.Threading.Tasks;
using ch1seL.RazorViewRender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorViewRender.Sample.Models;

namespace RazorViewRender.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RazorViewRenderService _razorViewRenderService;

        public HomeController(ILogger<HomeController> logger, RazorViewRenderService razorViewRenderService)
        {
            _logger = logger;
            _razorViewRenderService = razorViewRenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> FooBar()
        {
            var content = await _razorViewRenderService.RenderStringAsync("Home/FooBar",
                new FooBarModel
                {
                    Foo = "Foo",
                    Bar = "<b>B</b>ar"
                }
            );

            return new ContentResult
            {
                Content = content,
                ContentType = "text/html",
                StatusCode = 200
            };
        }
    }
}