using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class SemesterControllerTests : BaseControllerWithDefaultTests
    {
        public SemesterControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllSemesters_AtLeastOneExists_ReturnsValidResponseDto()
        {
            //Arrange
            var semester = await CreateSemester();

            //Act
            var response = await HttpClient.GetAsync("api/Semester");
            var apiResponse = await response.Content.ReadAsAsync<ApiListResponseDto<GetAllSemestersResponseDto>>();

            //Assert
            AssertApiListResponse(response, apiResponse);
            apiResponse.Result.ShouldContain(s => s.Id == semester.Id);
        }

        [Fact]
        public async Task GetSemester_SemesterExists_ReturnsValidResponseDto()
        {
            //Arrange
            var semester = await CreateSemester();

            //Act
            var response = await HttpClient.GetAsync($"api/Semester/{semester.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSemestersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(semester.Id);
        }

        [Fact]
        public Task CreateSemester_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateSemester();
        }

        [Fact]
        public async Task UpdateSemester_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var semester = await CreateSemester();
            var dto = new SaveSemesterRequestDto
            {
                Name = "SEMESTER-UPDATED"
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Semester/{semester.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSemestersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(semester.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
        }

        [Fact]
        public async Task DeleteSemester_SemesterExists_ReturnsValidResponseDto()
        {
            //Arrange
            var semester = await CreateSemester();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Semester/{semester.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            AssertEmptyResponse(response, apiResponse);
        }
    }
}
