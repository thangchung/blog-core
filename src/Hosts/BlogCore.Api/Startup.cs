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
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            // clear any handler for JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<IdentityServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
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
                .AddAspNetIdentity<AppUser>()
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

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
                        Flow = "password", // "implicit",
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

            services.AddAuthentication(o =>
             {
                 o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(options =>
             {
                 options.Authority = "http://localhost:8484";
                 options.Audience = "blogcore_api_resource";
                 options.RequireHttpsMetadata = false;

                 options.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = OnTokenValidated
                 };
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

            app.UseCors("CorsPolicy")
                .UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseSwagger().UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Core APIs");
                        c.ConfigureOAuth2("local_swagger", "secret".Sha256(), "local_swagger", "local_swagger");
                        // c.ConfigureOAuth2("swagger", "secret".Sha256(), "swagger", "swagger");
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

        private static async Task OnTokenValidated(TokenValidatedContext context)
        {
            // get current principal
            var principal = context.Principal;

            // get current claim identity
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            // build up the id_token and put it into current claim identity
            var headerToken =
                context.Request.Headers["Authorization"][0].Substring(context.Scheme.Name.Length + 1);
            claimsIdentity?.AddClaim(new Claim("id_token", headerToken));

            var securityContextInstance = context.HttpContext.RequestServices.GetService<ISecurityContext>();
            var securityContextPrincipal = securityContextInstance as ISecurityContextPrincipal;
            if (securityContextPrincipal == null)
                throw new ViolateSecurityException("Could not initiate the MasterSecurityContextPrincipal object.");
            securityContextPrincipal.Principal = principal;

            var blogRepoInstance = context.HttpContext.RequestServices.GetService<IEfRepository<BlogDbContext, BlogContext.Domain.Blog>>();
            var email = securityContextInstance.GetCurrentEmail();
            var blogs = await blogRepoInstance.ListAsync();
            var blog = blogs.FirstOrDefault(x => x.OwnerEmail == email);
            securityContextPrincipal.SetBlog(blog);

            await Task.FromResult(0);
        }
    }
}