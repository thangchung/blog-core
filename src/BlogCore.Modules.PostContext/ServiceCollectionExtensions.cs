using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.PostContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
