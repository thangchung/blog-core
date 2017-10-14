using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Reflection;

namespace BlogCore.Migrator
{
    public class IdentityServerDbContextFactory : IDesignTimeDbContextFactory<IdentityServerDbContext>
    {
        public IdentityServerDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConnectionString(Directory.GetCurrentDirectory());
            var migrationAssembly = typeof(IdentityServerDbContextFactory).GetTypeInfo().Assembly;

            return new IdentityServerDbContext(
                DbContextHelper.BuildDbContextOption<IdentityServerDbContext>(connString, migrationAssembly));
        }
    }
}