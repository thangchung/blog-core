using Autofac;
using BlogCore.AccessControl.Infrastructure;

namespace BlogCore.AccessControl.UseCases
{
    public class AccessControlUseCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UserRepository>()
                .AsImplementedInterfaces();
        }
    }
}