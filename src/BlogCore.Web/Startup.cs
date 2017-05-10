using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Core;
using BlogCore.Infrastructure.Data;
using BlogCore.Infrastructure.Security;
using BlogCore.Web.Blogs;
using FluentValidation.AspNetCore;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace BlogCore.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BlogsAdmin",
                    policyAdmin => { policyAdmin.RequireClaim("role", "blogcore_blogs__admin"); });
                options.AddPolicy("BlogsUser",
                    policyUser => { policyUser.RequireClaim("role", "blogcore_blogs__user"); });
            });

            services.AddMvc()
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssembly(typeof(BlogCoreDbContext).GetTypeInfo().Assembly));

            if (Environment.IsDevelopment())
                services.AddSwaggerGen(options =>
                {
                    options.DescribeAllEnumsAsStrings();
                    options.SwaggerDoc("v1", new Info
                    {
                        Title = "Blog Core",
                        Version = "v1",
                        Description = "Blog Core APIs"
                    });
                    options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                    {
                        Type = "oauth2",
                        Flow = "implicit",
                        TokenUrl = "http://localhost:8483/connect/token",
                        AuthorizationUrl = "http://localhost:8483/connect/authorize",
                        Scopes = new Dictionary<string, string>
                        {
                            {"blogcore_api_blogs", "The Blog APIs"},
                            {"blogcore_api_posts", "The Post APIs"}
                        }
                    });
                });

            services.AddDbContext<BlogCoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddMediatR(
                typeof(EntityBase).GetTypeInfo().Assembly,
                typeof(BlogCoreDbContext).GetTypeInfo().Assembly,
                typeof(Startup).GetTypeInfo().Assembly);

            // security context
            builder.RegisterType<SecurityContextProvider>()
                .As<ISecurityContext>()
                .As<ISecurityContextPrincipal>()
                .InstancePerLifetimeScope();

            // Core & Infra register
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>));

            // Web registers
            builder.RegisterType<BlogPresenter>()
                .AsSelf();

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                AuthenticationScheme = "Bearer",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = "http://localhost:8483",
                // ApiName = "blogcore_api_scope",
                // ApiSecret = "secret",
                SaveToken = true,
                AllowedScopes = new[] { "blogcore_api_blogs", "blogcore_api_posts" },
                RequireHttpsMetadata = false,
                JwtBearerEvents = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidated
                }
            });

            app.UseStaticFiles().UseCors("CorsPolicy");
            app.UseMvc();

            if (env.IsDevelopment())
                app.UseSwagger().UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Core APIs");
                        c.ConfigureOAuth2("swagger", "secret".Sha256(), "swagger", "swagger");
                    });
        }

        private static async Task OnTokenValidated(TokenValidatedContext context)
        {
            // get current principal
            var principal = context.Ticket.Principal;

            // get current claim identity
            var claimsIdentity = context.Ticket.Principal.Identity as ClaimsIdentity;

            // build up the id_token and put it into current claim identity
            var headerToken =
                context.Request.Headers["Authorization"][0].Substring(context.Ticket.AuthenticationScheme.Length + 1);
            claimsIdentity?.AddClaim(new Claim("id_token", headerToken));

            var securityContextInstance = context.HttpContext.RequestServices.GetService<ISecurityContext>();
            var securityContextPrincipal = securityContextInstance as ISecurityContextPrincipal;
            if (securityContextPrincipal == null)
            {
                throw new ViolateSecurityException("Could not initiate the MasterSecurityContextPrincipal object.");
            }
            securityContextPrincipal.Principal = principal;

            await Task.FromResult(0);
        }
    }
}