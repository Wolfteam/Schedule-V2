using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Schedule.Api.Managers;
using Schedule.Application.Interfaces.Managers;
using Schedule.Shared.Models.Settings;
using System;
using System.Globalization;

namespace Schedule.Api.Common.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

            if (string.IsNullOrEmpty(settings.Authority))
                throw new ArgumentNullException(nameof(settings.Authority));

            if (string.IsNullOrEmpty(settings.ApiName))
                throw new ArgumentNullException(nameof(settings.ApiName));

            if (string.IsNullOrEmpty(settings.ApiSecret))
                throw new ArgumentNullException(nameof(settings.ApiSecret));

            services.AddCors()
                .AddMvcCore()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .AddApiExplorer()
                .AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    options.ApiName = settings.ApiName;
                    options.ApiSecret = settings.ApiSecret;
                });

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("es-ES")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture("en-US", "es-ES");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;
            });

            return services.AddFluentValidation()
                .AddAppServices();
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<IAppUserManager>(ServiceLifetime.Scoped);
            services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();

            return services;
        }

        private static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IAppUserManager, AppUserManager>();
            return services;
        }
    }
}
