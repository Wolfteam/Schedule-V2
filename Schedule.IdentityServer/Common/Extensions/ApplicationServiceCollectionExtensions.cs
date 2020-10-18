using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Schedule.Domain.Interfaces.Managers;
using Schedule.IdentityServer.Managers;
using Schedule.Shared.Extensions;
using System.Globalization;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration config)
        {
            var mcBuilder = services.AddControllersWithViews();
#if DEBUG
            mcBuilder.AddRazorRuntimeCompilation();
#endif
            services.AddCors()
                .AddMvcCore()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .AddApiExplorer()
                .AddAuthorization();
            services.AddHttpContextAccessor();
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
            return services
                .AddAuthHandlers()
                .AddAppServices()
                .AddAppManagers()
                .AddAppSettings(config)
                .AddSwagger(config, "IdentityServer", "IdentityServer.xml");
        }

        private static IServiceCollection AddAuthHandlers(this IServiceCollection services)
        {
            //services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            return services;
        }

        private static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddAppManagers(this IServiceCollection services)
        {
            services.AddTransient<IDefaultAppUserManager, AppUserManager>();
            return services;
        }

        private static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
