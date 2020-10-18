using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Commands.CreateType
{
    public class CreateClassroomTypeCommand : BaseApiRequest<SaveClassroomTypeRequestDto, GetAllClassroomTypesResponseDto>
    {
        public CreateClassroomTypeCommand(SaveClassroomTypeRequestDto dto) : base(dto)
        {
        }
    }
}
