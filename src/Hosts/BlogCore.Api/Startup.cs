#region libs

using BlogCore.AccessControl;
using BlogCore.AccessControlContext.Domain;
using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.Api.Features.Posts.ListOutPostByBlog;
using BlogCore.BlogContext;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace BlogCore.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = env.BuildConfiguration();
            Environment = env;

            // https://github.com/dotnet/corefx/issues/9158
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // clear any handler for JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<IdentityServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("MainDb"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("MainDb"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddConfigurationStoreCache()
                .AddAspNetIdentity<AppUser>()
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            services.AddCorsForBlog()
                // .AddAuthorizationForBlog()
                .AddMvcForBlog(RegisteredAssemblies());

            services.AddOptions()
                .Configure<PagingOption>(Configuration.GetSection("Paging"));

            if (Environment.IsDevelopment())
                services.AddSwaggerForBlog();

            services.AddMediatR(RegisteredAssemblies());

            services.AddIdentityServerForBlog(OnTokenValidated);

            // register presenters
            services.AddScoped<ListOutPostByBlogPresenter>();

            return services.InitServices(RegisteredAssemblies(), Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            app.UseAuthentication()
                .UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        var maxAge = new TimeSpan(7, 0, 0, 0);
                        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                            "public,max-age=" + maxAge.TotalSeconds.ToString("0");
                    }
                });

            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(fordwardedHeaderOptions);
            app.UseIdentityServer();

            app.UseCors("CorsPolicy")
                .UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
                app.UseSwaggerUiForBlog();
        }

        private static Assembly[] RegisteredAssemblies()
        {
            return new[]
            {
                typeof(BlogUseCaseModule).GetTypeInfo().Assembly,
                typeof(PostUseCaseModule).GetTypeInfo().Assembly,
                typeof(AccessControlUseCaseModule).GetTypeInfo().Assembly,
                typeof(Startup).GetTypeInfo().Assembly
            };
        }

        private static async Task OnTokenValidated(TokenValidatedContext context)
        {
            // get current principal
            var principal = context.Principal;

            // get current claim identity
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            // build up the id_token and put it into current claim identity
            var headerToken =
                context.Request.Headers["Authorization"][0].Substring(context.Scheme.Name.Length + 1);
            claimsIdentity?.AddClaim(new Claim("id_token", headerToken));

            var securityContextInstance = context.HttpContext.RequestServices.GetService<ISecurityContext>();
            var securityContextPrincipal = securityContextInstance as ISecurityContextPrincipal;
            if (securityContextPrincipal == null)
                throw new ViolateSecurityException("Could not initiate the MasterSecurityContextPrincipal object.");
            securityContextPrincipal.Principal = principal;

            var blogRepoInstance = context.HttpContext.RequestServices.GetService<IEfRepository<BlogDbContext, BlogContext.Domain.Blog>>();
            var email = securityContextInstance.GetCurrentEmail();
            var blogs = await blogRepoInstance.ListAsync();
            var blog = blogs.FirstOrDefault(x => x.OwnerEmail == email);
            securityContextPrincipal.SetBlog(blog);

            await Task.FromResult(0);
        }
    }

    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
        private readonly UserManager<AppUser> _userManager;

        public IdentityWithAdditionalClaimsProfileService(UserManager<AppUser> userManager,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.FamilyName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.GivenName));
            claims.Add(new Claim("bio", user.Bio));
            claims.Add(new Claim("company", user.Company));
            claims.Add(new Claim("location", user.Location));
            claims.Add(new Claim(JwtClaimTypes.Role, "blogcore_blogs"));

            var isAdmin = claims.Any(claim => claim.Type == "role" && claim.Value == "admin");
            if (isAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            if (user.BlogId.HasValue)
            {
                claims.Add(new Claim("blog_id", user.BlogId.Value.ToString()));
            }

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}