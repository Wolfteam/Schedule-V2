using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Classrooms.Commands.Create
{
    public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
    {
        public CreateClassroomCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Capacity)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.ClassroomSubjectId)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
