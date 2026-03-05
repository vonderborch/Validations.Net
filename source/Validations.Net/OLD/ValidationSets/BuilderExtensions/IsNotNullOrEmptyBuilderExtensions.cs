using System.Runtime.CompilerServices;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationSets.BuilderExtensions;

public static class IsNotNullOrEmptyBuilderExtensions
{
    public static ValidationSetBuilder<T> AddIsNotNullOrEmpty<T>(this ValidationSetBuilder<T> builder,
        Func<T, string?> selector, string? message = null,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        return builder.Add((value, bb) =>
        {
            var str = selector(value);
            if (!string.IsNullOrEmpty(str))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNullOrEmpty.ValidatorName, message ?? IsNotNullOrEmpty.DefaultValidationFailureMessage,
                memberPath, bb, [("value", str)]);
        }, severity);
    }
}
