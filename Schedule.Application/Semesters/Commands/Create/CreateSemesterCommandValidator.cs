using FluentValidation;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Semesters.Commands.Create
{
    public class CreateSemesterCommandValidator : AbstractValidator<CreateSemesterCommand>
    {
        public CreateSemesterCommandValidator()
        {
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalInvalidRequestErrorCode();
        }
    }
}
