using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Schedule.IdentityServer.Models;
using Schedule.IdentityServer.Models.Entities;
using Schedule.IdentityServer.Models.Settings;
using System;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
        {
#if DEBUG || STAGING
            IdentityModelEventSource.ShowPII = true;
#endif
            var settings = config.GetSection(nameof(AppSettings)).Get<AppSettings>();

            //Identity config
            string connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("You need to provide a valid connection string");
            services.AddDbContext<AspIdentityDbContext>(options => options.UseMySql(connectionString, o =>
            {
                o.SchemaBehavior(MySqlSchemaBehavior.Translate, Shared.AppConstants.GenerateTableSchema);
                o.MigrationsHistoryTable($"{AppConstants.AspiScheme}{Shared.AppConstants.MigrationsTableName}"/*, AspIdentityDbContext.Schema*/);
            }));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Lockout.MaxFailedAccessAttempts = settings.MaxFailedAccessAttempts;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedAccount = true;
                }).AddEntityFrameworkStores<AspIdentityDbContext>()
                .AddDefaultTokenProviders();

            //This overrides the one set by identity
            services.ConfigureApplicationCookie(o => o.Cookie.Name = "Identity".GetCookieName());
            return services;
        }
    }
}
