using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIdentityServerForBlog(this IApplicationBuilder app)
            // Func<TokenValidatedContext, Task> onTokenValidated)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
            return app.UseIdentityServer();

            //TODO: refactoring consideration
            /*return app.UseIdentityServer(new IdentityServerAuthenticationOptions
            {
                AuthenticationScheme = "Bearer",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = "http://localhost:8483",
                SaveToken = true,
                AllowedScopes = new[] {"blogcore_api_scope"},
                RequireHttpsMetadata = false,
                JwtBearerEvents = new JwtBearerEvents
                {
                    OnTokenValidated = onTokenValidated
                }
            });*/
        }

        public static IApplicationBuilder UseSwaggerUiForBlog(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Core APIs");
                    c.ConfigureOAuth2("swagger", "secret".Sha256(), "swagger", "swagger");
                });
        }
    }
}