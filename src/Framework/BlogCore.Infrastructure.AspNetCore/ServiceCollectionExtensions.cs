using System.Collections.Generic;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsForBlog(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static IServiceCollection AddAuthorizationForBlog(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policyAdmin => { policyAdmin.RequireClaim("role", "admin"); });
                options.AddPolicy("User",
                    policyUser => { policyUser.RequireClaim("role", "user"); });
            });
        }

        public static IMvcBuilder AddMvcForBlog(this IServiceCollection services, Assembly[] assemblies)
        {
            var mvcBuilder = services.AddMvc();
            foreach (var assembly in assemblies)
            {
                // register controllers
                mvcBuilder.AddApplicationPart(assembly);

                // register validations
                mvcBuilder.AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssembly(assembly));
            }
            return mvcBuilder;
        }

        public static IServiceCollection AddSwaggerForBlog(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
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
                        {"blogcore_api_scope", "The Blog APIs"}
                    }
                });
            });
        }
    }
}