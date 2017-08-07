using Microsoft.Extensions.Configuration;
using System;

namespace BlogCore.Infrastructure.EfCore
{
    public class ConfigurationHelper
    {
        public static string GetConnectionString(string basePath, string environmentName)
        {
            var config = GetConfiguration(basePath, environmentName);
            var connstr = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connstr))
                throw new InvalidOperationException("Could not find a connection string named '(DefaultConnection)'.");

            if (string.IsNullOrEmpty(connstr))
                throw new InvalidOperationException($"{nameof(connstr)} is null or empty.");

            return connstr;
        }

        private static IConfigurationRoot GetConfiguration(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
