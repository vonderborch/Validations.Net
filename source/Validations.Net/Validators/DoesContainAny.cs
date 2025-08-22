using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a value contains any of a specified list of items.
/// </summary>
public static class DoesContainAny
{
    private const string ValidatorName = "DoesContainAny";

    #region Check Methods

    /// <summary>
    /// Checks if a string contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
            return false;
        
        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a string contains any of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, StringComparison comparisonType, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
            return false;
        
        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring, comparisonType))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a string contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
            return false;
        
        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a string contains any of the specified substrings from a list using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, StringComparison comparisonType, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
            return false;
        
        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring, comparisonType))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a collection contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this IEnumerable<T>? value, params T?[] items)
    {
        if (value == null || items == null || items.Length == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null && value.Contains(item))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a collection contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this IEnumerable<T>? value, IList<T?> items)
    {
        if (value == null || items == null || items.Count == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null && value.Contains(item))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a non-generic collection contains any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this IEnumerable? value, params object?[] items)
    {
        if (value == null || items == null || items.Length == 0)
            return false;
        
        foreach (var item in items)
        {
            foreach (var element in value)
            {
                if (Equals(element, item))
                    return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a non-generic collection contains any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this IEnumerable? value, IList<object?> items)
    {
        if (value == null || items == null || items.Count == 0)
            return false;
        
        foreach (var item in items)
        {
            foreach (var element in value)
            {
                if (Equals(element, item))
                    return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a span contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ReadOnlySpan<T> value, params T[]? items)
    {
        if (items is null || items.Length == 0)
            return false;
        
        foreach (var item in items)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(value[i], item))
                    return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a span contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items)
    {
        if (items == null || items.Count == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                        return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a span contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this Span<T> value, params T?[] items)
    {
        if (items is null || items.Length == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                        return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a span contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this Span<T> value, IList<T?> items)
    {
        if (items == null || items.Count == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                        return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a memory contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this Memory<T> value, params T?[] items)
    {
        if (items is null || items.Length == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value.Span[i], item))
                        return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a memory contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory contains any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this Memory<T> value, IList<T?> items)
    {
        if (items == null || items.Count == 0)
            return false;
        
        foreach (var item in items)
        {
            if (item != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value.Span[i], item))
                        return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a StringBuilder contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the StringBuilder contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this StringBuilder? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
            return false;
        
        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring != null && str.Contains(substring))
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks if a StringBuilder contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the StringBuilder contains any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this StringBuilder? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
            return false;
        
        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring != null && str.Contains(substring))
                return true;
        }
        
        return false;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates that a string contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        if (value.CheckDoesContainAny(substrings))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"String '{value}' does not contain any of the specified substrings.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a string contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, IList<string?> substrings, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContainAny(substrings))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"String '{value}' does not contain any of the specified substrings.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a string contains any of the specified substrings with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, StringComparison comparison, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        if (value.CheckDoesContainAny(comparison, substrings))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("comparison", comparison),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"String '{value}' does not contain any of the specified substrings using comparison '{comparison}'.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a string contains any of the specified substrings from a list with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, IList<string?> substrings, StringComparison comparison, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContainAny(substrings, comparison))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings),
            ("comparison", comparison)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"String '{value}' does not contain any of the specified substrings using comparison '{comparison}'.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a collection contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this IEnumerable<T>? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items)
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a collection contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this IEnumerable<T>? value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a non-generic collection contains any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this IEnumerable? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params object?[] items)
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a non-generic collection contains any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this IEnumerable? value, IList<object?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a ReadOnlySpan contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this ReadOnlySpan<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Span does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a ReadOnlySpan contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Span does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a Span contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this Span<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Span does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a Span contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this Span<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Span does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a Memory contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this Memory<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Memory does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a Memory contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this Memory<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (value.CheckDoesContainAny(items))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Memory does not contain any of the specified items.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a StringBuilder contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this StringBuilder? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        if (value.CheckDoesContainAny(substrings))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"StringBuilder does not contain any of the specified substrings.", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that a StringBuilder contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesContainAny(this StringBuilder? value, IList<string?> substrings, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesContainAny(substrings))
            return ValidationResult.CreateFromValidationSuccess();

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"StringBuilder does not contain any of the specified substrings.", parameterName, blackboard, contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the substrings.</exception>
    public static string? EnsureDoesContainAny(this string? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, substrings);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a string contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the substrings.</exception>
    public static string? EnsureDoesContainAny(this string? value, IList<string?> substrings, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContainAny(substrings, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a string contains any of the specified substrings with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the substrings.</exception>
    public static string? EnsureDoesContainAny(this string? value, StringComparison comparison, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        var result = value.ValidateDoesContainAny(comparison, blackboard, parameterName, substrings);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a string contains any of the specified substrings from a list with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the substrings.</exception>
    public static string? EnsureDoesContainAny(this string? value, IList<string?> substrings, StringComparison comparison, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContainAny(substrings, comparison, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a collection contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the items.</exception>
    public static IEnumerable<T>? EnsureDoesContainAny<T>(this IEnumerable<T>? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items)
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, items);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a collection contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the items.</exception>
    public static IEnumerable<T>? EnsureDoesContainAny<T>(this IEnumerable<T>? value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContainAny(items, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a non-generic collection contains any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the items.</exception>
    public static IEnumerable? EnsureDoesContainAny(this IEnumerable? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params object?[] items)
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, items);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a non-generic collection contains any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the items.</exception>
    public static IEnumerable? EnsureDoesContainAny(this IEnumerable? value, IList<object?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContainAny(items, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a ReadOnlySpan contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain any of the items.</exception>
    public static ReadOnlySpan<T> EnsureDoesContainAny<T>(this ReadOnlySpan<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, items);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a ReadOnlySpan contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain any of the items.</exception>
    public static ReadOnlySpan<T> EnsureDoesContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(items, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a Span contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain any of the items.</exception>
    public static Span<T> EnsureDoesContainAny<T>(this Span<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, items);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a Span contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain any of the items.</exception>
    public static Span<T> EnsureDoesContainAny<T>(this Span<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(items, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a Memory contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original memory if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain any of the items.</exception>
    public static Memory<T> EnsureDoesContainAny<T>(this Memory<T> value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params T?[] items) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, items);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a Memory contains any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it contains any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain any of the items.</exception>
    public static Memory<T> EnsureDoesContainAny<T>(this Memory<T> value, IList<T?> items, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        var result = value.ValidateDoesContainAny(items, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a StringBuilder contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original StringBuilder if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder does not contain any of the substrings.</exception>
    public static StringBuilder? EnsureDoesContainAny(this StringBuilder? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null, params string?[] substrings)
    {
        var result = value.ValidateDoesContainAny(blackboard, parameterName, substrings);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    /// <summary>
    /// Ensures that a StringBuilder contains any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original StringBuilder if it contains any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder does not contain any of the substrings.</exception>
    public static StringBuilder? EnsureDoesContainAny(this StringBuilder? value, IList<string?> substrings, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesContainAny(substrings, blackboard, parameterName);
        if (!result.IsValid)
            throw result.ValidationException!;
        return value;
    }

    #endregion
}
