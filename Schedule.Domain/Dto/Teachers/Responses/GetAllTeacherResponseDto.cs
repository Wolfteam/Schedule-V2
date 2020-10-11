namespace Schedule.Domain.Dto.Teachers.Responses
{
    public class GetAllTeacherResponseDto
    {
        public long Id { get; set; }
        public int IdentifierNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public long PriorityId { get; set; }
        public string Priority { get; set; }
    }
}
