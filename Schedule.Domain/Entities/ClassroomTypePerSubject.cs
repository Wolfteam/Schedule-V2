using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class ClassroomTypePerSubject : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Classroom> Classrooms { get; } = new List<Classroom>();
        public ICollection<Subject> Subjects { get; } = new List<Subject>();
    }
}
