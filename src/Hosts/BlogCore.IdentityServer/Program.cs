using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace IdentityServer4.Quickstart.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer4";
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Warning);
                    builder.AddFilter("IdentityServer4", LogLevel.Debug);
                })
                .UseKestrel()
                .UseUrls("http://localhost:8483")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
