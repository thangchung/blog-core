using BlogCore.BlogContext.Infrastructure;
using BlogCore.Infrastructure.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.Migrator
{
    class Program
    {
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
            _serviceProvider = new ServiceCollection().InitMigrationServices((services, config) =>
            {
                var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
                var connString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<BlogDbContext>(
                options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));
            });
        }

        private static async Task InitializeData()
        {
            using (var serviceScope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
                if (context.Database.GetPendingMigrations() != null)
                    context.Database.Migrate();

                await BlogContextSeeder.Seed(context);
            }
        }
    }
}