using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.BlogContext
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

            services.Decorate(typeof(IUseCase<,>), typeof(ValidUseCaseDecorator<,>));

            return services;
        }
    }
}
