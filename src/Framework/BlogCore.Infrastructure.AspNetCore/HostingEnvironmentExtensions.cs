using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot BuildConfiguration(this IHostingEnvironment env,
            string appSettingFileName = "appsettings.json")
        {
            return BuildConfiguration(env.ContentRootPath);
        }

        public static IConfigurationRoot BuildConfiguration(this string contentRoot,
            string envName = "ASPNETCORE_ENVIRONMENT", 
            string appSettingFileName = "appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile(appSettingFileName, false, true)
                .AddJsonFile($"appsettings.{envName}.json", true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}