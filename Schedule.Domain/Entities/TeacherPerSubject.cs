namespace Schedule.Domain.Entities
{
    public class TeacherPerSubject : BaseEntity
    {
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
