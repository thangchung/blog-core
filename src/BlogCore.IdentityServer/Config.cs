using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace BlogCore.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "blog.core.api",
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
                /*
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("373f4671-0c18-48d6-9da3-962b1c81299a".Sha256()) },

                    AllowedScopes = { "blog_core_api" }
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("373f4671-0c18-48d6-9da3-962b1c81299a".Sha256()) },

                    RedirectUris = { "http://localhost:5001/signin-oidc" },
                    LogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "blog_core_api" }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    
                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "blog_core_api" }
                }, */

                // swagger UI
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "swagger",
                    // ClientUri = "http://localhost:8484/swagger",

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