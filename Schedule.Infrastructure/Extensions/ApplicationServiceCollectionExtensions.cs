using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Infrastructure.Persistence;
using Schedule.Infrastructure.Persistence.Repositories;
using Schedule.Infrastructure.Services;
using Schedule.Shared;

namespace Schedule.Infrastructure.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
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
                options.UseMySql(config.GetConnectionString("DefaultConnection"), o =>
                {
                    o.SchemaBehavior(MySqlSchemaBehavior.Translate, AppConstants.GenerateTableSchema);
                    o.MigrationsHistoryTable($"{AppDbContext.ScheduleDbScheme}{AppConstants.MigrationsTableName}" /*, options.DefaultSchema*/);
                });
            });
            return services;
        }

        private static IServiceCollection AddAppRepos(this IServiceCollection services)
        {
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            return services;
        }

        private static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDataService, AppDataService>();
            return services;
        }
    }
}
