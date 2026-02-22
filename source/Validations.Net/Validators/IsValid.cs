using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.Validation;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that an object instance passes all ValidationAttributes declared on its type
/// (class-level, property-level, and field-level), including nested and collection validation.
/// </summary>
public static class IsValid
{
    public const string ValidatorName = "IsValid";
    public const string DefaultValidationFailureMessage = "Object failed validation";

    /// <summary>
    /// Checks whether the given instance passes all of its ValidationAttributes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? value)
    {
        if (value is null) return false;
        var result = ValidationRunner.Validate(value);
        return result.IsValid;
    }

    /// <summary>
    /// Validates the given instance against all of its ValidationAttributes and returns
    /// an aggregate result containing all failures.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AggregateValidationResult ValidateIsValid<T>(this T? value,
        IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value is null)
        {
            var builder = AggregateValidationResult.CreateBuilder();
            var failResult = ValidationResult.CreateFromValidationFailure(
                ValidatorName, "Instance is null", parameterName, blackboard,
                new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", failResult);
            return builder.Build();
        }

        return ValidationRunner.Validate(value, blackboard);
    }

    /// <summary>
    /// Ensures the given instance passes all of its ValidationAttributes,
    /// throwing a ValidationException if any fail.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsValid<T>(this T? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var aggregateResult = value.ValidateIsValid(blackboard, parameterName);
        if (!aggregateResult.IsValid)
        {
            var failures = aggregateResult.Failures;
            if (failures.Count > 0 && failures[0].Result.ValidationException is { } validationEx)
                throw validationEx;

            throw ValidationException.Create(ValidatorName, validationFailureMessage, parameterName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("failureCount", failures.Count) });
        }

        return value;
    }
}
