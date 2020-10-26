namespace Schedule.Domain.Dto.Classrooms.Requests
{
    public class SaveClassroomRequestDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public long ClassroomSubjectId { get; set; }
    }
}
