using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.AccessControl.Infrastructure;
using BlogCore.AccessControl.Migrator.DataSeeder;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.AccessControl.Migrator
{
    internal class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start to migration...");

                Console.WriteLine("Initialize the services...");
                RegisterServices();

                Console.WriteLine("Migrate IdentityServer data...");
                InitializeIdentityServer().Wait();

                Console.WriteLine("Migrate User & Role data...");
                InitializeBlogCoreDb().Wait();

                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        private static void RegisterServices()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true);

            builder.AddEnvironmentVariables();
            _configuration = builder.Build();

            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var connString = _configuration.GetConnectionString("DefaultConnection");

            var containerBuilder = new ContainerBuilder();
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<IdentityServerDbContext>(
                options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentityServer()
                .AddConfigurationStore(
                    x => x.UseSqlServer(connString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(
                    x => x.UseSqlServer(connString, options => options.MigrationsAssembly(migrationsAssembly)));

            containerBuilder.Populate(services);
            _serviceProvider = containerBuilder.Build().Resolve<IServiceProvider>();
        }

        private static async Task InitializeIdentityServer()
        {
            using (var serviceScope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                foreach (var resource in IdentityServerSeeder.GetIdentityResources())
                    await context.IdentityResources.AddAsync(resource.ToEntity());

                foreach (var resource in IdentityServerSeeder.GetApiResources())
                    await context.ApiResources.AddAsync(resource.ToEntity());

                foreach (var client in IdentityServerSeeder.GetClients())
                    await context.Clients.AddAsync(client.ToEntity());

                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeBlogCoreDb()
        {
            using (var serviceScope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
                context.Database.Migrate();

                await UserSeeder.Seed(context);
            }
        }
    }
}