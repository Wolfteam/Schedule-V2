using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Semester : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Subject> Subjects { get; } = new List<Subject>();
    }
}
