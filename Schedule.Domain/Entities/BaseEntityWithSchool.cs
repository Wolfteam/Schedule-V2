namespace Schedule.Domain.Entities
{
    public abstract class BaseEntityWithSchool : BaseEntity
    {
        public long SchoolId { get; set; }
        public School School { get; set; }
    }
}
