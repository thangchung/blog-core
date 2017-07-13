using Autofac;
using BlogCore.Post.Infrastructure;
using BlogCore.Post.Presenters.ListOutPostByBlog;

namespace BlogCore.Post.Presenters
{
    public class PostPresenterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PostRepository>()
                .AsImplementedInterfaces();

            builder.RegisterType<ListOutPostByBlogPresenter>()
                .AsSelf();
        }
    }
}