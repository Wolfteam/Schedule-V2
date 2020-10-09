using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Career : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
