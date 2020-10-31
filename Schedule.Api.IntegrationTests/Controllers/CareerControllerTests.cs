using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class CareerControllerTests : BaseControllerWithDefaultTests
    {
        public CareerControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllCareers_AtLeastOneCareerExists_ReturnsValidResponseDto()
        {
            //Arrange
            var career = await CreateCareer();

            //Act
            var response = await HttpClient.GetAsync("api/Career");
            var apiResponse = await response.Content.ReadAsAsync<ApiListResponseDto<GetAllCareersResponseDto>>();

            //Assert
            AssertApiListResponse(response, apiResponse);
            apiResponse.Result.ShouldContain(c => c.Id == career.Id);
        }

        [Fact]
        public async Task GetCareer_CareerExists_ReturnsValidResponseDto()
        {
            //Arrange
            var career = await CreateCareer();

            //Act
            var response = await HttpClient.GetAsync($"api/Career/{career.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllCareersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(career.Id);
        }

        [Fact]
        public async Task CreateCareer_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var dto = new SaveCareerRequestDto
            {
                Name = "Ingenieria Mecatronica"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Career", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllCareersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
        }

        [Fact]
        public async Task UpdateCareer_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var career = await CreateCareer();
            var dto = new SaveCareerRequestDto
            {
                Name = "Ingenieria agricola"
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Career/{career.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllCareersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Name.ShouldBe(dto.Name);
        }

        [Fact]
        public async Task DeleteCareer_CareerExists_ReturnsValidResponseDto()
        {
            //Arrange
            var career = await CreateCareer();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Career/{career.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            AssertEmptyResponse(response, apiResponse);
        }
    }
}
