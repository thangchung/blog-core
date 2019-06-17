using BlogCore.Modules.AccessControlContext;
using BlogCore.Modules.BlogContext;
using BlogCore.Modules.CommonContext;
using BlogCore.Modules.PostContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Server
{
    public class Startup
    {
        public static readonly SymmetricSecurityKey
            SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var mvcBuilder = services.AddMvc()
                .AddNewtonsoftJson();

            RegisterServices(services, mvcBuilder);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { MediaTypeNames.Application.Octet });
            });

            RegisterOpenApi(services);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateActor = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = SecurityKey,
                        };

                    options.Authority = "http://localhost:5001";
                    options.Audience = "api1";
                    options.RequireHttpsMetadata = false; // for Demo only
                    options.SaveToken = true;

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            context.HttpContext.User = context.Principal;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("api", policy =>
                {
                    policy.WithOrigins("http://localhost:5001");

                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();

            app.UseCors("api");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogCore Api v1"); });
        }

        private static void RegisterServices(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            services.AddHttpContextAccessor();

            mvcBuilder.AddApplicationPart(typeof(ValuesController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(UsersController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(BlogsController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(PostsController).Assembly);

            services.AddAccessControlModule();
            services.AddBlogModule();
            services.AddPostModule();
        }

        private void RegisterOpenApi(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", CreateInfoForApiVersion(Configuration));
            });

            OpenApiInfo CreateInfoForApiVersion(IConfiguration config)
            {
                var info = new OpenApiInfo
                {
                    Title = "BlogCore Service",
                    Version = "v1.0",
                    Description = "BlogCore Service"
                };
                return info;
            }
        }
    }
}
