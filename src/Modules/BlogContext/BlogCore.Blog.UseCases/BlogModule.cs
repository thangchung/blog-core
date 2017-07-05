using Autofac;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;

namespace BlogCore.Blog.UseCases
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