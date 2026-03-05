using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsZero class provides methods for validation to ensure that
/// a numeric value is zero. Includes functionality to check, enforce,
/// and validate instances where a zero value is required.
/// </summary>
public static class IsZero
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsZero";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be zero";

    /// <summary>
    /// Checks if the given value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is zero; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsZero<T>(this T value) where T : INumber<T>
    {
        return value == T.Zero;
    }

    /// <summary>
    /// Validates whether the given value is zero.
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
    public static ValidationResult ValidateIsZero<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsZero())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is zero, throwing an exception if validation fails.
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
    /// Thrown when the validation fails and the value is not zero.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsZero<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsZero(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ZeroValidator : IValidator
{
    public static readonly ZeroValidator Instance = new();
    public string Name => IsZero.ValidatorName;
    public string DefaultFailureMessage => IsZero.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            long v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            short v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            byte v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            uint v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            nint v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            double v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            float v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            Half v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsZero(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
