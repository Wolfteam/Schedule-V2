using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public long ClassroomTypePerSubjectId { get; set; }
        public ClassroomTypePerSubject ClassroomTypePerSubject { get; set; }
        public ICollection<TeacherSchedule> TeacherSchedules { get; set; } = new List<TeacherSchedule>();
    }
}
