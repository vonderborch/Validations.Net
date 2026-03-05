using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotWhiteSpace class provides methods for validation to ensure that
/// a string is not whitespace (null, empty, or contains at least one non-whitespace character).
/// Includes functionality to check, enforce, and validate instances.
/// </summary>
public static class IsNotWhiteSpace
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotWhiteSpace";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be whitespace";

    /// <summary>
    /// Checks if the given value is not whitespace.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is null, empty, or contains at least one non-whitespace character; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return value is null || value.Length == 0 || value.AsSpan().Trim().Length > 0;
    }

    /// <summary>
    /// Ensures the given value is not whitespace, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotWhiteSpace(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotWhiteSpace(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value is not whitespace.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotWhiteSpace(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public sealed class NotWhiteSpaceValidator : IValidator
{
    public static readonly NotWhiteSpaceValidator Instance = new();
    public string Name => IsNotWhiteSpace.ValidatorName;
    public string DefaultFailureMessage => IsNotWhiteSpace.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            string s => s.ValidateIsNotWhiteSpace(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationSuccess()
        };
}

public sealed class ValidateIsNotWhiteSpaceAttribute() : ValidatorAttribute(NotWhiteSpaceValidator.Instance);
