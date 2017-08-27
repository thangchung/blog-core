using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace BlogCore.AccessControlContext.Migrator
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