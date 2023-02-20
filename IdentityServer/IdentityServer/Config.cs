using IdentityServer4.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("alevelwebsite.com")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("mvc")
                    },
                },
                new ApiResource("catalog")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog.api.artifact"),
                        new Scope("catalog.api.abnormaltype"),
                        new Scope("catalog.api.anomaly"),
                        new Scope("catalog.api.characteristic"),
                        new Scope("catalog.api.frequence"),
                        new Scope("catalog.api.location"),
                        new Scope("catalog.api.catalogbff"),
                    },
                },
                new ApiResource("basket")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basket.basketCache.api")
                    }
                },
                new ApiResource("order")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("order.oprderbff.api"),
                        new Scope("order.order.api"),
                        new Scope("order.orderitem.api")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "mvc_pkce",
                    ClientName = "MVC PKCE Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc" },
                    AllowedScopes = { "openid", "profile", "mvc", "catalog.api.catalogbff", "basket.basketCache.api" },
                    RequirePkce = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "catalog",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "catalog.api.artifact",
                        "catalog.api.abnormaltype",
                        "catalog.api.anomaly",
                        "catalog.api.characteristic",
                        "catalog.api.frequence",
                        "catalog.api.location",
                        "catalog.api.catalogbff"
                    }
                },
                new Client
                {
                    ClientId = "basket",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket.basketCache.api"
                    }
                },
                new Client
                {
                    ClientId = "order",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "orderswaggerui",
                    ClientName = "Order Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["OrderApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["OrderApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "order.oprderbff.api",
                        "order.order.api",
                        "order.orderitem.api",
                    }
                }
            };
        }
    }
}