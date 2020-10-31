using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Refit;
using Schedule.Domain.Interfaces.Managers;
using Schedule.Shared.Middleware;
using Schedule.Web.Common.Handler;
using Schedule.Web.Interfaces.Apis;
using Schedule.Web.Managers;
using Schedule.Web.Models.Settings;
using Schedule.Web.Services.Api;
using System;

namespace Schedule.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var idsSettings = Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();
            services.AddSingleton(idsSettings);
            services.AddHttpContextAccessor().AddDistributedMemoryCache();

            services.AddTransient<IDefaultAppUserManager, AppUserManager>();

            services.AddSession(options =>
            {
                options.Cookie.Name = GetCookieName("Session");
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddMvcCore()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .AddApiExplorer()
                .AddAuthorization();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, c => c.Cookie.Name = GetCookieName("App"));
            services.AddControllers();
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

            RegisterApis(services);
            RegisterAppServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AppStatusMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                    //spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void RegisterApis(IServiceCollection services)
        {
            var baseAddress = new Uri(Configuration["BaseBackendUrl"]);
            services.AddRefitClient<ICareerApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);

            services.AddRefitClient<IClassroomApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);

            services.AddRefitClient<IPeriodApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);

            services.AddRefitClient<ISubjectApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);

            services.AddRefitClient<ITeacherApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);

            services.AddRefitClient<ISemesterApi>()
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .AddHttpMessageHandler(ResolveApiHandler);
        }

        private static AuthenticatedHttpClientHandler ResolveApiHandler(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<IHttpContextAccessor>();
            var settings = provider.GetRequiredService<IdentityServerSettings>();
            return new AuthenticatedHttpClientHandler(context, settings);
        }

        private void RegisterAppServices(IServiceCollection services)
        {
            services.AddSingleton<ICareerApiService, CareerApiService>();
            services.AddSingleton<IClassroomApiService, ClassroomApiService>();
            services.AddSingleton<IPeriodApiService, PeriodApiService>();
            services.AddSingleton<ISemesterApiService, SemesterApiService>();
            services.AddSingleton<ISubjectApiService, SubjectApiService>();
            services.AddSingleton<ITeacherApiService, TeacherApiService>();
        }

        private static string GetCookieName(string x)
            => $"Schedule.Web.{x}";
    }
}
