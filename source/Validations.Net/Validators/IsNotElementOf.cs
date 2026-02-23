using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotElementOf class provides methods for validation to ensure that
/// a value is not an element of a specified collection. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class IsNotElementOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotElementOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be an element of the specified collection";

    /// <summary>
    /// Checks if the given value is not an element of the specified collection.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotElementOf<T>(this T? value, IEnumerable<T>? collection)
    {
        return !value.CheckIsElementOf(collection);
    }

    /// <summary>
    /// Checks if the given string does not contain the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substring);
        return !value.CheckIsElementOf(substring, comparison);
    }

    /// <summary>
    /// Validates whether the given value is not an element of the specified collection.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotElementOf<T>(this T? value, IEnumerable<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotElementOf(collection))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("collection", collection)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string does not contain the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotElementOf(substring, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substring", substring)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not an element of the specified collection, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotElementOf<T>(this T? value, IEnumerable<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotElementOf(collection, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given string does not contain the specified substring, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotElementOf(substring, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsNotElementOfValidator : IValidator
{
    public object?[] DisallowedValues { get; }

    public IsNotElementOfValidator(params object?[] disallowedValues)
    {
        DisallowedValues = disallowedValues;
    }

    public string Name => IsNotElementOf.ValidatorName;
    public string DefaultFailureMessage => IsNotElementOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (DisallowedValues.Length == 0)
            return ValidationResult.CreateFromValidationSuccess();

        if (value is string str && DisallowedValues.All(v => v is string))
        {
            var stringValues = DisallowedValues.Cast<string>().ToArray();
            if (str.CheckIsNotElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsNotElementOf(DisallowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("disallowedValues", DisallowedValues)]);
    }
}

public sealed class ValidateIsNotElementOfAttribute(params object?[] disallowedValues)
    : ValidatorAttribute(new IsNotElementOfValidator(disallowedValues));
