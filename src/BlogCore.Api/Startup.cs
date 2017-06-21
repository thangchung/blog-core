using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Core;
using BlogCore.Core.Security;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Infrastructure.Data;
using BlogCore.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogCore.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = env.BuildConfiguration();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            services.AddCorsForBlog()
                .AddAuthorizationForBlog()
                .AddMvcForBlog(typeof(BlogCoreDbContext).GetTypeInfo().Assembly);

            if (Environment.IsDevelopment())
                services.AddSwaggerForBlog();

            services.AddDbContext<BlogCoreDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MainDb")))
                .AddMediatR(RegisteredAssemblies());

            // security context
            builder.RegisterType<SecurityContextProvider>()
                .As<ISecurityContext>()
                .As<ISecurityContextPrincipal>()
                .InstancePerLifetimeScope();

            // core & infra register
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>));

            // scan modules in other assemblies
            builder.RegisterAssemblyModules(RegisteredAssemblies());

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerForBlog()
                .UseStaticFiles()
                .UseCors("CorsPolicy")
                .UseMvc();

            if (env.IsDevelopment())
                app.UseSwaggerUiForBlog();
        }

        private static Assembly[] RegisteredAssemblies()
        {
            return new[]
            {
                typeof(EntityBase).GetTypeInfo().Assembly,
                typeof(BlogCoreDbContext).GetTypeInfo().Assembly,
                typeof(Startup).GetTypeInfo().Assembly
            };
        }
    }
}