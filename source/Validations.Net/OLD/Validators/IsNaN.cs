using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNaN class provides methods for validation to ensure that
/// a floating-point value is NaN (Not a Number).
/// Includes functionality to check, enforce, and validate instances where a NaN value is required.
/// </summary>
public static class IsNaN
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNaN";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be NaN";

    /// <summary>
    /// Checks if the given value is NaN.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is NaN; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNaN<T>(this T value) where T : INumber<T>
    {
        return T.IsNaN(value);
    }

    /// <summary>
    /// Validates whether the given value is NaN.
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
    public static ValidationResult ValidateIsNaN<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsNaN())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is NaN, throwing an exception if validation fails.
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
    /// Thrown when the validation fails and the value is not NaN.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNaN<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsNaN(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NaNValidator : IValidator
{
    public static readonly NaNValidator Instance = new();
    public string Name => IsNaN.ValidatorName;
    public string DefaultFailureMessage => IsNaN.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            double v => v.ValidateIsNaN(blackboard, this.DefaultFailureMessage, memberName),
            float v => v.ValidateIsNaN(blackboard, this.DefaultFailureMessage, memberName),
            Half v => v.ValidateIsNaN(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsNaNAttribute() : ValidatorAttribute(NaNValidator.Instance);
