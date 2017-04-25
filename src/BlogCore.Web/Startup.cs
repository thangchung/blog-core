using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Core;
using BlogCore.Infrastructure.Data;
using BlogCore.Web.BlogFeature;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogCore.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(EntityBase).GetTypeInfo().Assembly));

            services.AddDbContext<BlogCoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddMediatR(
                typeof(EntityBase).GetTypeInfo().Assembly,
                typeof(Startup).GetTypeInfo().Assembly);

            // Core & Infra register
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));

            // Web registers
            builder.RegisterType<BlogPresenter>().AsSelf();

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}