using System.Runtime.CompilerServices;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationSets.BuilderExtensions;

public static class IsNotNullOrWhiteSpaceBuilderExtensions
{
    public static ValidationSetBuilder<T> AddIsNotNullOrWhiteSpace<T>(this ValidationSetBuilder<T> builder,
        Func<T, string?> selector, string? message = null,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        return builder.Add((value, bb) =>
        {
            var str = selector(value);
            if (!string.IsNullOrWhiteSpace(str))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNullOrWhiteSpace.ValidatorName, message ?? IsNotNullOrWhiteSpace.DefaultValidationFailureMessage,
                memberPath, bb, [("value", str)]);
        }, severity);
    }
}
