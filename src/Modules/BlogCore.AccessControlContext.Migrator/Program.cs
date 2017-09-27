using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.AccessControlContext.Migrator.DataSeeder;
using BlogCore.Infrastructure.AspNetCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Migrator
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

                Console.WriteLine("Migrate IdentityServer data...");
                InitializeIdentityServer().Wait();

                Console.WriteLine("Migrate User & Role data...");
                InitializeBlogCoreDb().Wait();
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
            });
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