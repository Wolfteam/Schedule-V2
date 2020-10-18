using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class IdentityServerAppBuilderExtensions
    {
        public static async Task ApplyIdentityServerMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            await serviceScope.ServiceProvider.GetService<ConfigurationDbContext>().Database.MigrateAsync();
            await serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.MigrateAsync();
        }

        public static async Task SeedConfig(this IApplicationBuilder app)
        {
            await app.ApplyIdentityServerMigrations();

            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
            var apis = new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "Schedule Api",
                    DisplayName = "Schedule Api",
                    Description = "The schedule web api",
                    ApiSecrets = new List<Secret>
                    {
                        new Secret("schedule_api_secret".Sha256(),"The Schedule web api secret" ),
                    },
                    Scopes = new List<string>
                    {
                        "schedule_api"
                    },
                }
            };

            var apiScopes = new List<ApiScope>
            {
                new ApiScope("schedule_api", "Web api scope")
            };

            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "Schedule_PCC",
                    ClientName = "Schedule client that can be used with password / client credentials",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("schedule_pcc_client_secret".Sha256())
                    },
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    // scopes that client has access to
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "schedule_api"
                    },
                    //Claims = new List<ClientClaim>
                    //{
                    //    new ClientClaim(Shared.AppConstants.MerchantIdClaim, "1", ClaimValueTypes.Integer32),
                    //    new ClientClaim(Shared.AppConstants.PermissionsClaim, PermissionType.All.GetPermissionStringValue(), ClaimValueTypes.Integer32)
                    //},
                    AllowOfflineAccess = true,
                    ClientClaimsPrefix = null
                },
                new Client
                {
                    ClientId = "FrontEnd",
                    ClientName = "Frontend Client",
                    ClientSecrets =
                    {
                        new Secret("front_client_secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysSendClientClaims = true,
                    RequirePkce = true,
                    RedirectUris =           { "https://localhost:44304/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44304" },
                    AllowedCorsOrigins =     { "https://localhost:44304" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "schedule_api"
                    },
                    AllowOfflineAccess = true,
                    ClientClaimsPrefix = null
                },
                new Client
                {
                    ClientId = "identity_server_swagger",
                    ClientName = "IdentityServer Swagger",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("identity_server_swagger_client_secret".Sha256())
                    },
                    AlwaysSendClientClaims = true,
                    RequirePkce = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "schedule_api"
                    },
                    //Claims = new List<ClientClaim>
                    //{
                    //    new ClientClaim(Shared.AppConstants.PermissionsClaim, PermissionType.All.GetPermissionStringValue(), ClaimValueTypes.Integer32)
                    //},
                    RedirectUris = {
                        "https://localhost:44313/swagger/oauth2-redirect.html"
                    },
                    ClientClaimsPrefix = null
                },
                new Client
                {
                    ClientId = "schedule_api_swagger",
                    ClientName = "Schedule Api Swagger",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("schedule_swagger_secret".Sha256())
                    },
                    AlwaysSendClientClaims = true,
                    RequirePkce = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "schedule_api"
                    },
                    //Claims = new List<ClientClaim>
                    //{
                    //    new ClientClaim(Shared.AppConstants.PermissionsClaim, PermissionType.All.GetPermissionStringValue(), ClaimValueTypes.Integer32)
                    //},
                    RedirectUris = {
                        "https://localhost:44341/swagger/oauth2-redirect.html"
                    },
                    ClientClaimsPrefix = null
                }
            };
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            if (!configContext.Clients.Any())
            {
                foreach (var client in clients)
                {
                    configContext.Clients.Add(client.ToEntity());
                }
                await configContext.SaveChangesAsync();
            }

            if (!configContext.IdentityResources.Any())
            {
                foreach (var resource in identityResources)
                {
                    configContext.IdentityResources.Add(resource.ToEntity());
                }
                await configContext.SaveChangesAsync();
            }

            if (!configContext.ApiResources.Any())
            {
                foreach (var resource in apis)
                {
                    configContext.ApiResources.Add(resource.ToEntity());
                }
                await configContext.SaveChangesAsync();
            }

            if (!configContext.ApiScopes.Any())
            {
                foreach (var resource in apiScopes)
                {
                    configContext.ApiScopes.Add(resource.ToEntity());
                }
                await configContext.SaveChangesAsync();
            }
        }
    }
}
