using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class ApplicationAppBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app, IConfiguration config)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();
            try
            {
                logger.LogInformation("Trying to apply any pending migrations to the db...");
                app.ApplyIdentityMigrations().GetAwaiter().GetResult();
                app.ApplyIdentityServerMigrations().GetAwaiter().GetResult();
                logger.LogInformation("Successfully applied any pending migration.");

                if (config.GetValue<bool>("SeedDb"))
                {
                    app.SeedUsers().GetAwaiter().GetResult();
                    app.SeedConfig().GetAwaiter().GetResult();
                    throw new Exception("User and config migration completed, disable that thing");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unknown error occurred while trying to apply any pending migration to the db.");
                throw;
            }

            return app;
        }

    }
}
