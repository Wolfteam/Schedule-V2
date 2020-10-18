using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Dto.Teachers.Responses;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class BaseControllerWithDefaultTests : BaseControllerTests
    {
        protected BaseControllerWithDefaultTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        protected async Task<GetAllPrioritiesResponseDto> CreatePriority()
        {
            //Arrange
            var dto = new SavePriorityRequestDto
            {
                HoursToComplete = 12,
                Name = $"MC_PRO_{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Teacher/Priorities", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllPrioritiesResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.HoursToComplete.ShouldBe(dto.HoursToComplete);

            return apiResponse.Result;
        }

        protected async Task<GetAllTeacherResponseDto> CreateTeacher()
        {
            //Arrange
            var priority = await CreatePriority();
            var dto = new SaveTeacherRequestDtoBuilder()
                .WithDefaults("Efrain", "Bastidas", priority.Id)
                .Build();

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Teacher", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllTeacherResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.PriorityId.ShouldBe(priority.Id);
            apiResponse.Result.FirstName.ShouldBe(dto.FirstName);
            apiResponse.Result.FirstLastName.ShouldBe(dto.FirstLastName);
            apiResponse.Result.IdentifierNumber.ShouldBe(dto.IdentifierNumber);

            return apiResponse.Result;
        }

        protected async Task<GetAllSubjectsResponseDto> CreateSubject()
        {
            //Arrange
            var dto = new SaveSubjectRequestDtoBuilder()
                .WithDefaults(1, "")
                .WithHours(5, 60)
                .WithRelations(1, 2, 3)
                .Build();

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Subject", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.AcademicHoursPerWeek.ShouldBe(dto.AcademicHoursPerWeek);
            apiResponse.Result.TotalAcademicHours.ShouldBe(dto.TotalAcademicHours);
            apiResponse.Result.CareerId.ShouldBe(dto.CareerId);
            apiResponse.Result.SemesterId.ShouldBe(dto.SemesterId);
            apiResponse.Result.ClassroomTypePerSubjectId.ShouldBe(dto.ClassroomTypePerSubjectId);

            return apiResponse.Result;
        }

        public async Task<GetAllClassroomTypesResponseDto> CreateClassroomType()
        {
            //Arrange
            var dto = new SaveClassroomTypeRequestDto
            {
                Name = $"Lab of Sex_{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Classroom/Types", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomTypesResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            return apiResponse.Result;
        }

        public async Task<GetAllPeriodsResponseDto> CreatePeriod()
        {
            //Arrange
            var dto = new SavePeriodRequestDto
            {
                IsActive = true,
                Name = $"2020-I_{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Period", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllPeriodsResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.IsActive.ShouldBe(dto.IsActive);
            return apiResponse.Result;
        }
    }
}
