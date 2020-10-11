namespace Schedule.Domain.Dto.Classrooms.Responses
{
    public class GetAllClassroomsResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public long ClassroomTypePerSubjectId { get; set; }
        public string ClassroomType { get; set; }
    }
}
