using Schedule.Domain.Dto.Subjects.Requests;

namespace Schedule.Api.IntegrationTests.Builders
{
    public class SaveSubjectRequestDtoBuilder : BaseDtoBuilder<SaveSubjectRequestDto>
    {
        public SaveSubjectRequestDtoBuilder WithDefaults(int code, string name)
        {
            Dto.Code = code;
            Dto.Name = name;
            return this;
        }

        public SaveSubjectRequestDtoBuilder WithHours(int academicHours, int totalAcademicHours)
        {
            Dto.AcademicHoursPerWeek = academicHours;
            Dto.TotalAcademicHours = totalAcademicHours;
            return this;
        }

        public SaveSubjectRequestDtoBuilder WithRelations(
            long semesterId,
            long careerId,
            long classroomTypePerSubjectId)
        {
            Dto.SemesterId = semesterId;
            Dto.CareerId = careerId;
            Dto.ClassroomTypePerSubjectId = classroomTypePerSubjectId;
            return this;
        }
    }
}
