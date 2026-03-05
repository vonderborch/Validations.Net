using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsPositive class provides methods for validation to ensure that
/// a numeric value is positive. Includes functionality to check, enforce,
/// and validate instances where a positive value is required.
/// </summary>
public static class IsPositive
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsPositive";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be positive";

    /// <summary>
    /// Checks if the given value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is positive; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPositive<T>(this T value) where T : INumber<T>
    {
        return T.IsPositive(value);
    }

    /// <summary>
    /// Validates whether the given value is positive.
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
    public static ValidationResult ValidateIsPositive<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsPositive())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is positive, throwing an exception if validation fails.
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
    /// Thrown when the validation fails and the value is not positive.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsPositive<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsPositive(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class PositiveValidator : IValidator
{
    public static readonly PositiveValidator Instance = new();
    public string Name => IsPositive.ValidatorName;
    public string DefaultFailureMessage => IsPositive.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            long v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            short v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            byte v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            uint v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            nint v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            double v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            float v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            Half v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsPositive(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
