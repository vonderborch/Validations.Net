using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationSets;

public static class IsNotNullBuilderExtensions
{
    public static ValidationSetBuilder<T> AddIsNotNull<T>(this ValidationSetBuilder<T> builder,
        string? message = null,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        return builder.Add((value, bb) =>
        {
            if (value is not null)
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNull.ValidatorName, message ?? IsNotNull.DefaultValidationFailureMessage,
                null, bb, [("value", value)]);
        }, severity);
    }

    public static ValidationSetBuilder<T> AddIsNotNull<T, TMember>(this ValidationSetBuilder<T> builder,
        Func<T, TMember?> selector, string? message = null,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        return builder.Add((value, bb) =>
        {
            var member = selector(value);
            if (member is not null)
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNull.ValidatorName, message ?? IsNotNull.DefaultValidationFailureMessage,
                memberPath, bb, [("value", member)]);
        }, severity);
    }
}
