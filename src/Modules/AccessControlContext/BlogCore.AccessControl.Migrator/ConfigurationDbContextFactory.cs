using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.AccessControl.Migrator
{
    public class ConfigurationDbContextFactory : IDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext Create(DbContextFactoryOptions options)
        {
            var connString = ConfigurationHelper.GetConnectionString(
                options.ContentRootPath, 
                options.EnvironmentName);

            var migrationAssembly = typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly;

            return new ConfigurationDbContext(
                DbContextHelper.BuildDbContextOption<ConfigurationDbContext>(connString, migrationAssembly), 
                new ConfigurationStoreOptions());
        }
    }
}