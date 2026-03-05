using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsCreditCard class provides methods for validation to ensure that
/// a string represents a valid credit card number using the Luhn algorithm.
/// Strips non-digits, validates length 13-19, and runs Luhn checksum.
/// </summary>
public static class IsCreditCard
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsCreditCard";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be a valid credit card number";

    /// <summary>
    /// Checks if the given string is a valid credit card number using the Luhn algorithm.
    /// Strips non-digits, validates length 13-19, and runs Luhn checksum.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is a valid credit card number; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsCreditCard(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var digits = new char[value.Length];
        var digitCount = 0;
        foreach (var c in value)
        {
            if (char.IsDigit(c))
            {
                digits[digitCount++] = c;
            }
        }

        if (digitCount < 13 || digitCount > 19)
            return false;

        var sum = 0;
        var alternate = false;
        for (var i = digitCount - 1; i >= 0; i--)
        {
            var n = digits[i] - '0';
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                    n -= 9;
            }

            sum += n;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }

    /// <summary>
    /// Validates whether the given string is a valid credit card number.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails. Defaults to the default failure message.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsCreditCard(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsCreditCard())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid credit card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails and the value is not a valid credit card number.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsCreditCard(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsCreditCard(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class CreditCardValidator : IValidator
{
    public static readonly CreditCardValidator Instance = new();
    public string Name => IsCreditCard.ValidatorName;
    public string DefaultFailureMessage => IsCreditCard.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value is string s ? s.ValidateIsCreditCard(blackboard, this.DefaultFailureMessage, memberName)
            : ValidationResult.CreateFromValidationSuccess();
}

public sealed class ValidateIsCreditCardAttribute() : ValidatorAttribute(CreditCardValidator.Instance);
