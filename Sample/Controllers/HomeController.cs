using System.Diagnostics;
using System.Threading.Tasks;
using ch1seL.RazorViewRender;
using Microsoft.AspNetCore.Mvc;
using RazorViewRender.Sample.Models;

namespace RazorViewRender.Sample.Controllers; 

public class HomeController : Controller {
	private readonly IRazorViewRenderService _razorViewRenderService;

	public HomeController(IRazorViewRenderService razorViewRenderService) {
		_razorViewRenderService = razorViewRenderService;
	}

	public IActionResult Index() {
		return View();
	}

	public IActionResult Privacy() {
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() {
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	public async Task<IActionResult> FooBar() {
		string content = await _razorViewRenderService.RenderStringAsync("Test/FooBar",
		                                                                 new FooBarModel {
			                                                                 Foo = "Foo",
			                                                                 Bar = "<b>B</b>ar"
		                                                                 }
		                 );

		return new ContentResult {
			Content = content,
			ContentType = "text/html",
			StatusCode = 200
		};
	}
}