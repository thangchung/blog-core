using BlogCore.Modules.PostContext.Domain;
using BlogCore.Modules.PostContext.Services;
using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.PostContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Scan(s => 
                s.FromCallingAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(IUseCase<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddScoped<ITagService, TagService>();
            return services;
        }
    }
}
