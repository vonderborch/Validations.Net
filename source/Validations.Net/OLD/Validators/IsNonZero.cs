using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNonZero class provides methods for validation to ensure that
/// a numeric value is not zero. Includes functionality to check, enforce,
/// and validate instances where a non-zero value is required.
/// </summary>
public static class IsNonZero
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNonZero";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be zero";

    /// <summary>
    /// Checks if the given value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is not zero; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNonZero<T>(this T value) where T : INumber<T>
    {
        return value != T.Zero;
    }

    /// <summary>
    /// Validates whether the given value is not zero.
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
    public static ValidationResult ValidateIsNonZero<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsNonZero())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not zero, throwing an exception if validation fails.
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
    /// Thrown when the validation fails and the value is zero.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNonZero<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsNonZero(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NonZeroValidator : IValidator
{
    public static readonly NonZeroValidator Instance = new();
    public string Name => IsNonZero.ValidatorName;
    public string DefaultFailureMessage => IsNonZero.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            long v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            short v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            byte v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            uint v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            nint v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            double v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            float v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            Half v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsNonZero(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
