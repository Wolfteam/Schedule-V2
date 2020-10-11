namespace Schedule.Domain.Dto.Subjects.Responses
{
    public class GetAllSubjectsResponseDto
    {
        public long Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int TotalAcademicHours { get; set; }
        public int AcademicHoursPerWeek { get; set; }
        public long CareerId { get; set; }
        public string Career { get; set; }
        public long SemesterId { get; set; }
        public string Semester { get; set; }
        public long ClassroomTypePerSubjectId { get; set; }
        public string ClassroomType { get; set; }
    }
}
