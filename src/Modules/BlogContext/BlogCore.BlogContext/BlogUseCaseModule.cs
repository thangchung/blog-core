using Autofac;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.BlogContext.UseCases.BasicCrud;
using BlogCore.BlogContext.UseCases.GetBlogsByUserName;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Infrastructure.UseCase;
using FluentValidation;

namespace BlogCore.BlogContext
{
    public class BlogUseCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x =>
                DbContextHelper.BuildDbContext<BlogDbContext>(
                    x.ResolveKeyed<string>("MainDbConnectionString")));
                // .SingleInstance();

            builder.RegisterType<CreateBlogRequestValidator>()
                .As<IValidator<CreateBlogRequest>>();
            builder.RegisterType<UpdateBlogRequestValidator>()
                .As<IValidator<UpdateBlogRequest>>();
            builder.RegisterType<DeleteBlogRequestValidator>()
                .As<IValidator<DeleteBlogRequest>>();

            builder.RegisterType<BasicCrudInteractor>()
                .As<IAsyncUseCaseRequestHandler<CreateBlogRequest, CreateBlogResponse>>()
                .As<IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>>>()
                .As<IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse>>()
                .As<IAsyncUseCaseRequestHandler<UpdateBlogRequest, UpdateBlogResponse>>()
                .As<IAsyncUseCaseRequestHandler<DeleteBlogRequest, DeleteBlogResponse>>();

               builder.RegisterType<GetBlogsByUserNameInteractor>()
                .AsSelf();
        }
    }
}