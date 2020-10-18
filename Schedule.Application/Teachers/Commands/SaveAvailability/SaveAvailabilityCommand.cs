using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Commands.SaveAvailability
{
    public class SaveAvailabilityCommand : BaseApiListRequest<SaveTeacherAvailabilityRequestDto, TeacherAvailabilityResponseDto>
    {
        public long TeacherId { get; }
        public SaveAvailabilityCommand(long teacherId, SaveTeacherAvailabilityRequestDto dto) : base(dto)
        {
            TeacherId = teacherId;
        }
    }
}
