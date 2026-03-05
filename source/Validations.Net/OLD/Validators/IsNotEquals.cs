using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotEquals class provides methods for validation to ensure that
/// a value does not equal an expected value. Includes functionality to check, enforce,
/// and validate instances where inequality is required.
/// </summary>
public static class IsNotEquals
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotEquals";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not equal the expected value";

    /// <summary>
    /// Checks if the given value does not equal the expected value (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals(this object? value, object? expected)
    {
        return !Equals(value, expected);
    }

    /// <summary>
    /// Checks if the given value does not equal the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The value that must not match.</param>
    /// <returns>
    /// True if the value does not equal the expected; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? expected) where T : IEquatable<T>
    {
        return !EqualityComparer<T>.Default.Equals(value, expected);
    }

    /// <summary>
    /// Checks if the given value is not approximately equal to the expected value within the specified tolerance.
    /// Useful for floating-point comparisons where exact equality may fail due to rounding.
    /// </summary>
    /// <typeparam name="T">A numeric type implementing <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The value that must not match.</param>
    /// <param name="tolerance">The minimum absolute difference required between <paramref name="value"/> and <paramref name="expected"/>.</param>
    /// <returns>True if the absolute difference is greater than <paramref name="tolerance"/>; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T value, T expected, T tolerance) where T : INumber<T>
    {
        return T.Abs(value - expected) > tolerance;
    }

    /// <summary>
    /// Validates whether the given value does not equal the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotEquals<T>(this T? value, T? expected, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (!value.CheckIsNotEquals(expected))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expected", expected)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value does not equal the expected value, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotEquals<T>(this T? value, T? expected, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var validationResult = value.ValidateIsNotEquals(expected, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value is not approximately equal to the expected value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotEquals<T>(this T value, T expected, T tolerance, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>
    {
        if (!value.CheckIsNotEquals(expected, tolerance))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expected", expected), ("tolerance", tolerance)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not approximately equal to the expected value within the specified tolerance,
    /// throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNotEquals<T>(this T value, T expected, T tolerance, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>
    {
        var validationResult = value.ValidateIsNotEquals(expected, tolerance, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NotEqualsValidator : IValidator
{
    public object? Expected { get; }

    public NotEqualsValidator(object? expected)
    {
        this.Expected = expected;
    }

    public string Name => IsNotEquals.ValidatorName;
    public string DefaultFailureMessage => IsNotEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotEquals(this.Expected))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expected", this.Expected)]);
    }
}

public sealed class ValidateIsNotEqualsAttribute(object? expected)
    : ValidatorAttribute(new NotEqualsValidator(expected));
