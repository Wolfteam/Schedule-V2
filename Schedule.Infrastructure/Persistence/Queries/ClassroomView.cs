using System;

namespace Schedule.Infrastructure.Persistence.Queries
{
    public class ClassroomView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public long SchoolId { get; set; }
        public long ClassroomSubjectId { get; set; }
        public string ClassroomSubject { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtString { get; set; }

        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
