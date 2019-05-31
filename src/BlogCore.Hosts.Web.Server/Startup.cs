using BlogCore.Modules.AccessControlContext;
using BlogCore.Modules.BlogContext;
using BlogCore.Modules.CommonContext;
using BlogCore.Modules.PostContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Mime;

namespace BlogCore.Hosts.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers()
                .AddNewtonsoftJson();

            mvcBuilder.AddApplicationPart(typeof(ValuesController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(UsersController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(BlogsController).Assembly);
            mvcBuilder.AddApplicationPart(typeof(PostsController).Assembly);

            services.AddAccessControlModule();
            services.AddBlogModule();
            services.AddPostModule();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { MediaTypeNames.Application.Octet });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
