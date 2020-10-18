using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Infrastructure.Persistence;
using Schedule.Infrastructure.Persistence.Repositories;
using Schedule.Infrastructure.Services;
using Schedule.Shared;
using Serilog;

namespace Schedule.Infrastructure
{
    public static class DependencyInjection
    {
        public static IApplicationBuilder UseAndApplyAppMigrations(this IApplicationBuilder app, IConfiguration config)
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

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            return services.AddAppDbContext(config)
                .AddAppRepos()
                .AddAppServices();
        }

        private static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(c => c.AddSerilog()));
                options.UseMySql(config.GetConnectionString("DefaultConnection"), o =>
                {
                    o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    o.SchemaBehavior(MySqlSchemaBehavior.Translate, AppConstants.GenerateTableSchema);
                    o.MigrationsHistoryTable($"{AppDbContext.ScheduleDbScheme}{AppConstants.MigrationsTableName}" /*, options.DefaultSchema*/);
                }).EnableSensitiveDataLogging();
            });
            return services;
        }

        private static IServiceCollection AddAppRepos(this IServiceCollection services)
        {
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IClassroomSubjectRepository, ClassroomSubjectRepository>();
            services.AddScoped<IPeriodRepository, PeriodRepository>();
            services.AddScoped<IPeriodSectionRepository, PeriodSectionRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherAvailabilityRepository, TeacherAvailabilityRepository>();
            services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherScheduleRepository, TeacherScheduleRepository>();
            return services;
        }

        private static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDataService, AppDataService>();
            return services;
        }
    }
}
