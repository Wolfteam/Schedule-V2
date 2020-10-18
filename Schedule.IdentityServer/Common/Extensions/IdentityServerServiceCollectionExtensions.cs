using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Schedule.IdentityServer.Models.Entities;
using Schedule.IdentityServer.Models.Settings;
using Schedule.IdentityServer.Services;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class IdentityServerServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServer(
            this IServiceCollection services,
            IConfiguration config,
            IWebHostEnvironment environment)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("You need to provide a valid connection string");

            var settings = config.GetSection(nameof(AppSettings)).Get<AppSettings>();
            services.AddSingleton(settings);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.UserInteraction.LoginUrl = "/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Account/Logout";
                    options.UserInteraction.ErrorUrl = "/Account/Error";
                    options.Authentication = new AuthenticationOptions
                    {
                        CookieAuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                        CheckSessionCookieName = "Session".GetCookieName(),
                        CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
                        CookieSlidingExpiration = true
                    };
                })
                .AddConfigurationStore(options =>
                {
                    options.DefaultSchema = AppConstants.IscScheme;
                    options.ConfigureDbContext = b => b.UseMySql(connectionString, o =>
                    {
                        o.SchemaBehavior(MySqlSchemaBehavior.Translate, Shared.AppConstants.GenerateTableSchema);
                        o.MigrationsHistoryTable($"{AppConstants.IscScheme}{Shared.AppConstants.MigrationsTableName}" /*, options.DefaultSchema*/);
                        o.MigrationsAssembly(migrationsAssembly);
                    });
                })
                .AddOperationalStore(options =>
                {
                    options.DefaultSchema = AppConstants.IspScheme;
                    options.ConfigureDbContext = b => b.UseMySql(connectionString, o =>
                    {
                        o.SchemaBehavior(MySqlSchemaBehavior.Translate, Shared.AppConstants.GenerateTableSchema);
                        o.MigrationsHistoryTable($"{AppConstants.IspScheme}{Shared.AppConstants.MigrationsTableName}" /*, options.DefaultSchema*/);
                        o.MigrationsAssembly(migrationsAssembly);
                    });
                    options.EnableTokenCleanup = true;
                }).AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                .AddCertificate(config, environment);

            //This overrides the ones set by identity server
            services.AddAuthentication()
                .AddCookie(o => o.Cookie.Name = "App".GetCookieName())
                .AddJwtBearer(options =>
                {
                    options.Authority = settings.Domain;
                    //Here we don't care who the audience may be
                    options.TokenValidationParameters.ValidateAudience = false;
                    if (!environment.IsEnvironment(Shared.AppConstants.TestingEnvironment))
                        return;
                    SetupForTest(options);
                });

            return services;
        }

        private static IIdentityServerBuilder AddCertificate(
            this IIdentityServerBuilder builder,
            IConfiguration config,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment() || environment.IsEnvironment(Shared.AppConstants.TestingEnvironment))
            {
                return builder.AddDeveloperSigningCredential();
            }

            var settings = config.GetSection(nameof(AppSettings)).Get<AppSettings>();

            if (string.IsNullOrWhiteSpace(settings.CertificatePassword) || string.IsNullOrWhiteSpace(settings.CertificatePath))
            {
                throw new Exception("You need to provide a valid certificate path and the corresponding password");
            }

            var cert = new X509Certificate2(
                settings.CertificatePath,
                settings.CertificatePassword,
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet |
                X509KeyStorageFlags.Exportable
            );
            builder.AddSigningCredential(cert);

            return builder;
        }

        private static void SetupForTest(JwtBearerOptions options)
        {
            //TODO: IMPROVE THIS 
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidIssuer = Shared.AppConstants.TestingUrl,
                ValidateAudience = false,
                //ValidateIssuer = false,
                //ValidateIssuerSigningKey = false,
            };
            options.TokenValidationParameters = tokenValidationParams;
            options.Authority = Shared.AppConstants.TestingUrl;
            options.RequireHttpsMetadata = false;
            //For some reason, the config can't be loaded...
            //That's why we manually loaded it
            options.Configuration = new OpenIdConnectConfiguration
            {
                //ScopesSupported =
                //{
                //    "profile",
                //    "openid",
                //    "gift_api",
                //    "offline_access"
                //},
                //ClaimsSupported =
                //{
                //    "updated_at",
                //    "locale",
                //    "zoneinfo",
                //    "birthdate",
                //    "gender",
                //    "website",
                //    "picture",
                //    "profile",
                //    "preferred_username",
                //    "nickname",
                //    "middle_name",
                //    "given_name",
                //    "family_name",
                //    "name",
                //    "sub"
                //},
                GrantTypesSupported =
                {
                    "authorization_code",
                    "client_credentials",
                    "refresh_token",
                    "implicit",
                    "password",
                    "urn:ietf:params:oauth:grant-type:device_code"
                },
                ResponseTypesSupported =
                {
                    "code",
                    "token",
                    "id_token",
                    "id_token token",
                    "code id_token",
                    "code token",
                    "code id_token token"
                },
                IdTokenSigningAlgValuesSupported =
                {
                    "RS256"
                },
                ResponseModesSupported =
                {
                    "form_post",
                    "query",
                    "fragment"
                },
                TokenEndpointAuthMethodsSupported =
                {
                    "client_secret_basic",
                    "client_secret_post"
                },
                SubjectTypesSupported =
                {
                    "public"
                },
                RequestParameterSupported = true,
            };
            //For some reason, the developer key is not already loaded...
            //That's why manually load it
            string tempKey = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.jwk");
            using var fs = new FileStream(tempKey, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
            var json = sr.ReadToEnd();
            var jwk = new JsonWebKey(json);
            options.Configuration.SigningKeys.Add(jwk);
        }
    }
}
