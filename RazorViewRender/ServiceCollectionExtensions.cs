using Microsoft.Extensions.DependencyInjection;

namespace RazorViewRender
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorViewRenderService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<RazorViewRenderService>();
        }
    }
}