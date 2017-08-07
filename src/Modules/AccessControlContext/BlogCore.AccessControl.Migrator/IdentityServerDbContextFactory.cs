using System.Reflection;
using BlogCore.AccessControl.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.AccessControl.Migrator
{
    public class IdentityServerDbContextFactory : IDbContextFactory<IdentityServerDbContext>
    {
        public IdentityServerDbContext Create(DbContextFactoryOptions options)
        {
            var connString = ConfigurationHelper.GetConnectionString(
                options.ContentRootPath, 
                options.EnvironmentName);

            var migrationAssembly = typeof(IdentityServerDbContextFactory).GetTypeInfo().Assembly;

            return new IdentityServerDbContext(
                DbContextHelper.BuildDbContextOption<IdentityServerDbContext>(connString, migrationAssembly));
        }
    }
}