using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides utility methods for validating whether a value is null.
/// Includes methods for checks, ensuring null values, and validation result generation.
/// </summary>
public static class IsNull
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNull";
    
    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be null";

    /// <summary>
    /// Checks if the given value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is null; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNull<T>(this T? value)
    {
        return value is null;
    }

    /// <summary>
    /// Ensures the given value is null, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails. Defaults to "Parameter must be null".</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful (i.e., the value is null).
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails and the value is not null.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNull<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNull(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails. Defaults to "Parameter must be null".</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNull<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNull())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public sealed class NullValidator : IValidator
{
    public static readonly NullValidator Instance = new();
    public string Name => IsNull.ValidatorName;
    public string DefaultFailureMessage => IsNull.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsNull(blackboard, DefaultFailureMessage, memberName);
}

public sealed class ValidateIsNullAttribute() : ValidatorAttribute(NullValidator.Instance);
