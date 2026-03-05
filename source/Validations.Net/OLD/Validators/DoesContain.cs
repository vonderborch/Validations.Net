using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The DoesContain class provides methods for validation to ensure that
/// a collection or string contains a specified item or substring. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesContain
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesContain";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain the specified item";

    /// <summary>
    /// Checks if the given collection contains the specified item.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this IEnumerable<T>? collection, T item)
    {
        if (collection is null)
            return false;

        var comparer = EqualityComparer<T>.Default;
        foreach (var element in collection)
        {
            if (comparer.Equals(element, item))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the given string contains the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substring);
        return value is not null && value.Contains(substring, comparison);
    }

    /// <summary>
    /// Validates whether the given collection contains the specified item.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContain<T>(this IEnumerable<T>? collection, T item, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesContain(item))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("item", item)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string contains the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesContain(substring, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substring", substring)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection contains the specified item, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesContain<T>(this IEnumerable<T>? collection, T item, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesContain(item, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string contains the specified substring, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesContain(substring, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public readonly record struct DoesContainParams(string Substring, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesContainValidator : IValidator
{
    public DoesContainParams Params { get; }

    public DoesContainValidator(string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        this.Params = new DoesContainParams(substring, comparison);
    }

    public string Name => DoesContain.ValidatorName;
    public string DefaultFailureMessage => DoesContain.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckDoesContain(this.Params.Substring, this.Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substring", this.Params.Substring)]);
    }
}

public sealed class ValidateDoesContainAttribute(string substring)
    : ValidatorAttribute(new DoesContainValidator(substring));
