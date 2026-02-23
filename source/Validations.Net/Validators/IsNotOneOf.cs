using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotOneOf class provides methods for validation to ensure that
/// a value is not one of a set of disallowed values. Includes functionality to check, enforce,
/// and validate instances where exclusion from a set is required.
/// </summary>
public static class IsNotOneOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotOneOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be one of the disallowed values";

    /// <summary>
    /// Checks if the given value is not one of the disallowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, params T[] disallowedValues)
    {
        ArgumentNullException.ThrowIfNull(disallowedValues);
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < disallowedValues.Length; i++)
        {
            if (comparer.Equals(value, disallowedValues[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the given value is not one of the disallowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, IEnumerable<T> disallowedValues)
    {
        ArgumentNullException.ThrowIfNull(disallowedValues);
        var comparer = EqualityComparer<T>.Default;
        foreach (var disallowed in disallowedValues)
        {
            if (comparer.Equals(value, disallowed))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given value is not one of the disallowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, params T[] disallowedValues)
    {
        return value.ValidateIsNotOneOf((IEnumerable<T>)disallowedValues);
    }

    /// <summary>
    /// Validates whether the given value is not one of the disallowed values.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, IEnumerable<T> disallowedValues, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotOneOf(disallowedValues))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not one of the disallowed values, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotOneOf<T>(this T? value, params T[] disallowedValues)
    {
        return value.EnsureIsNotOneOf((IEnumerable<T>)disallowedValues);
    }

    /// <summary>
    /// Ensures the given value is not one of the disallowed values, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotOneOf<T>(this T? value, IEnumerable<T> disallowedValues, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotOneOf(disallowedValues, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsNotOneOfValidator : IValidator
{
    public object[] Values { get; }

    public IsNotOneOfValidator(params object[] values)
    {
        Values = values;
    }

    public string Name => IsNotOneOf.ValidatorName;
    public string DefaultFailureMessage => IsNotOneOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotOneOf(Values))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}

public sealed class ValidateIsNotOneOfAttribute(params object[] values)
    : ValidatorAttribute(new IsNotOneOfValidator(values));
