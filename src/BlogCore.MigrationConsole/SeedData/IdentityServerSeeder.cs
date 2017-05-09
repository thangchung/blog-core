using System.Collections.Generic;
using IdentityServer4.Models;

namespace BlogCore.MigrationConsole.SeedData
{
    public static class IdentityServerSeeder
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("blogcore_identity_scope",
                    new[]
                    {
                        "role",
                        "admin",
                        "user",
                        "blogcore_blogs",
                        "blogcore_blogs__admin",
                        "blogcore_blogs__user"
                    })
            };
        }

        /// <summary>
        /// https://leastprivilege.com/2016/12/01/new-in-identityserver4-resource-based-configuration/
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "blogcore_api",
                    DisplayName = "My Blog Core API",
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "blogcore_api_blogs",
                            DisplayName = "The blog API scope.",
                            UserClaims =
                            {
                                "role",
                                "admin",
                                "user",
                                "blogcore_blogs",
                                "blogcore_blogs__admin",
                                "blogcore_blogs__user"
                            }
                        },
                        new Scope
                        {
                            Name = "blogcore_api_posts",
                            DisplayName = "The post API scope.",
                            UserClaims =
                            {
                                "role",
                                "admin",
                                "user",
                                "blogcore_posts",
                                "blogcore_posts__admin",
                                "blogcore_posts__user"
                            }
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
                    ClientId = "blogcore_client",
                    ClientName = "Blogcore Client",
                    ClientUri = "http://localhost:3000",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:3000/callback"
                    },
                    PostLogoutRedirectUris = {"http://localhost:3000"},
                    AllowedCorsOrigins = {"http://localhost:3000"},
                    AccessTokenLifetime = 300,
                    AllowedScopes =
                    {
                        "openid",
                        "profile",
                        "role",
                        "blogcore_identity_scope",
                        "blogcore_api",
                        "blogcore_api_blogs",
                        "blogcore_api_posts"
                    }
                },

                // swagger UI
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "swagger",
                    ClientSecrets = new List<Secret> {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:8484/swagger/o2c.html"
                    },
                    PostLogoutRedirectUris = {"http://localhost:8484/swagger"},
                    AllowedCorsOrigins = {"http://localhost:8484"},
                    AccessTokenLifetime = 300,
                    AllowedScopes =
                    {
                        "openid",
                        "profile",
                        "role",
                        "blogcore_identity_scope",
                        "blogcore_api",
                        "blogcore_api_blogs",
                        "blogcore_api_posts"
                    }
                }
            };
        }
    }
}