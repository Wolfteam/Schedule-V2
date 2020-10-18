using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Commands.UpdateType
{
    public class UpdateClassroomTypeCommand : BaseApiRequest<SaveClassroomTypeRequestDto, GetAllClassroomTypesResponseDto>
    {
        public long Id { get; }

        public UpdateClassroomTypeCommand(long id, SaveClassroomTypeRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
