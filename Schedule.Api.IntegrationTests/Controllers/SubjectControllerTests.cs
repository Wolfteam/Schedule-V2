using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using Shouldly;
using System.Net;
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

        [Fact(Skip = "Missing semester,etc")]
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
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
        }

        [Fact(Skip = "Missing semester,etc")]
        public Task CreateSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateSubject();
        }

        [Fact(Skip = "Missing semester,etc")]
        public async Task UpdateSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var subject = await CreateSubject();
            var dto = new SaveSubjectRequestDtoBuilder()
                .WithDefaults(1, "")
                .WithHours(5, 60)
                .WithRelations(1, 2, 3)
                .Build();

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Subject/{subject.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(subject.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.AcademicHoursPerWeek.ShouldBe(dto.AcademicHoursPerWeek);
            apiResponse.Result.TotalAcademicHours.ShouldBe(dto.TotalAcademicHours);
            apiResponse.Result.CareerId.ShouldBe(dto.CareerId);
            apiResponse.Result.SemesterId.ShouldBe(dto.SemesterId);
            apiResponse.Result.ClassroomTypePerSubjectId.ShouldBe(dto.ClassroomTypePerSubjectId);
        }

        [Fact(Skip = "Missing semester,etc")]
        public async Task DeleteSubject_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var subject = await CreateSubject();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Subject/{subject.Id}");
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
        }
    }
}
