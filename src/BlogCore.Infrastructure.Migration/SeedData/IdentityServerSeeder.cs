using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace BlogCore.Infrastructure.MigrationConsole.SeedData
{
    public static class IdentityServerSeeder
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "blog_core_api",
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },

                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "blog_core_api",
                            DisplayName = "My Blog Core API"
                        }
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // SPA client using implicit flow
                new Client
                {
                    ClientId = "blog_core_client",
                    ClientName = "Blog Core Client",
                    ClientUri = "http://localhost:8485",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:8485/index.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:8485/index.html" },
                    AllowedCorsOrigins = { "http://localhost:8485" },
                    AccessTokenLifetime = 300,

                    AllowedScopes = { "openid", "profile", "blog_core_api" }
                },

                // swagger UI
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "swagger",

                    ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:8484/swagger/o2c.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:8484/swagger" },
                    AllowedCorsOrigins = { "http://localhost:8484" },
                    AccessTokenLifetime = 300,

                    AllowedScopes = { "openid", "profile", "blog_core_api" }
                }
            };
        }


    }
}