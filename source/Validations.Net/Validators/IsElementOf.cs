using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsElementOf class provides methods for validation to ensure that
/// a value is an element of a specified collection. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class IsElementOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsElementOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be an element of the specified collection";

    /// <summary>
    /// Checks if the given value is an element of the specified collection.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsElementOf<T>(this T? value, IEnumerable<T>? collection)
    {
        if (collection is null)
            return false;

        var comparer = EqualityComparer<T>.Default;
        foreach (var element in collection)
        {
            if (comparer.Equals(element, value))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the given string contains the specified substring (value is element of the set of strings containing substring).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substring);
        return value is not null && value.Contains(substring, comparison);
    }

    /// <summary>
    /// Validates whether the given value is an element of the specified collection.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsElementOf<T>(this T? value, IEnumerable<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsElementOf(collection))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("collection", collection)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string contains the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsElementOf(substring, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substring", substring)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is an element of the specified collection, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsElementOf<T>(this T? value, IEnumerable<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsElementOf(collection, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given string contains the specified substring, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsElementOf(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsElementOf(substring, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsElementOfValidator : IValidator
{
    public object?[] AllowedValues { get; }

    public IsElementOfValidator(params object?[] allowedValues)
    {
        AllowedValues = allowedValues;
    }

    public string Name => IsElementOf.ValidatorName;
    public string DefaultFailureMessage => IsElementOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (AllowedValues.Length == 0)
            return Fail(value, memberName, blackboard);

        if (value is string str && AllowedValues.All(v => v is string))
        {
            var stringValues = AllowedValues.Cast<string>().ToArray();
            if (str.CheckIsElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsElementOf(AllowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
        => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("allowedValues", AllowedValues)]);
}

public sealed class ValidateIsElementOfAttribute(params object?[] allowedValues)
    : ValidatorAttribute(new IsElementOfValidator(allowedValues));
