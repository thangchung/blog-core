using System;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BlogCore.MigrationConsole
{
    public class ConfigurationDbContextFactory : IDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(options.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{options.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var connstr = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connstr))
                throw new InvalidOperationException("Could not find a connection string named '(DefaultConnection)'.");

            if (string.IsNullOrEmpty(connstr))
                throw new InvalidOperationException($"{nameof(connstr)} is null or empty.");

            var migrationsAssembly = typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly.GetName().Name;
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsBuilder.UseSqlServer(connstr, b => b.MigrationsAssembly(migrationsAssembly));

            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}