using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Api.IntegrationTests.Config;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;
using System;
using System.Net.Http;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class BaseControllerTests : IClassFixture<AppFactory<Startup>>
    {
        protected HttpClient HttpClient;
        protected AppFactory<Startup> Factory;

        protected BaseControllerTests(AppFactory<Startup> factory)
        {
            Factory = factory;
            HttpClient = BuildAuthClient();
        }

        protected virtual void SetUpDefaultTestServices(IServiceCollection services)
        {
        }

        protected virtual void SetTestAuthScheme(
            IServiceCollection services,
            long schoolId = AppConstants.IdThatShouldExist,
            SchedulePermissionType permission = SchedulePermissionType.All)
        {
            services.AddAuthentication(AppConstants.TestAuthScheme)
                .AddScheme<TestAuthSchemeOptions, TestAuthHandler>(
                    AppConstants.TestAuthScheme,
                    options =>
                    {
                        options.SchoolId = schoolId;
                        options.Permissions = permission;
                    });
        }

        protected HttpClient BuildAuthClient(
            long schoolId = AppConstants.IdThatShouldExist,
            SchedulePermissionType permission = SchedulePermissionType.All,
            Action<IServiceCollection> testServices = null)
        {
            return Factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        SetTestAuthScheme(services, schoolId, permission);
                        SetUpDefaultTestServices(services);
                        testServices?.Invoke(services);
                    });

                    builder.UseEnvironment(Shared.AppConstants.TestingEnvironment);
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });
        }

        protected HttpClient BuildUnAuthorizedClient()
        {
            return Factory
                .WithWebHostBuilder(builder => builder.UseEnvironment(Shared.AppConstants.TestingEnvironment))
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        protected string SetUrlParameters(string baseUrl, object dto)
        {
            return QueryHelpers.AddQueryString(baseUrl, dto.ToKeyValue());
        }
    }
}
