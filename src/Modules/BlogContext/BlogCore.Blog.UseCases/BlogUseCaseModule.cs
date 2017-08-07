using Autofac;
using BlogCore.Blog.Infrastructure;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases
{
    public class BlogUseCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x =>
                DbContextHelper.BuildDbContext<BlogDbContext>(
                    x.ResolveKeyed<string>("MainDbConnectionString")))
                .SingleInstance();
        }
    }
}