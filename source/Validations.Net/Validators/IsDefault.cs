using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsDefault class provides methods for validation to ensure that
/// a value equals the default value for its type. Includes functionality to check, enforce,
/// and validate instances where default values are required.
/// </summary>
public static class IsDefault
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsDefault";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be the default value for its type";

    /// <summary>
    /// Checks if the given boxed value equals the default value for its runtime type.
    /// For reference types, checks for null. For boxed value types, compares against
    /// the default value of the underlying type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDefault(this object? value)
    {
        if (value is null) return true;
        var type = value.GetType();
        if (!type.IsValueType) return false;
        var defaultValue = Activator.CreateInstance(type);
        return Equals(value, defaultValue);
    }

    /// <summary>
    /// Checks if the given value equals the default value for its type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDefault<T>(this T? value)
    {
        return EqualityComparer<T>.Default.Equals(value, default);
    }

    /// <summary>
    /// Validates whether the given value equals the default value for its type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDefault<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsDefault())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value equals the default value for its type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsDefault<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsDefault(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class DefaultValidator : IValidator
{
    public static readonly DefaultValidator Instance = new();
    public string Name => IsDefault.ValidatorName;
    public string DefaultFailureMessage => IsDefault.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsDefault(blackboard, DefaultFailureMessage, memberName);
}

public sealed class ValidateIsDefaultAttribute() : ValidatorAttribute(DefaultValidator.Instance);
