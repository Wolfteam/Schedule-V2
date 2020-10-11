using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Teachers.Commands.Create
{
    public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            var error = AppMessageType.SchInvalidRequest;
            RuleFor(cmd => cmd.Dto.IdentifierNumber)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.SecondName)
                .MaximumLength(50)
                .When(cmd => !string.IsNullOrEmpty(cmd.Dto.SecondName))
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.FirstLastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.SecondLastName)
                .MaximumLength(50)
                .When(cmd => !string.IsNullOrEmpty(cmd.Dto.SecondLastName))
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.PriorityId)
                .NotEmpty()
                .WithGlobalErrorCode(error);
        }
    }
}
