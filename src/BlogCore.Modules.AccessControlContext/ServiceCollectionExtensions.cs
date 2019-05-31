using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.AccessControlContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccessControlModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
