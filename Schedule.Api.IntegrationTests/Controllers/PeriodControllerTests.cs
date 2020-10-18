using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Shouldly;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class PeriodControllerTests : BaseControllerWithDefaultTests
    {
        public PeriodControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllPeriods_AtLeastOnePeriodExists_ReturnsValidResponseDto()
        {
            //Arrange
            await CreatePeriod();
            var dto = new GetAllPeriodsRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Period", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllPeriodsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
        }

        [Fact]
        public Task CreatePeriod_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreatePeriod();
        }

        [Fact]
        public async Task UpdatePeriod_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var period = await CreatePeriod();
            var dto = new SavePeriodRequestDto
            {
                IsActive = false,
                Name = period.Name
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Period/{period.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllPeriodsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBe(period.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.IsActive.ShouldBe(dto.IsActive);
        }

        [Fact]
        public async Task DeletePeriod_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var period = await CreatePeriod();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Period/{period.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
        }
    }
}
