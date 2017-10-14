using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.PostContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace BlogCore.Migrator
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

                Console.WriteLine("Migrate IdentityServer data & User & Role data...");
                AccessControlContext.Migrator.Migrator.Run(_serviceProvider).Wait();

                Console.WriteLine("Migrate blog data...");
                BlogContext.Migrator.Migrator.Run(_serviceProvider).Wait();

                Console.WriteLine("Migrate post data...");
                PostContext.Migrator.Migrator.Run(_serviceProvider).Wait();
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

                // Access Control context
                services.AddDbContext<IdentityServerDbContext>(
                options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));
                services.AddIdentityServer()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(connString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(connString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));
                    });

                // Blog context
                services.AddDbContext<BlogDbContext>(
                    options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));

                // Post context
                services.AddDbContext<PostDbContext>(
                    options => options.UseSqlServer(connString, b => b.MigrationsAssembly(migrationsAssembly)));
            });
        }
    }
}
