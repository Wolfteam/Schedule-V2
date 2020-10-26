using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Api.IntegrationTests.Config;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;
using Shouldly;
using System;
using System.Net;
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

        protected void AssertEmptyResponse(
            HttpResponseMessage response,
            EmptyResponseDto apiResponse,
            HttpStatusCode desiredStatusCode = HttpStatusCode.OK,
            bool shouldSucceed = true)
        {
            response.StatusCode.ShouldBe(
                desiredStatusCode,
                $"Something went wrong {desiredStatusCode} != {response.StatusCode}.... " +
                $"Msg = {apiResponse?.ErrorMessage ?? "N/A"} {Environment.NewLine} Code = {apiResponse?.ErrorMessageId ?? "N/A"}");
            apiResponse.ShouldNotBeNull();
            if (shouldSucceed)
            {
                apiResponse.Succeed.ShouldBeTrue($"Api response didn't succeed, error = {apiResponse.ErrorMessage} - {apiResponse.ErrorMessageId}");
            }
            else
            {
                apiResponse.Succeed.ShouldBeFalse();
            }
        }

        protected void AssertApiResponse<TDto>(
            HttpResponseMessage response,
            ApiResponseDto<TDto> apiResponse,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            bool shouldSucceed = true)
            where TDto : class
        {
            AssertEmptyResponse(response, apiResponse, statusCode, shouldSucceed);
            if (shouldSucceed)
                apiResponse.Result.ShouldNotBeNull();
        }

        protected void AssertApiListResponse<TDto>(
            HttpResponseMessage response,
            ApiListResponseDto<TDto> apiResponse,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            bool shouldSucceed = true)
            where TDto : class
        {
            AssertEmptyResponse(response, apiResponse, statusCode, shouldSucceed);
            if (shouldSucceed)
                apiResponse.Result.ShouldNotBeEmpty();
        }

        protected void AssertPaginatedResponse<TDto>(
            HttpResponseMessage response,
            PaginatedResponseDto<TDto> apiResponse,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            bool shouldSucceed = true)
            where TDto : class
        {
            AssertEmptyResponse(response, apiResponse, statusCode, shouldSucceed);
            if (!shouldSucceed)
                return;
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeEmpty();
            apiResponse.Records.ShouldBeGreaterThan(0);
            apiResponse.TotalRecords.ShouldBeGreaterThan(0);
            apiResponse.Take.ShouldBeGreaterThan(0);
            apiResponse.TotalPages.ShouldBeGreaterThan(0);
            apiResponse.CurrentPage.ShouldBeGreaterThan(0);
        }
    }
}
