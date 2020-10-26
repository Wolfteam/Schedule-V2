using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Resources;
using Schedule.Domain.Enums;

namespace Schedule.Shared.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithGlobalMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            string errorMessage)
        {
            foreach (var item in (rule as RuleBuilder<T, TProperty>).Rule.Validators)
                item.Options.ErrorMessageSource = new StaticStringSource(errorMessage);

            return rule;
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            AppMessageType error)
        {
            return rule.WithGlobalMessage(error.GetErrorMsg());
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            string errorCode)
        {
            foreach (var item in (rule as RuleBuilder<T, TProperty>).Rule.Validators)
                item.Options.ErrorCodeSource = new StaticStringSource(errorCode);

            return rule;
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            AppMessageType error)
        {
            return rule.WithGlobalErrorCode(error.GetErrorCode());
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalInvalidRequestErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithGlobalErrorCode(AppMessageType.SchApiInvalidRequest.GetErrorCode());
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalErrorMsgAndCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            string errorMessage,
            string errorCode)
        {
            foreach (var item in (rule as RuleBuilder<T, TProperty>).Rule.Validators)
            {
                item.Options.ErrorMessageSource = new StaticStringSource(errorMessage);
                item.Options.ErrorCodeSource = new StaticStringSource(errorCode);
            }

            return rule;
        }

        public static IRuleBuilderOptions<T, TProperty> WithGlobalErrorMsgAndCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            AppMessageType error)
        {
            return rule.WithGlobalErrorMsgAndCode(error.GetErrorMsg(), error.GetErrorCode());
        }
    }
}
