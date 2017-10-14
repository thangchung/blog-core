using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.AccessControlContext.Migrator.DataSeeder;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Migrator
{
    public class Migrator
    {
        public static async Task Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
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

                // migrate the data for our identity server
                var identityServerContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
                identityServerContext.Database.Migrate();

                await UserSeeder.Seed(identityServerContext);
            }
        }
    }
}
