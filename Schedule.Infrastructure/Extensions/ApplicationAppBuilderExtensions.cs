using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Schedule.Infrastructure.Persistence;
using System;

namespace Schedule.Infrastructure.Extensions
{
    public static class ApplicationAppBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app, IConfiguration config)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<AppDbContext>>();
            try
            {
                logger.LogInformation("Trying to apply any pending migrations to the db...");
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                dbContext.Database.Migrate();
                logger.LogInformation("Successfully applied any pending migration.");

                if (config.GetValue<bool>("SeedDb"))
                {
                    Seed.Init(dbContext).GetAwaiter().GetResult();
                    throw new Exception("Backend migration completed, disable that thing");
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
