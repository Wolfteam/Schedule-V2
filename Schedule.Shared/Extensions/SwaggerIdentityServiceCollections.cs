using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Schedule.Shared.Models.Settings;
using System;
using System.IO;
using System.Linq;

namespace Schedule.Shared.Extensions
{
    public static class SwaggerIdentityServiceCollections
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            IConfiguration config,
            string apiName,
            string xmlFileName,
            string version = "V1")
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

            var apiInfo = new OpenApiInfo
            {
                Version = version,
                Title = $"{apiName} api",
                Description = $"This is the {apiName} api"
            };

            var (oauthScheme, oauthSecReq, oauthSecScheme) = BuildOauth(settings);

            var (bearerScheme, bearerSecReq, bearerSecScheme) = BuildBearer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, apiInfo);
                c.AddSecurityDefinition(oauthScheme, oauthSecScheme);
                c.AddSecurityRequirement(oauthSecReq);

                c.AddSecurityDefinition(bearerScheme, bearerSecScheme);
                c.AddSecurityRequirement(bearerSecReq);

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        private static (string, OpenApiSecurityRequirement, OpenApiSecurityScheme) BuildBearer()
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            };
            string scheme = "Bearer";
            var securityReq = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = scheme
                        }
                    }, Enumerable.Empty<string>().ToList()
                }
            };

            return (scheme, securityReq, securityScheme);
        }

        private static (string, OpenApiSecurityRequirement, OpenApiSecurityScheme) BuildOauth(SwaggerSettings settings)
        {
            var scopes = settings.SwaggerScopes?.Split(' ');
            var scopeDictionary = scopes.ToDictionary(x => x, x => "");

            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "Oauth flow",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{settings.Authority}/connect/authorize"),
                        Scopes = scopeDictionary,
                        TokenUrl = new Uri($"{settings.Authority}/connect/token"),
                    }
                }
            };

            string scheme = "oauth2";

            var securityReq = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = scheme
                        }
                    }, Enumerable.Empty<string>().ToList()
                }
            };

            return (scheme, securityReq, securityScheme);
        }
    }
}