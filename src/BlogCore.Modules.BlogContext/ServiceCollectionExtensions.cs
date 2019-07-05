using BlogCore.Modules.BlogContext.Usecases;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.BlogContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogModule(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<GetMyBlogsRequest, PaginatedBlogDto>, GetBlogByUsernameInteractor>();
            return services;
        }
    }
}
