using System;

namespace Schedule.Infrastructure.Persistence.Queries
{
    public class SubjectView
    {
        public long Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int TotalAcademicHours { get; set; }
        public int AcademicHoursPerWeek { get; set; }

        public string Career { get; set; }
        public long CareerId { get; set; }
        public long SemesterId { get; set; }
        public string Semester { get; set; }
        public long ClassroomTypePerSubjectId { get; set; }
        public string ClassroomType { get; set; }
        public long SchoolId { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtString { get; set; }

        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
