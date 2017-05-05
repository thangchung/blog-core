using System;
using System.IO;
using BlogCore.Infrastructure.Data;
using IdentityServer4;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BlogCore.IdentityServer
{
    public class Startup
    {
        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment environment)
        {
            Environment = environment;

            var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(@"identityserver4_log.txt")
                .WriteTo.LiterateConsole(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}");

            loggerFactory
                .WithFilter(new FilterLoggerSettings
                {
                    {"IdentityServer4", LogLevel.Debug},
                    {"Microsoft", LogLevel.Warning},
                    {"System", LogLevel.Warning}
                })
                .AddSerilog(serilog.CreateLogger());

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IHostingEnvironment Environment { get; }
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BlogCoreDbContext>(options =>
                options.UseSqlServer(connString));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddRoleStore<ExtendedRoleStore>()
                .AddUserStore<ExtendedUserStore>()
                .AddDefaultTokenProviders()
                .AddIdentityServerUserClaimsPrincipalFactory();

            services.AddMvc();

            services.AddIdentityServer(SetIdentityServerOptions)
                .AddTemporarySigningCredential()
                .AddConfigurationStore(x => x.UseSqlServer(connString))
                .AddOperationalStore(x => x.UseSqlServer(connString))
                .AddAspNetIdentity<AppUser>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme,
                CookieName = "blogcore.auth",
                ExpireTimeSpan = TimeSpan.FromMinutes(20),
                SlidingExpiration = true,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseIdentity();
            app.UseIdentityServer();

            // middleware for google authentication
            // must use http://localhost:5000 for this configuration to work
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com",
                ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private static void SetIdentityServerOptions(IdentityServerOptions identityServerOptions)
        {
            identityServerOptions.Authentication.AuthenticationScheme =
                IdentityServerConstants.DefaultCookieAuthenticationScheme;
        }
    }
}