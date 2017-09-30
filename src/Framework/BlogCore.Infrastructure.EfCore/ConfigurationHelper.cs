using Microsoft.Extensions.Configuration;
using System;

namespace BlogCore.Infrastructure.EfCore
{
    public class ConfigurationHelper
    {
        public static string GetConnectionString(string basePath)
        {
            var config = GetConfiguration(basePath);
            var connstr = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connstr))
                throw new InvalidOperationException("Could not find a connection string named '(DefaultConnection)'.");

            if (string.IsNullOrEmpty(connstr))
                throw new InvalidOperationException($"{nameof(connstr)} is null or empty.");

            return connstr;
        }

        private static IConfigurationRoot GetConfiguration(string basePath)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
