using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class PeriodSection : BaseEntity
    {
        public int NumerOfSections { get; set; }
        public int MaxNumberOfStudents { get; set; }

        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long PeriodId { get; set; }
        public Period Period { get; set; }
        public ICollection<TeacherSchedule> TeacherSchedules { get; } = new List<TeacherSchedule>();
    }
}
