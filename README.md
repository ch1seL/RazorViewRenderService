# ch1seL.RazorViewRenderService

Easy way to render Razor View to string.
It might be use to compose mail message or compose other texts through Razor Views.

## How to use

### Installation

#### Nuget Package Manager Console:

```powershell
Install-Package ch1seL.RazorViewRenderService
```

#### .Net CLI:
```powershell
dotnet add package ch1seL.RazorViewRenderService
```

### Sample using

Create blank ASP.NET Core Web Application MVC:
```powershell 
dotnet new mvc -n RazorViewRender.Sample
```
Install Nuget package
```powershell
dotnet add package ch1seL.RazorViewRenderService
```

Update Startup.cs:
```
...
        public void ConfigureServices(IServiceCollection services)
        {
                ...
                services.AddRazorViewRenderService();          
        }
...
```

Add template model class Models/FooBarModel.cs
```
namespace RazorViewRender.Sample.Models
{
        public class FooBarModel
        {
                public string Foo { get; set; }
                public string Bar { get; set; }
        }
}
```

Add razor template Views/Home/FooBar.cshtml
```
@model RazorViewRender.Sample.Models.FooBarModel

<!DOCTYPE html>

<html>
<head>
    <title>@Model.Foo</title>
</head>
<body>
<div>
    @Html.Raw(Model.Bar)
</div>
</body>
</html>
```

Update Controllers/HomeController.cs
Add RazorViewRenderService as DI
Add FooBar method to compose html-string content
```
        private readonly RazorViewRenderService _razorViewRenderService;

        public HomeController(ILogger<HomeController> logger, RazorViewRenderService razorViewRenderService)
        {
            _logger = logger;
            _razorViewRenderService = razorViewRenderService;
        }

        public async Task<IActionResult> FooBar()
        {
            var content = await _razorViewRenderService.RenderStringAsync("Home/FooBar",
                new FooBarModel
                {
                    Foo = "Foo", 
                    Bar = "<b>B</b>ar"
                });

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
                StatusCode = 200
            };
        }        
```

Run application
```powershell
dotnet run 
```

Open site `https://localhost:5001/Home/FooBar` to get result
