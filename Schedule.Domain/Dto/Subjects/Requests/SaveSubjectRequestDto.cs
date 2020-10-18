namespace Schedule.Domain.Dto.Subjects.Requests
{
    public class SaveSubjectRequestDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int TotalAcademicHours { get; set; }
        public int AcademicHoursPerWeek { get; set; }

        public long CareerId { get; set; }
        public long SemesterId { get; set; }
        public long ClassroomTypePerSubjectId { get; set; }
    }
}
