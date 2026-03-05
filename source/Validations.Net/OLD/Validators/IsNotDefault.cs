using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotDefault class provides methods for validation to ensure that
/// a value does not equal the default value for its type. Includes functionality to check, enforce,
/// and validate instances where non-default values are required.
/// </summary>
public static class IsNotDefault
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotDefault";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be the default value for its type";

    /// <summary>
    /// Checks if the given boxed value does not equal the default value for its runtime type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotDefault(this object? value)
    {
        return !value.CheckIsDefault();
    }

    /// <summary>
    /// Checks if the given value does not equal the default value for its type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotDefault<T>(this T? value)
    {
        return !EqualityComparer<T>.Default.Equals(value, default);
    }

    /// <summary>
    /// Validates whether the given value does not equal the default value for its type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotDefault<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotDefault())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value does not equal the default value for its type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotDefault<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotDefault(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NotDefaultValidator : IValidator
{
    public static readonly NotDefaultValidator Instance = new();
    public string Name => IsNotDefault.ValidatorName;
    public string DefaultFailureMessage => IsNotDefault.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotDefault())
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}

public sealed class ValidateIsNotDefaultAttribute() : ValidatorAttribute(NotDefaultValidator.Instance);
