using Autofac;
using BlogCore.Api.Features.Posts.ListOutPostByBlog;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;
using BlogCore.PostContext.UseCases.ListOutPostByBlog;

namespace BlogCore.PostContext
{
    public class PostUseCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x =>
                DbContextHelper.BuildDbContext<PostDbContext>(
                    x.ResolveKeyed<string>("MainDbConnectionString")))
                .SingleInstance();

            builder.RegisterType<ListOutPostByBlogInteractor>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ListOutPostByBlogPresenter>()
                .AsSelf()
                .SingleInstance();
        }
    }
}