using FluentValidation;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Careers.Commands.Update
{
    public class UpdateCareerCommandValidator : AbstractValidator<UpdateCareerCommand>
    {
        public UpdateCareerCommandValidator()
        {
            RuleFor(dto => dto.Id)
                .GreaterThan(0)
                .WithGlobalInvalidRequestErrorCode();

            RuleFor(dto => dto.Dto.Name)
                .NotEmpty()
                .MaximumLength(255)
                .WithGlobalInvalidRequestErrorCode();
        }
    }
}
