﻿using Schedule.Domain.Enums;

namespace Schedule.Domain.Entities
{
    public class TeacherAvailability : BaseEntity
    {
        public LaboralDaysType Day { get; set; }
        public LaboralHoursType StartHour { get; set; }
        public LaboralHoursType EndHour { get; set; }

        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public long PeriodId { get; set; }
        public Period Period { get; set; }
    }
}
