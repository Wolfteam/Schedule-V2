using FluentValidation;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Semesters.Commands.Update
{
    public class UpdateSemesterCommandValidator : AbstractValidator<UpdateSemesterCommand>
    {
        public UpdateSemesterCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithGlobalInvalidRequestErrorCode();

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalInvalidRequestErrorCode();
        }
    }
}
