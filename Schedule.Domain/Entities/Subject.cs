using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int TotalAcademicHours { get; set; }
        public int AcademicHoursPerWeek { get; set; }

        public long CareerId { get; set; }
        public Career Career { get; set; }

        public long SemesterId { get; set; }
        public Semester Semester { get; set; }

        public long ClassroomTypePerSubjectId { get; set; }
        public ClassroomTypePerSubject ClassroomTypePerSubject { get; set; }
        public ICollection<TeacherPerSubject> Teachers { get; } = new List<TeacherPerSubject>();

        public static Subject NewSubject(int code, string name, long semesterId, long classroomTypeId, long careerId, int totalHours, int hoursPerWeek)
        {
            return new Subject
            {
                Code = code,
                Name = name,
                SemesterId = semesterId,
                ClassroomTypePerSubjectId = classroomTypeId,
                CareerId = careerId,
                TotalAcademicHours = totalHours,
                AcademicHoursPerWeek = hoursPerWeek
            };
        }
    }
}
