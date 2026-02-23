using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsSameAs class provides methods for validation to ensure that
/// a value is the same object reference as another. Includes functionality to check, enforce,
/// and validate reference equality.
/// </summary>
public static class IsSameAs
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSameAs";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be the same object reference";

    /// <summary>
    /// Checks if the given value is the same object reference as the other.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsSameAs(this object? value, object? other)
    {
        return ReferenceEquals(value, other);
    }

    /// <summary>
    /// Validates whether the given value is the same object reference as the other.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsSameAs(this object? value, object? other, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsSameAs(other))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("other", other)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is the same object reference as the other, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsSameAs(this object? value, object? other, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsSameAs(other, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsSameAsValidator : IValidator
{
    public object? Other { get; }

    public IsSameAsValidator(object? other)
    {
        Other = other;
    }

    public string Name => IsSameAs.ValidatorName;
    public string DefaultFailureMessage => IsSameAs.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsSameAs(Other))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("other", Other)]);
    }
}

public sealed class ValidateIsSameAsAttribute(object? other)
    : ValidatorAttribute(new IsSameAsValidator(other));
