using System.Runtime.CompilerServices;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationSets.BuilderExtensions;

public static class IsInRangeBuilderExtensions
{
    public static ValidationSetBuilder<T> AddIsInRange<T, TValue>(this ValidationSetBuilder<T> builder,
        Func<T, TValue> selector, TValue min, TValue max,
        bool minInclusive = true, bool maxInclusive = true, string? message = null,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null) where TValue : IComparable<TValue>
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        return builder.Add((value, bb) =>
        {
            var member = selector(value);
            if (member is not null && member.CheckIsInRange(min, max, minInclusive, maxInclusive))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsInRange.ValidatorName, message ?? IsInRange.DefaultValidationFailureMessage,
                memberPath, bb, [("value", member), ("min", min), ("max", max)]);
        }, severity);
    }
}
