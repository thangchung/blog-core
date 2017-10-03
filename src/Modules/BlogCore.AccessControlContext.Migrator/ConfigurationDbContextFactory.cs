using BlogCore.Infrastructure.EfCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Reflection;

namespace BlogCore.AccessControlContext.Migrator
{
    /// <summary>
    /// Reference at https://github.com/aspnet/Announcements/issues/258
    /// </summary>
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConnectionString(Directory.GetCurrentDirectory());
            var migrationAssembly = typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly;

            return new ConfigurationDbContext(
                DbContextHelper.BuildDbContextOption<ConfigurationDbContext>(connString, migrationAssembly),
                new ConfigurationStoreOptions());
        }
    }
}