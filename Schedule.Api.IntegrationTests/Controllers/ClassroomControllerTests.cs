using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class ClassroomControllerTests : BaseControllerWithDefaultTests
    {
        public ClassroomControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        #region Classroom
        [Fact]
        public async Task GetAllClassrooms_AtLeastOneClassroomExists_ReturnsValidResponseDto()
        {
            //Arrange
            var classroom = await CreateClassroom();
            var dto = new GetAllClassroomsRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Classroom", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllClassroomsResponseDto>>();

            //Assert
            AssertPaginatedResponse(response, apiResponse);
        }

        [Fact]
        public async Task GetClassroom_ClassroomExists_ReturnsValidResponseDto()
        {
            //Arrange
            var classroom = await CreateClassroom();

            //Act
            var response = await HttpClient.GetAsync($"api/Classroom/{classroom.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(classroom.Id);
        }

        [Fact]
        public Task CreateClassroom_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateClassroom();
        }

        [Fact]
        public async Task UpdateClassroom_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var classroom = await CreateClassroom();
            var dto = new SaveClassroomRequestDto
            {
                Name = "Lab de computacion",
                ClassroomSubjectId = classroom.ClassroomSubjectId,
                Capacity = 60
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Classroom/{classroom.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(classroom.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
        }
        #endregion

        #region Classroom Types
        [Fact]
        public async Task GetAllClassroomTypes_AtLeastOneClassroomTypeExists_ReturnsValidResponseDto()
        {
            //Arrange
            await CreateClassroomType();
            var dto = new GetAllClassroomTypesRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Classroom/Types", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllClassroomTypesResponseDto>>();

            //Assert
            AssertPaginatedResponse(response, apiResponse);
        }

        [Fact]
        public async Task GetClassroomType_ClassroomTypeExists_ReturnsValidResponseDto()
        {
            //Arrange
            var classroomType = await CreateClassroomType();

            //Act
            var response = await HttpClient.GetAsync($"api/Classroom/Types/{classroomType.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomTypesResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(classroomType.Id);
        }

        [Fact]
        public Task CreateClassroomType_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateClassroomType();
        }

        [Fact]
        public async Task UpdateClassroomType_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var type = await CreateClassroomType();
            var dto = new SaveClassroomTypeRequestDto
            {
                Name = $"Lab of Something_{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Classroom/Types/{type.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomTypesResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(type.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
        }

        [Fact]
        public async Task DeleteClassroomType_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var type = await CreateClassroomType();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Classroom/Types/{type.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            AssertEmptyResponse(response, apiResponse);
        }
        #endregion
    }
}
