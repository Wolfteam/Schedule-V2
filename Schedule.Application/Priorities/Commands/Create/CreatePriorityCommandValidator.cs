using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Priorities.Commands.Create
{
    public class CreatePriorityCommandValidator : AbstractValidator<CreatePriorityCommand>
    {
        public CreatePriorityCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.HoursToComplete)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
