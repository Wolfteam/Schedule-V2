using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Shouldly;
using System;
using System.Net;
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
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
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
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
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
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
        }
        #endregion
    }
}
