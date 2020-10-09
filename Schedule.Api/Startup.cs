using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Api.Common.Extensions;
using Schedule.Application;
using Schedule.Infrastructure.Extensions;
using Schedule.Shared.Extensions;
using Schedule.Shared.Models.Settings;

namespace Schedule.Api
{
    public class Startup
    {
        private const string SwaggerApiName = "Schedule";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApi(Configuration)
                .AddInfrastructure(Configuration)
                .AddApplication()
                .AddSwagger(Configuration, SwaggerApiName, "Schedule.xml");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplyMigrations(Configuration);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(options =>
                options.WithOrigins(Configuration.GetValue<string>($"{nameof(IdentityServerSettings)}:{nameof(IdentityServerSettings.Authority)}"))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
            //app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<AppStatusMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
            app.UseSwagger(Configuration, SwaggerApiName);
        }
    }
}
