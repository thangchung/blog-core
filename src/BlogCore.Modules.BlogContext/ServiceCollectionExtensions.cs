using BlogCore.Modules.BlogContext.Usecases;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Common;
using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.BlogContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<RetrieveBlogsRequest, PaginatedItemResponse>, RetrieveBlogsUseCase>();
            services.AddScoped<IUseCase<GetMyBlogsRequest, PaginatedItemResponse>, GetBlogByUserNameUseCase>();
            services.AddScoped<IUseCase<CreateBlogRequest, CreateBlogResponse>, CreateBlogUseCase>();
            return services;
        }
    }
}
