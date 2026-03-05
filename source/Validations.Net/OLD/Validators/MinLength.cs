using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The MinLength class provides methods for validation to ensure that
/// a string has at least a specified minimum length. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class MinLength
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "MinLength";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter length must be at least the minimum";

    /// <summary>
    /// Checks if the given value has at least the specified minimum length.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minLength">The minimum length required.</param>
    /// <returns>
    /// True if the value length is at least minLength; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckMinLength(this string? value, int minLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minLength);
        return value is not null && value.Length >= minLength;
    }

    /// <summary>
    /// Ensures the given value has at least the specified minimum length, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureMinLength(this string? value, int minLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateMinLength(minLength, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value has at least the specified minimum length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateMinLength(this string? value, int minLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckMinLength(minLength))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("minLength", minLength), ("actualLength", value?.Length)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct MinLengthParams(int MinLength);

public sealed class MinLengthValidator : IValidator
{
    public MinLengthParams Params { get; }

    public MinLengthValidator(int minLength)
    {
        this.Params = new MinLengthParams(minLength);
    }

    public string Name => MinLength.ValidatorName;
    public string DefaultFailureMessage => MinLength.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckMinLength(this.Params.MinLength))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("minLength", this.Params.MinLength), ("actualLength", (value as string)?.Length)]);
    }
}

public sealed class ValidateMinLengthAttribute(int minLength)
    : ValidatorAttribute(new MinLengthValidator(minLength));
