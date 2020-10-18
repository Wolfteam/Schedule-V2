using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Classroom : BaseEntityWithSchool
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public long ClassroomSubjectId { get; set; }
        public ClassroomSubject ClassroomSubject { get; set; }
        public ICollection<TeacherSchedule> TeacherSchedules { get; set; } = new List<TeacherSchedule>();
    }
}
