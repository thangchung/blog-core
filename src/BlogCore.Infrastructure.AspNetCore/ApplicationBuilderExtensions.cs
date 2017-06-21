using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogCore.Core;
using BlogCore.Core.Blogs;
using BlogCore.Core.Security;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Infrastructure.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIdentityServerForBlog(this IApplicationBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
            return app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
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
                    OnTokenValidated = OnTokenValidated
                }
            });
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
                throw new ViolateSecurityException("Could not initiate the MasterSecurityContextPrincipal object.");
            securityContextPrincipal.Principal = principal;

            var blogRepoInstance = context.HttpContext.RequestServices.GetService<IRepository<Blog>>();
            var email = securityContextInstance.GetCurrentEmail();
            var blogs = await blogRepoInstance.ListAsync();
            var blog = blogs.FirstOrDefault(x => x.OwnerEmail == email);
            securityContextPrincipal.SetBlog(blog);

            await Task.FromResult(0);
        }
    }
}