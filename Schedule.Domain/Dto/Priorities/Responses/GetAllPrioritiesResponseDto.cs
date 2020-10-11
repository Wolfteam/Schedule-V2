namespace Schedule.Domain.Dto.Priorities.Responses
{
    public class GetAllPrioritiesResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int HoursToComplete { get; set; }
    }
}
