using Microsoft.Extensions.DependencyInjection;

namespace ch1seL.RazorViewRender
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorViewRenderService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IRazorViewRenderService, RazorViewRenderService>();
        }
    }
}