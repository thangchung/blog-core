using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot BuildConfiguration(this IHostingEnvironment env,
            string appSettingFileName = "appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(appSettingFileName, false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}