using BlogCore.Infrastructure.EfCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace BlogCore.AccessControlContext.Migrator
{
    public class PersistedGrantDbContextFactory : IDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext Create(DbContextFactoryOptions options)
        {
            var connString = ConfigurationHelper.GetConnectionString(
                options.ContentRootPath,
                options.EnvironmentName);

            var migrationAssembly = typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly;

            return new PersistedGrantDbContext(
                DbContextHelper.BuildDbContextOption<PersistedGrantDbContext>(connString, migrationAssembly),
                new OperationalStoreOptions());
        }
    }
}