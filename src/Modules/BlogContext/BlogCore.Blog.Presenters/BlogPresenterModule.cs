using Autofac;
using BlogCore.Blog.Presenters.CreateBlog;
using BlogCore.Blog.Presenters.GetBlog;
using BlogCore.Blog.Presenters.ListOutBlog;

namespace BlogCore.Blog.Presenters
{
    public class BlogPresenterModule : Module
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