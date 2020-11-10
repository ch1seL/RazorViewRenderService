using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace ch1seL.RazorViewRender
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorViewRenderService(this IServiceCollection serviceCollection, string viewsPath = null)
        {
            if (viewsPath != null)
            {
                serviceCollection.Configure<RazorViewEngineOptions>(options => {
                    options.ViewLocationExpanders.Add(new ViewLocationExpander(viewsPath));
                });    
            }
            
            return serviceCollection.AddScoped<IRazorViewRenderService, RazorViewRenderService>();
        }
    }
}