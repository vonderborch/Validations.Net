using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using System.Numerics;

namespace Validations.Net.Validators;

/// <summary>
/// The IsFinite class provides methods for validation to ensure that
/// a floating-point value is finite (not NaN, not positive or negative infinity).
/// Includes functionality to check, enforce, and validate instances where a finite value is required.
/// </summary>
public static class IsFinite
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsFinite";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be finite";

    /// <summary>
    /// Checks if the given value is finite.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is finite; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsFinite<T>(this T value) where T : INumber<T>
    {
        return T.IsFinite(value);
    }

    /// <summary>
    /// Validates whether the given value is finite.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails. Defaults to the default failure message.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsFinite<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsFinite())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is finite, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails and the value is not finite.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsFinite<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsFinite(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class FiniteValidator : IValidator
{
    public static readonly FiniteValidator Instance = new();
    public string Name => IsFinite.ValidatorName;
    public string DefaultFailureMessage => IsFinite.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            double v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsFiniteAttribute() : ValidatorAttribute(FiniteValidator.Instance);
