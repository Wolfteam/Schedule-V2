using System.Collections.Generic;
using Schedule.Domain.Enums;

namespace Schedule.Domain.Dto.Teachers.Requests
{
    public class SaveTeacherAvailabilityRequestDto
    {
        public List<TeacherAvailabilityRequestDto> Availability { get; set; } = new List<TeacherAvailabilityRequestDto>();
    }

    public class TeacherAvailabilityRequestDto
    {
        public LaboralDaysType Day { get; set; }
        public LaboralHoursType StartHour { get; set; }
        public LaboralHoursType EndHour { get; set; }
    }
}
