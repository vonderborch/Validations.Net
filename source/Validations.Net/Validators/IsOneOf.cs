using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsOneOf class provides methods for validation to ensure that
/// a value is one of a set of allowed values. Includes functionality to check, enforce,
/// and validate instances where membership in a set is required.
/// </summary>
public static class IsOneOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsOneOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be one of the allowed values";

    /// <summary>
    /// Checks if the given value is one of the allowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, params T[] allowedValues)
    {
        ArgumentNullException.ThrowIfNull(allowedValues);
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < allowedValues.Length; i++)
        {
            if (comparer.Equals(value, allowedValues[i]))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the given value is one of the allowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, IEnumerable<T> allowedValues)
    {
        ArgumentNullException.ThrowIfNull(allowedValues);
        var comparer = EqualityComparer<T>.Default;
        foreach (var allowed in allowedValues)
        {
            if (comparer.Equals(value, allowed))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Validates whether the given value is one of the allowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOneOf<T>(this T? value, params T[] allowedValues)
    {
        return value.ValidateIsOneOf((IEnumerable<T>)allowedValues);
    }

    /// <summary>
    /// Validates whether the given value is one of the allowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOneOf<T>(this T? value, IEnumerable<T> allowedValues, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsOneOf(allowedValues))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is one of the allowed values, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsOneOf<T>(this T? value, params T[] allowedValues)
    {
        return value.EnsureIsOneOf((IEnumerable<T>)allowedValues);
    }

    /// <summary>
    /// Ensures the given value is one of the allowed values, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsOneOf<T>(this T? value, IEnumerable<T> allowedValues, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsOneOf(allowedValues, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsOneOfValidator : IValidator
{
    public object[] Values { get; }

    public IsOneOfValidator(params object[] values)
    {
        Values = values;
    }

    public string Name => IsOneOf.ValidatorName;
    public string DefaultFailureMessage => IsOneOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsOneOf(Values))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}

public sealed class ValidateIsOneOfAttribute(params object[] values)
    : ValidatorAttribute(new IsOneOfValidator(values));
