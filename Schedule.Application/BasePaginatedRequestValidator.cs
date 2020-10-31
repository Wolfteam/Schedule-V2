using FluentValidation;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Domain.Interfaces.Dto;
using Schedule.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Schedule.Application
{
    public abstract class BasePaginatedRequestValidator<TDto> : AbstractValidator<BaseRequest<TDto>>
        where TDto : IPaginatedRequestDto
    {
        public abstract IReadOnlyList<string> OrderByOnLy { get; }

        protected BasePaginatedRequestValidator(
            string searchTermRegex = @"[^\w\./ @+-]",
            AppMessageType errorCode = AppMessageType.SchApiInvalidRequest)
        {
            var eCode = errorCode.GetErrorCode();
            RuleFor(query => query.Dto.Page)
                .GreaterThan(0)
                .WithGlobalErrorCode(eCode);

            RuleFor(query => query.Dto.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithErrorCode(eCode);

            RuleFor(query => query.Dto.SearchTerm)
                .Must(searchTerm => !Regex.IsMatch(searchTerm, searchTermRegex))
                .When(query => !string.IsNullOrEmpty(query.Dto.SearchTerm))
                .WithGlobalErrorMsgAndCode($"{nameof(PaginatedRequestDto.SearchTerm)} is not valid", eCode);

            RuleFor(query => query.Dto.OrderBy)
                .Must(ValidateOrderByProperties)
                .When(query => !string.IsNullOrEmpty(query.Dto.OrderBy))
                .WithGlobalErrorMsgAndCode(
                    $"{nameof(PaginatedRequestDto.OrderBy)} is not valid. " +
                    $"The allowed properties are: {string.Join(",", OrderByOnLy)}",
                    eCode);

            RuleFor(query => query.Dto.Language)
                .IsInEnum()
                .WithErrorCode(eCode);
        }

        public virtual bool ValidateOrderByProperties(string prop)
            => OrderByOnLy.Any(orderBy => orderBy.Equals(prop, StringComparison.OrdinalIgnoreCase));
    }
}
