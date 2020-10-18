using Schedule.Api.IntegrationTests.Builders;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Enums;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Schedule.Api.IntegrationTests.Controllers
{
    public class TeacherControllerTests : BaseControllerWithDefaultTests
    {
        public TeacherControllerTests(AppFactory<Startup> factory) : base(factory)
        {
        }

        #region Teachers
        [Fact]
        public async Task GetAllTeachers_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            await CreateTeacher();
            var dto = new GetAllTeachersRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Teacher", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllTeacherResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
        }

        [Fact]
        public Task CreateTeacher_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreateTeacher();
        }

        [Fact]
        public async Task UpdateTeacher_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var teacher = await CreateTeacher();
            var dto = new SaveTeacherRequestDtoBuilder()
                .WithDefaults("Haruka", "Nozaki", teacher.PriorityId)
                .Build();

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Teacher/{teacher.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllTeacherResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBeGreaterThan(0);
            apiResponse.Result.PriorityId.ShouldBe(teacher.PriorityId);
            apiResponse.Result.FirstName.ShouldBe(dto.FirstName);
            apiResponse.Result.FirstLastName.ShouldBe(dto.FirstLastName);
            apiResponse.Result.IdentifierNumber.ShouldBe(dto.IdentifierNumber);
        }

        [Fact]
        public async Task DeleteTeacher_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var teacher = await CreateTeacher();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Teacher/{teacher.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
        }
        #endregion

        #region Availabilities
        [Fact]
        public async Task GetAvailability_AtLeastOneAvailabilityExists_ReturnsValidResponseDto()
        {
            //Arrange
            var teacher = await CreateTeacher();
            var period = await CreatePeriod();
            await SaveAvailability(teacher.Id, period.Id);

            //Act
            var response = await HttpClient.GetAsync($"api/Teacher/{teacher.Id}/Availability");
            var apiResponse = await response.Content.ReadAsAsync<ApiListResponseDto<TeacherAvailabilityResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
            apiResponse.Result.ShouldAllBe(a => a.Id > 0 && a.PeriodId == period.Id && a.TeacherId == teacher.Id);
        }

        [Fact]
        public async Task SaveAvailability_ValidRequestDto_ReturnsValidResponseDto()
        {
            var teacher = await CreateTeacher();
            var period = await CreatePeriod();
            await SaveAvailability(teacher.Id, period.Id);
        }
        #endregion

        #region Priorities
        [Fact]
        public async Task GetAllPriorities_AtLeastOneTeacherExists_ReturnsValidResponseDto()
        {
            //Arrange
            await CreatePriority();
            var dto = new GetAllPrioritiesRequestDtoBuilder()
                .WithDefaults()
                .Build();
            var url = SetUrlParameters("api/Teacher/Priorities", dto);

            //Act
            var response = await HttpClient.GetAsync(url);
            var apiResponse = await response.Content.ReadAsAsync<PaginatedResponseDto<GetAllPrioritiesResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
        }

        [Fact]
        public Task CreatePriority_ValidRequestDto_ReturnsValidResponseDto()
        {
            return CreatePriority();
        }

        [Fact]
        public async Task UpdatePriority_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var priority = await CreatePriority();
            var dto = new SavePriorityRequestDto
            {
                HoursToComplete = 8,
                Name = $"MC_PRO_{DateTimeOffset.UtcNow.Ticks}"
            };

            //Act
            var response = await HttpClient.PutAsJsonAsync($"api/Teacher/Priorities/{priority.Id}", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiResponseDto<GetAllPrioritiesResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.Id.ShouldBe(priority.Id);
            apiResponse.Result.Name.ShouldBe(dto.Name);
            apiResponse.Result.HoursToComplete.ShouldBe(dto.HoursToComplete);
        }

        [Fact]
        public async Task DeletePriority_ValidRequestDto_ReturnsValidResponseDto()
        {
            //Arrange
            var priority = await CreatePriority();

            //Act
            var response = await HttpClient.DeleteAsync($"api/Teacher/Priorities/{priority.Id}");
            var apiResponse = await response.Content.ReadAsAsync<EmptyResponseDto>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
        }
        #endregion

        #region Helpers
        private async Task<List<TeacherAvailabilityResponseDto>> SaveAvailability(long teacherId, long periodId)
        {
            //Arrange
            var dto = new SaveTeacherAvailabilityRequestDtoBuilder()
                .ForPeriod(periodId)
                .WithAvailability(LaboralDaysType.Monday, LaboralHoursType.EightAmFortyToNineThirtyAm, LaboralHoursType.ElevenTenAmToTwelvePm)
                .WithAvailability(LaboralDaysType.Friday, LaboralHoursType.ThreeThirtyPmToFourTwentyPm, LaboralHoursType.FiveTenPmToSixPm)
                .Build();

            //Act
            var response = await HttpClient.PostAsJsonAsync($"api/Teacher/{teacherId}/Availability", dto);
            var apiResponse = await response.Content.ReadAsAsync<ApiListResponseDto<TeacherAvailabilityResponseDto>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.ShouldNotBeNull();
            apiResponse.Result.ShouldNotBeNull();
            apiResponse.Succeed.ShouldBeTrue();
            apiResponse.Result.ShouldNotBeEmpty();
            apiResponse.Result.Count.ShouldBe(dto.Availability.Count);
            apiResponse.Result.ShouldAllBe(a => a.Id > 0 && a.PeriodId == periodId && a.TeacherId == teacherId);
            foreach (var item in dto.Availability)
            {
                apiResponse.Result.ShouldContain(a => a.Day == item.Day && a.StartHour == item.StartHour && a.EndHour == item.EndHour);
            }

            return apiResponse.Result;
        }
        #endregion
    }
}
