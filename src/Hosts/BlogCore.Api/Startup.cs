using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Infrastructure.EfCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using BlogCore.AccessControl.Domain;
using BlogCore.AccessControl.Domain.SecurityContext;
using BlogCore.AccessControl.Infrastructure;
using BlogCore.AccessControl.Infrastructure.SecurityContext;
using BlogCore.AccessControl.Presenters;
using BlogCore.AccessControl.UseCases;
using BlogCore.Blog.Infrastructure;
using BlogCore.Blog.Presenters;
using BlogCore.Blog.UseCases;
using BlogCore.Post.Infrastructure;
using BlogCore.Post.Presenters;
using BlogCore.Post.UseCases;

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
                .AddMvcForBlog(RegisteredAssemblies());

            if (Environment.IsDevelopment())
                services.AddSwaggerForBlog();

            services.AddMediatR(RegisteredAssemblies());

            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddDbContext<PostDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddDbContext<IdentityServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            // security context
            builder.RegisterType<SecurityContextProvider>()
                .As<ISecurityContext>()
                .As<ISecurityContextPrincipal>()
                .InstancePerLifetimeScope();

            // core & infra register
            builder.RegisterGeneric(typeof(EfRepository<,>))
                .As(typeof(IRepository<,>));
            builder.RegisterType<UserRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // scan modules in other assemblies
            builder.RegisterAssemblyModules(RegisteredAssemblies());

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerForBlog(OnTokenValidated)
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
                // Blog
                typeof(BlogDbContext).GetTypeInfo().Assembly,
                typeof(BlogPresenterModule).GetTypeInfo().Assembly,
                typeof(BlogUseCaseModule).GetTypeInfo().Assembly,
                // Post
                typeof(PostDbContext).GetTypeInfo().Assembly,
                typeof(PostPresenterModule).GetTypeInfo().Assembly,
                typeof(PostUseCaseModule).GetTypeInfo().Assembly,
                // Access Control
                typeof(IdentityServerDbContext).GetTypeInfo().Assembly,
                typeof(AccessControlPresenterModule).GetTypeInfo().Assembly,
                typeof(AccessControlUseCaseModule).GetTypeInfo().Assembly,
                // Main
                typeof(Startup).GetTypeInfo().Assembly
            };
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

            var blogRepoInstance = context.HttpContext.RequestServices.GetService<IRepository<BlogDbContext, Blog.Domain.Blog>>();
            var email = securityContextInstance.GetCurrentEmail();
            var blogs = await blogRepoInstance.ListAsync();
            var blog = blogs.FirstOrDefault(x => x.OwnerEmail == email);
            securityContextPrincipal.SetBlog(blog);

            await Task.FromResult(0);
        }
    }
}