using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Enums;

namespace Schedule.Api.IntegrationTests.Builders
{
    public class SaveTeacherAvailabilityRequestDtoBuilder : BaseDtoBuilder<SaveTeacherAvailabilityRequestDto>
    {
        public SaveTeacherAvailabilityRequestDtoBuilder ForPeriod(long id)
        {
            Dto.PeriodId = id;
            return this;
        }

        public SaveTeacherAvailabilityRequestDtoBuilder WithAvailability(LaboralDaysType day, LaboralHoursType startHour, LaboralHoursType endHour)
        {
            Dto.Availability.Add(new TeacherAvailabilityRequestDto
            {
                StartHour = startHour,
                EndHour = endHour,
                Day = day
            });
            return this;
        }
    }
}
