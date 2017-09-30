using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
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