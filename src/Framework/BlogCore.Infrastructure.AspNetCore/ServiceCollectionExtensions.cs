using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Infrastructure.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider InitServices(this IServiceCollection services, Assembly[] registeredAssemlies, IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();

            // register the global maindb's connection string
            builder.RegisterInstance(configuration.GetConnectionString("MainDb"))
                .Named<string>("MainDbConnectionString");

            // core & infra register
            builder.RegisterGeneric(typeof(EfRepository<,>))
                .As(typeof(IEfRepository<,>));

            // scan modules in other assemblies
            builder.RegisterAssemblyModules(registeredAssemlies);

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        public static IServiceProvider InitMigrationServices(this IServiceCollection services,
            Action<IServiceCollection, IConfigurationRoot> additionalWorks = null,
            string envName = "ASPNETCORE_ENVIRONMENT")
        {
            var builder = new ContainerBuilder();

            var environmentName = envName ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ;
            var configuration = Directory.GetCurrentDirectory().BuildConfiguration(environmentName);
            var connString = configuration.GetConnectionString("DefaultConnection");

            additionalWorks?.Invoke(services, configuration);

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }
    }
}