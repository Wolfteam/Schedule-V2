using Schedule.Domain.Enums;

namespace Schedule.Domain.Dto.Teachers.Responses
{
    public class TeacherAvailabilityResponseDto
    {
        public long Id { get; set; }
        public LaboralDaysType Day { get; set; }
        public LaboralHoursType StartHour { get; set; }
        public LaboralHoursType EndHour { get; set; }

        public long TeacherId { get; set; }
        public long PeriodId { get; set; }
    }
}
