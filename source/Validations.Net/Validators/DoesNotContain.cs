using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a value does not contain a specified item.
/// </summary>
public static class DoesNotContain
{
    private const string ValidatorName = "DoesNotContain";

    #region Check Methods

    /// <summary>
    /// Checks if a string does not contain the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <returns>True if the string does not contain the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string? substring)
    {
        if (value == null || substring == null)
            return true;
        
        return !value.Contains(substring);
    }

    /// <summary>
    /// Checks if a string does not contain the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string does not contain the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string? substring, StringComparison comparisonType)
    {
        if (value == null || substring == null)
            return true;
        
        return !value.Contains(substring, comparisonType);
    }

    /// <summary>
    /// Checks if a collection does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the collection does not contain the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this IEnumerable<T>? value, T? item)
    {
        if (value == null)
            return true;
        
        foreach (var element in value)
        {
            if (EqualityComparer<T>.Default.Equals(element, item))
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if a non-generic collection does not contain the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the collection does not contain the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this IEnumerable? value, object? item)
    {
        if (value == null)
            return true;
        
        foreach (var element in value)
        {
            if (Equals(element, item))
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the span does not contain the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ReadOnlySpan<T> value, T? item) where T : IEquatable<T>
    {
        return !value.Contains(item);
    }

    /// <summary>
    /// Checks if a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the span does not contain the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this Span<T> value, T? item) where T : IEquatable<T>
    {
        return !((ReadOnlySpan<T>)value).Contains(item);
    }

    /// <summary>
    /// Checks if a memory does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the memory does not contain the item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this Memory<T> value, T? item) where T : IEquatable<T>
    {
        return !value.Span.Contains(item);
    }

    /// <summary>
    /// Checks if a StringBuilder does not contain the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <returns>True if the StringBuilder does not contain the substring; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this StringBuilder? value, string? substring)
    {
        if (value == null || substring == null)
            return true;
        
        return !value.ToString().Contains(substring);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string does not contain the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    public static string? EnsureDoesNotContain(this string? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContain(substring, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a string does not contain the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    public static string? EnsureDoesNotContain(this string? value, string? substring, StringComparison comparisonType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContain(substring, comparisonType, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a collection does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain the item.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the item.</exception>
    public static IEnumerable<T>? EnsureDoesNotContain<T>(this IEnumerable<T>? value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a non-generic collection does not contain the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain the item.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the item.</exception>
    public static IEnumerable? EnsureDoesNotContain(this IEnumerable? value, object? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain the item.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains the item.</exception>
    public static ReadOnlySpan<T> EnsureDoesNotContain<T>(this ReadOnlySpan<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain the item.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains the item.</exception>
    public static Span<T> EnsureDoesNotContain<T>(this Span<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a memory does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it does not contain the item.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains the item.</exception>
    public static Memory<T> EnsureDoesNotContain<T>(this Memory<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContain(item, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a StringBuilder does not contain the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original StringBuilder if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder contains the substring.</exception>
    public static StringBuilder? EnsureDoesNotContain(this StringBuilder? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContain(substring, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates that a string does not contain the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContain(substring))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "String contains the specified substring.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a string does not contain the specified substring using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, string? substring, StringComparison comparisonType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContain(substring, comparisonType))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring),
            ("comparisonType", comparisonType)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "String contains the specified substring.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a collection does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this IEnumerable<T>? value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection contains the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a non-generic collection does not contain the specified item.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain(this IEnumerable? value, object? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection contains the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this ReadOnlySpan<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesNotContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span contains the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a span does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this Span<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesNotContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span contains the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a memory does not contain the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this Memory<T> value, T? item, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesNotContain(item))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value.ToArray()),
            ("item", item)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Memory contains the specified item.", parameterName, blackboard, context);
    }

    /// <summary>
    /// Validates that a StringBuilder does not contain the specified substring.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContain(this StringBuilder? value, string? substring, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContain(substring))
            return ValidationResult.CreateFromValidationSuccess();

        var context = new List<(string key, object? value)>
        {
            ("value", value),
            ("substring", substring)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "StringBuilder contains the specified substring.", parameterName, blackboard, context);
    }

    #endregion
}
