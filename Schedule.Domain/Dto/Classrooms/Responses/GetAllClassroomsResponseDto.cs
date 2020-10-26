namespace Schedule.Domain.Dto.Classrooms.Responses
{
    public class GetAllClassroomsResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public long ClassroomSubjectId { get; set; }
        public string ClassroomSubject { get; set; }
    }
}
