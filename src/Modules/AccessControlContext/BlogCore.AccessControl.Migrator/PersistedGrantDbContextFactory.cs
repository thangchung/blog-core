using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.AccessControl.Migrator
{
    public class PersistedGrantDbContextFactory : IDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext Create(DbContextFactoryOptions options)
        {
            var connString = ConfigurationHelper.GetConnectionString(
                options.ContentRootPath,
                options.EnvironmentName);

            var migrationAssembly = typeof(PersistedGrantDbContext).GetTypeInfo().Assembly;

            return new PersistedGrantDbContext(
                DbContextHelper.BuildDbContextOption<PersistedGrantDbContext>(connString, migrationAssembly),
                new OperationalStoreOptions());
        }
    }
}