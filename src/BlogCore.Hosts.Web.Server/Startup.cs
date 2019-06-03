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
using Microsoft.OpenApi.Models;
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

            RegisterServices(services, mvcBuilder);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { MediaTypeNames.Application.Octet });
            });

            RegisterOpenApi(services);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogCore Api v1"); });
            app.UseBlazor<Client.Startup>();
        }

        private static void RegisterServices(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
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
