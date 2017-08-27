using Autofac;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;

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
        }
    }
}