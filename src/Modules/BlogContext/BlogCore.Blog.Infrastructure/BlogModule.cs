using Autofac;
using BlogCore.Blog.Infrastructure.UseCases.CreateBlog;
using BlogCore.Blog.Infrastructure.UseCases.GetBlog;
using BlogCore.Blog.Infrastructure.UseCases.ListOutBlog;

namespace BlogCore.Blog.Infrastructure
{
    public class BlogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CreateBlogPresenter>()
                .AsSelf();

            builder.RegisterType<GetBlogPresenter>()
                .AsSelf();

            builder.RegisterType<ListOfBlogPresenter>()
                .AsSelf();
        }
    }
}