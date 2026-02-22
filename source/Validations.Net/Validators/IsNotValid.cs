using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.Validation;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that an object instance fails at least one ValidationAttribute declared on its type.
/// The logical inverse of IsValid.
/// </summary>
public static class IsNotValid
{
    public const string ValidatorName = "IsNotValid";
    public const string DefaultValidationFailureMessage = "Object passed all validations but was expected to be invalid";

    /// <summary>
    /// Checks whether the given instance fails at least one of its ValidationAttributes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T? value)
    {
        if (value is null) return true;
        var result = ValidationRunner.Validate(value);
        return !result.IsValid;
    }

    /// <summary>
    /// Validates that the given instance has at least one failing ValidationAttribute.
    /// Returns success when at least one attribute fails; returns failure when all pass.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValid<T>(this T? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        var aggregateResult = ValidationRunner.Validate(value, blackboard);
        if (!aggregateResult.IsValid)
            return ValidationResult.CreateFromValidationSuccess();

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName, validationFailureMessage, parameterName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }

    /// <summary>
    /// Ensures the given instance fails at least one ValidationAttribute,
    /// throwing a ValidationException if all validations pass.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotValid<T>(this T? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotValid(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;

        return value;
    }
}
