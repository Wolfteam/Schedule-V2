namespace Schedule.Application.Subjects.Queries.GetAllTeachers
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
    }
}
