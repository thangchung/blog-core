using BlogCore.Infrastructure.EfCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Reflection;

namespace BlogCore.AccessControlContext.Migrator
{
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConnectionString(Directory.GetCurrentDirectory());
            var migrationAssembly = typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly;

            return new PersistedGrantDbContext(
                DbContextHelper.BuildDbContextOption<PersistedGrantDbContext>(connString, migrationAssembly),
                new OperationalStoreOptions());
        }
    }
}