using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Blog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Blog.Migrator
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        internal static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Register services...");
                RegisterServices();

                Console.WriteLine("Migrate data...");
                InitializeData().Wait();
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

            services.AddDbContext<BlogDbContext>(
                options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));

            containerBuilder.Populate(services);
            _serviceProvider = containerBuilder.Build().Resolve<IServiceProvider>();
        }

        private static async Task InitializeData()
        {
            using (var serviceScope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
                context.Database.Migrate();

                await BlogContextSeeder.Seed(context);
            }
        }
    }
}
