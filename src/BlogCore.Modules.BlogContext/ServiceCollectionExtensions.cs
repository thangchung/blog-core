using BlogCore.Modules.BlogContext.Usecases;
using BlogCore.Shared.v1.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.BlogContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogModule(this IServiceCollection services)
        {
            services.AddTransient<GetMyBlogsRequestValidator>();
            services.AddTransient<GetBlogByUsernameInteractor>();
            return services;
        }
    }
}
