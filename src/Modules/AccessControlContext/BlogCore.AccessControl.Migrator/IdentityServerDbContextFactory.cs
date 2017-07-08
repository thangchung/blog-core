using System;
using System.Reflection;
using BlogCore.AccessControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BlogCore.AccessControl.Migrator
{
    public class IdentityServerDbContextFactory : IDbContextFactory<IdentityServerDbContext>
    {
        public IdentityServerDbContext Create(DbContextFactoryOptions options)
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

            var migrationsAssembly = typeof(IdentityServerDbContextFactory).GetTypeInfo().Assembly.GetName().Name;
            var optionsBuilder = new DbContextOptionsBuilder<IdentityServerDbContext>();
            optionsBuilder.UseSqlServer(connstr, b => b.MigrationsAssembly(migrationsAssembly));

            return new IdentityServerDbContext(optionsBuilder.Options);
        }
    }
}