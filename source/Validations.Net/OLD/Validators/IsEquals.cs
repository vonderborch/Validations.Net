using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsEquals class provides methods for validation to ensure that
/// a value equals an expected value. Includes functionality to check, enforce,
/// and validate instances where equality is required.
/// </summary>
public static class IsEquals
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsEquals";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must equal the expected value";

    /// <summary>
    /// Checks if the given value equals the expected value (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals(this object? value, object? expected)
    {
        return Equals(value, expected);
    }

    /// <summary>
    /// Checks if the given value equals the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>
    /// True if the value equals the expected; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? expected) where T : IEquatable<T>
    {
        return EqualityComparer<T>.Default.Equals(value, expected);
    }

    /// <summary>
    /// Checks if the given value is approximately equal to the expected value within the specified tolerance.
    /// Useful for floating-point comparisons where exact equality may fail due to rounding.
    /// </summary>
    /// <typeparam name="T">A numeric type implementing <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="tolerance">The maximum allowed absolute difference between <paramref name="value"/> and <paramref name="expected"/>.</param>
    /// <returns>True if the absolute difference is less than or equal to <paramref name="tolerance"/>; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T value, T expected, T tolerance) where T : INumber<T>
    {
        return T.Abs(value - expected) <= tolerance;
    }

    /// <summary>
    /// Validates whether the given value equals the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEquals<T>(this T? value, T? expected, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (!value.CheckIsEquals(expected))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expected", expected)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value equals the expected value, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsEquals<T>(this T? value, T? expected, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var validationResult = value.ValidateIsEquals(expected, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value is approximately equal to the expected value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEquals<T>(this T value, T expected, T tolerance, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>
    {
        if (!value.CheckIsEquals(expected, tolerance))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expected", expected), ("tolerance", tolerance)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is approximately equal to the expected value within the specified tolerance,
    /// throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsEquals<T>(this T value, T expected, T tolerance, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>
    {
        var validationResult = value.ValidateIsEquals(expected, tolerance, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsEqualsValidator : IValidator
{
    public object? Expected { get; }

    public IsEqualsValidator(object? expected)
    {
        this.Expected = expected;
    }

    public string Name => IsEquals.ValidatorName;
    public string DefaultFailureMessage => IsEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsEquals(this.Expected))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expected", this.Expected)]);
    }
}

public sealed class ValidateIsEqualsAttribute(object? expected)
    : ValidatorAttribute(new IsEqualsValidator(expected));
