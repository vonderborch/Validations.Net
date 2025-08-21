using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a value contains a specified item.
/// </summary>
public static class DoesContain
{
    private const string ValidatorName = "DoesContain";

    #region Check Methods

    /// <summary>
    /// Checks if a string contains the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <returns>True if the string contains the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string? substring)
    {
        if (value == null || substring == null)
            return false;
        
        return value.Contains(substring);
    }

    /// <summary>
    /// Checks if a string contains the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string contains the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string? substring, StringComparison comparisonType)
    {
        if (value == null || substring == null)
            return false;
        
        return value.Contains(substring, comparisonType);
    }

    /// <summary>
    /// Checks if a collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the collection contains the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this IEnumerable<T>? value, T? item)
    {
        if (value == null)
            return false;
        
        foreach (var element in value)
        {
            if (EqualityComparer<T>.Default.Equals(element, item))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a non-generic collection contains the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the collection contains the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this IEnumerable? value, object? item)
    {
        if (value == null)
            return false;
        
        foreach (var element in value)
        {
            if (Equals(element, item))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the span contains the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this ReadOnlySpan<T> value, T? item) where T : IEquatable<T>
    {
        return value.Contains(item);
    }

    /// <summary>
    /// Checks if a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the span contains the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this Span<T> value, T? item) where T : IEquatable<T>
    {
        return ((ReadOnlySpan<T>)value).Contains(item);
    }

    /// <summary>
    /// Checks if a memory contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the memory contains the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this Memory<T> value, T? item) where T : IEquatable<T>
    {
        return value.Span.Contains(item);
    }

    /// <summary>
    /// Checks if a StringBuilder contains the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <returns>True if the StringBuilder contains the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this StringBuilder? value, string? substring)
    {
        if (value == null || substring == null)
            return false;
        
        return value.ToString().Contains(substring);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string contains the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    public static string? EnsureDoesContain(this string? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContain(substring, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a string contains the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    public static string? EnsureDoesContain(this string? value, string? substring, StringComparison comparisonType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContain(substring, comparisonType, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains the item.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain the item.</exception>
    public static IEnumerable<T>? EnsureDoesContain<T>(this IEnumerable<T>? value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a non-generic collection contains the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains the item.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain the item.</exception>
    public static IEnumerable? EnsureDoesContain(this IEnumerable? value, object? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains the item.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain the item.</exception>
    public static ReadOnlySpan<T> EnsureDoesContain<T>(this ReadOnlySpan<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains the item.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain the item.</exception>
    public static Span<T> EnsureDoesContain<T>(this Span<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a memory contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it contains the item.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain the item.</exception>
    public static Memory<T> EnsureDoesContain<T>(this Memory<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a StringBuilder contains the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original StringBuilder if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder does not contain the substring.</exception>
    public static StringBuilder? EnsureDoesContain(this StringBuilder? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContain(substring, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates that a string contains the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContain(substring))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "String does not contain the specified substring.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a string contains the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, string? substring, StringComparison comparisonType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContain(substring, comparisonType))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring),
            ("comparisonType", comparisonType)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "String does not contain the specified substring.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this IEnumerable<T>? value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection does not contain the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a non-generic collection contains the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain(this IEnumerable? value, object? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection does not contain the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this ReadOnlySpan<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span does not contain the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a span contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this Span<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span does not contain the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a memory contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this Memory<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Memory does not contain the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a StringBuilder contains the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContain(this StringBuilder? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContain(substring))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "StringBuilder does not contain the specified substring.", parameterName, blackboard, context);
    }

    #endregion
}
