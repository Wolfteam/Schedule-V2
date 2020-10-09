using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Period : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TeacherAvailability> TeacherAvailabilities { get; } = new List<TeacherAvailability>();
        public ICollection<TeacherSchedule> TeacherSchedules { get; } = new List<TeacherSchedule>();
        public ICollection<PeriodSection> PeriodSections { get; } = new List<PeriodSection>();
    }
}
