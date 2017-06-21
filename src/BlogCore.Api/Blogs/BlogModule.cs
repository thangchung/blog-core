using Autofac;
using BlogCore.Api.Blogs.CreateBlog;
using BlogCore.Api.Blogs.GetBlog;
using BlogCore.Api.Blogs.ListOutBlogs;

namespace BlogCore.Api.Blogs
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