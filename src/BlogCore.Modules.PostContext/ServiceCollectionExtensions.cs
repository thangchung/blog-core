using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.PostContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
