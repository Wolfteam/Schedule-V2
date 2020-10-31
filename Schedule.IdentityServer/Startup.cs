using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.IdentityServer.Common.Extensions;
using Schedule.Shared.Extensions;
using Schedule.Shared.Middleware;
using System;
using System.IO;

namespace Schedule.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        //Add-Migration InitConfigDb -Context ConfigurationDbContext -OutputDir Migrations/IdentityServer/ConfigurationDb
        //Add-Migration InitPersistedDb -Context PersistedGrantDbContext -OutputDir Migrations/IdentityServer/PersistedDb
        //Add-Migration InitAspIdentity -Context AspIdentityDbContext -OutputDir Migrations/AspIdentity
        //TODO: GOOGLE AUTH
        //TODO: REACT VIEW

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            //this is required, otherwise logs are not created
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApp(Configuration)
                .AddIdentity(Configuration)
                .AddIdentityServer(Configuration, Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplyMigrations(Configuration);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(options =>
                options.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
            );
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseMiddleware<AppStatusMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
            app.UseSwagger(Configuration, "IdentityServer");
        }
    }
}
