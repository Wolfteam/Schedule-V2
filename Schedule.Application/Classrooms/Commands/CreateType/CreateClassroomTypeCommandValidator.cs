using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Classrooms.Commands.CreateType
{
    public class CreateClassroomTypeCommandValidator : AbstractValidator<CreateClassroomTypeCommand>
    {
        public CreateClassroomTypeCommandValidator()
        {
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(AppMessageType.SchInvalidRequest);
        }
    }
}
