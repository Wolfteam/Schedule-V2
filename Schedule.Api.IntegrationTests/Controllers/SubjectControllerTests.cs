using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class SubjectControllerTests : BaseControllerWithDefaultTests
    {
        public SubjectControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllSubjects_AtLeastOneSubjectExists_ReturnsValidResponseDto()
        {
            //Arrange
            await CreateSubject();
            var dto = new GetAllSubjectsRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Subject", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            AssertPaginatedResponse(response, apiResponse);
            apiResponse.Result.ShouldAllBe(r => r.Career != null);
            apiResponse.Result.ShouldAllBe(r => r.ClassroomType != null);
            apiResponse.Result.ShouldAllBe(r => r.Semester != null);
        }

        [Fact]
        public async Task GetSubject_SubjectExists_ReturnsValidResponseDto()
        {
            //Arrange
            var subject = await CreateSubject();

            //Act
            var response = await HttpClient.GetAsync($"api/Subject/{subject.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(subject.Id);
            apiResponse.Result.Career.ShouldNotBeNullOrEmpty();
            apiResponse.Result.ClassroomType.ShouldNotBeNullOrEmpty();
            apiResponse.Result.Semester.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public Task CreateSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateSubject();
        }

        [Fact]
        public async Task UpdateSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var subject = await CreateSubject();
            var dto = new SaveSubjectRequestDtoBuilder()
                .WithDefaults(689874, "Updated subject")
                .WithHours(7, 100)
                .WithRelations(subject.SemesterId, subject.CareerId, subject.ClassroomTypePerSubjectId)
                .Build();

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Subject/{subject.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBe(subject.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.AcademicHoursPerWeek.ShouldBe(dto.AcademicHoursPerWeek);
            apiResponse.Result.TotalAcademicHours.ShouldBe(dto.TotalAcademicHours);
            apiResponse.Result.Career.ShouldNotBeEmpty();
            apiResponse.Result.CareerId.ShouldBe(dto.CareerId);
            apiResponse.Result.Semester.ShouldNotBeEmpty();
            apiResponse.Result.SemesterId.ShouldBe(dto.SemesterId);
            apiResponse.Result.ClassroomTypePerSubjectId.ShouldBe(dto.ClassroomTypePerSubjectId);
            apiResponse.Result.ClassroomType.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task DeleteSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var subject = await CreateSubject();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Subject/{subject.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            AssertEmptyResponse(response, apiResponse);
        }
    }
}
