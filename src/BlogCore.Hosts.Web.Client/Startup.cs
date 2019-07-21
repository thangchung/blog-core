using BlogCore.Hosts.Web.Client.Services;
using BlogCore.Hosts.Web.Client.Services.Impl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCore.Hosts.Web.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // authn
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();

            // services and state
            services.AddScoped<AppState>();
            services.AddScoped<BlogService>();
            services.AddScoped<PostService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
