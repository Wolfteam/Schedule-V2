using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Api.Common;
using Serilog;
using System.IO;
using Schedule.Api.IntegrationTests.Config;

namespace Schedule.Api.IntegrationTests
{
    public class AppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            LoggingConfig.SetupLogging();
            builder.ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(Directory.GetCurrentDirectory());
                c.AddJsonFile("appsettings.Test.json", true, true);
            });
            builder.ConfigureServices(ConfigureServices);
            builder.UseSerilog();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            if (DbConfig.IsDbCreated)
                return;
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            DbConfig.Init(scope.ServiceProvider);
        }
    }
}
