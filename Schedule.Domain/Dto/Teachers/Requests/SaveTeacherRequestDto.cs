namespace Schedule.Domain.Dto.Teachers.Requests
{
    public class SaveTeacherRequestDto
    {
        public int IdentifierNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }

        public long PriorityId { get; set; }
    }
}
