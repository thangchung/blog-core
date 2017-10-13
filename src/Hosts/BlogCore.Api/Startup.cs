#region libs

using BlogCore.AccessControl;
using BlogCore.AccessControlContext.Domain;
using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.Api.Features.Posts.ListOutPostByBlog;
using BlogCore.BlogContext;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServerWithAspNetIdentity.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace BlogCore.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = env.BuildConfiguration();
            Environment = env;

            // https://github.com/dotnet/corefx/issues/9158
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // clear any handler for JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<IdentityServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policyAdmin => { policyAdmin.RequireClaim("role", "admin"); });
                options.AddPolicy("User",
                    policyUser => { policyUser.RequireClaim("role", "user"); });
            });*/

            var mvcBuilder = services.AddMvc();
            foreach (var assembly in RegisteredAssemblies())
            {
                // register controllers
                mvcBuilder.AddApplicationPart(assembly);

                // register validations
                mvcBuilder.AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssembly(assembly));
            }

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<AppUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("MainDb"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("MainDb"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddConfigurationStoreCache()
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            services.AddOptions()
                .Configure<PagingOption>(Configuration.GetSection("Paging"));

            if (Environment.IsDevelopment())
            {
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
                        Flow = "implicit", // "password",
                        TokenUrl = "http://localhost:8484/connect/token",
                        AuthorizationUrl = "http://localhost:8484/connect/authorize",
                        Scopes = new Dictionary<string, string>
                    {
                        {"blogcore_api_scope", "The Blog APIs"}
                    }
                    });
                });
            }

            services.AddMediatR(RegisteredAssemblies());

            services.AddAuthentication()
             .AddIdentityServerAuthentication(o =>
             {
                 o.Authority = "http://localhost:8484";
                 o.RequireHttpsMetadata = !Environment.IsDevelopment();
                 o.ApiName = "blogcore_api_resource";
                 o.SupportedTokens = SupportedTokens.Both;
                 o.RequireHttpsMetadata = false;
                 o.EnableCaching = true;
                 o.CacheDuration = TimeSpan.FromMinutes(10); //default
             });

            // register presenters
            services.AddScoped<ListOutPostByBlogPresenter>();

            return services.InitServices(RegisteredAssemblies(), Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            app.UseAuthentication()
                .UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        var maxAge = new TimeSpan(7, 0, 0, 0);
                        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                            "public,max-age=" + maxAge.TotalSeconds.ToString("0");
                    }
                });

            app.UseIdentityServer();

            app.UseMiddleware<BlogAuthenticationMiddleware>();

            app.UseCors("CorsPolicy")
                .UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseSwagger().UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Core APIs");
                        c.ConfigureOAuth2("swagger", "secret".Sha256(), "swagger", "swagger");
                    });
            }
        }

        private static Assembly[] RegisteredAssemblies()
        {
            return new[]
            {
                typeof(BlogUseCaseModule).GetTypeInfo().Assembly,
                typeof(PostUseCaseModule).GetTypeInfo().Assembly,
                typeof(AccessControlUseCaseModule).GetTypeInfo().Assembly,
                typeof(Startup).GetTypeInfo().Assembly
            };
        }
    }

    public class BlogAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BlogAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.User != null && context.User.Identity.IsAuthenticated)
            {
                // get current claim identity
                var claimsIdentity = context.User.Identity as ClaimsIdentity;

                var securityContextInstance = context.RequestServices.GetService<ISecurityContext>();
                var securityContextPrincipal = securityContextInstance as ISecurityContextPrincipal;
                if (securityContextPrincipal == null)
                    throw new ViolateSecurityException("Could not initiate the MasterSecurityContextPrincipal object.");
                securityContextPrincipal.Claims = claimsIdentity;

                var blogRepoInstance = context.RequestServices.GetService<IEfRepository<BlogDbContext, BlogContext.Domain.Blog>>();
                var email = securityContextInstance.GetCurrentEmail();
                var blogs = await blogRepoInstance.ListAsync();
                var blog = blogs.FirstOrDefault(x => x.OwnerEmail == email);
                securityContextPrincipal.SetBlog(blog);
            }

            // Call the next delegate/middleware in the pipeline
            await _next.Invoke(context);
        }
    }
}