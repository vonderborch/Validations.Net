using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotSameAs class provides methods for validation to ensure that
/// a value is not the same object reference as another. Includes functionality to check, enforce,
/// and validate that references differ.
/// </summary>
public static class IsNotSameAs
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSameAs";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be the same object reference";

    /// <summary>
    /// Checks if the given value is not the same object reference as the other.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotSameAs(this object? value, object? other)
    {
        return !ReferenceEquals(value, other);
    }

    /// <summary>
    /// Validates whether the given value is not the same object reference as the other.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotSameAs(this object? value, object? other, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotSameAs(other))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("other", other)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not the same object reference as the other, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotSameAs(this object? value, object? other, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotSameAs(other, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NotSameAsValidator : IValidator
{
    public object? Other { get; }

    public NotSameAsValidator(object? other)
    {
        this.Other = other;
    }

    public string Name => IsNotSameAs.ValidatorName;
    public string DefaultFailureMessage => IsNotSameAs.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotSameAs(this.Other))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("other", this.Other)]);
    }
}

public sealed class ValidateIsNotSameAsAttribute(object? other)
    : ValidatorAttribute(new NotSameAsValidator(other));
