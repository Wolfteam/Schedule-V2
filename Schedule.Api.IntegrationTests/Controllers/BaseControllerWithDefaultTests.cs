using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Dto.Teachers.Responses;
using Shouldly;
using System;
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
            AssertApiResponse(response, apiResponse);
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
            AssertApiResponse(response, apiResponse);
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
            var classroomType = await CreateClassroomType();
            var career = await CreateCareer();
            var semester = await CreateSemester();
            var dto = new SaveSubjectRequestDtoBuilder()
                .WithDefaults((int)DateTimeOffset.UtcNow.Ticks, $"Subject - {DateTimeOffset.UtcNow.Ticks}")
                .WithHours(5, 60)
                .WithRelations(semester.Id, career.Id, classroomType.Id)
                .Build();

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Subject", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSubjectsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
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
            AssertApiResponse(response, apiResponse);
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
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.IsActive.ShouldBe(dto.IsActive);
            return apiResponse.Result;
        }

        public async Task<GetAllSemestersResponseDto> CreateSemester()
        {
            var dto = new SaveSemesterRequestDto
            {
                Name = $"Semester-X{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Semester", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllSemestersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            return apiResponse.Result;
        }

        public async Task<GetAllCareersResponseDto> CreateCareer()
        {
            //Arrange
            var dto = new SaveCareerRequestDto
            {
                Name = $"Ingenieria Mecatronica-{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Career", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllCareersResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            return apiResponse.Result;
        }

        public async Task<GetAllClassroomsResponseDto> CreateClassroom()
        {
            //Arrange
            var type = await CreateClassroomType();
            var dto = new SaveClassroomRequestDto
            {
                Name = $"Lab of Chemistry_{DateTimeOffset.UtcNow.Ticks}",
                ClassroomSubjectId = type.Id,
                Capacity = 40
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync("api/Classroom", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllClassroomsResponseDto>>();

            //Assert
            AssertApiResponse(response, apiResponse);
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.ClassroomSubjectId.ShouldBe(dto.ClassroomSubjectId);
            return apiResponse.Result;
        }
    }
}
