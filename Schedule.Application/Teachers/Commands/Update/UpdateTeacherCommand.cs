using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Commands.Update
{
    public class UpdateTeacherCommand : BaseApiRequest<SaveTeacherRequestDto, GetAllTeacherResponseDto>
    {
        public long Id { get; }
        public UpdateTeacherCommand(long id, SaveTeacherRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
