using BlogCore.Modules.BlogContext.Usecases;
using BlogCore.Modules.BlogContext.Usecases.Shared;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Presenter;
using BlogCore.Shared.v1.Usecase;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Modules.BlogContext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<RetrieveBlogsRequest, PaginatedBlogResponse>, RetrieveBlogsUseCase>();
            services.AddScoped<IUseCase<GetMyBlogsRequest, PaginatedBlogResponse>, GetBlogByUserNameUseCase>();
            services.AddScoped<IApiPresenter<PaginatedBlogResponse>, PaginatedBlogPresenter>();

            services.AddScoped<IUseCase<CreateBlogRequest, CreateBlogResponse>, CreateBlogUseCase>();
            services.AddScoped<IApiPresenter<CreateBlogResponse>, CreateBlogResponsePresenter>();

            return services;
        }
    }
}
