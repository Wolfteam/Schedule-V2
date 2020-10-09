using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Priority : BaseEntity
    {
        public string Name { get; set; }
        public int HoursToComplete { get; set; }

        public ICollection<Teacher> Teachers { get; } = new List<Teacher>();
    }
}
