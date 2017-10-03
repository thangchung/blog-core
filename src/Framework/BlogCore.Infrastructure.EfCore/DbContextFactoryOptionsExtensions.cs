using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace BlogCore.Infrastructure.EfCore
{
    public static class DbContextFactoryOptionsExtensions
    {
        public static DbContextOptionsBuilder<TDbContext> BuildSqlServerDbContext<TDbContext>(this DbContextOptionsBuilder<TDbContext> dbContextOptionsBuilder, Assembly migrateAssembly)
            where TDbContext : DbContext
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var connstr = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connstr))
                throw new InvalidOperationException("Could not find a connection string named '(DefaultConnection)'.");

            if (string.IsNullOrEmpty(connstr))
                throw new InvalidOperationException($"{nameof(connstr)} is null or empty.");

            var migrationsAssembly = migrateAssembly.GetName().Name;
            dbContextOptionsBuilder.UseSqlServer(connstr, b => b.MigrationsAssembly(migrationsAssembly));
            return dbContextOptionsBuilder;
        }
    }
}