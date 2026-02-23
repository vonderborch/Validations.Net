using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

public static class MustBuilderExtensions
{
    public static ValidationSetBuilder<T> AddMust<T>(this ValidationSetBuilder<T> builder,
        Func<T, bool> predicate, string message,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(predicate))] string? predicateExpression = null)
    {
        return builder.Add((value, bb) =>
        {
            if (predicate(value))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                "Must", message, predicateExpression, bb,
                [("value", value)]);
        }, severity);
    }

    public static ValidationSetBuilder<T> AddMust<T, TMember>(this ValidationSetBuilder<T> builder,
        Func<T, TMember> selector, Func<TMember, bool> predicate,
        string message,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        return builder.Add((value, bb) =>
        {
            var member = selector(value);
            if (predicate(member))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                "Must", message, memberPath, bb,
                [("value", member)]);
        }, severity);
    }
}
