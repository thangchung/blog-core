using BlogCore.Modules.PostContext.Usecases;
using BlogCore.Shared.v1.Post;
using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.PostContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<GetPostsByBlogRequest, GetPostsByBlogResponse>, GetPostsByBlogUseCase>();
            return services;
        }
    }
}
