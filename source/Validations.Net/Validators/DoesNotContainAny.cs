using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a value does not contain any of a specified set of items.
/// </summary>
public static class DoesNotContainAny
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotContainAny";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain any of the specified items";

    /// <summary>
    ///     Checks if a string does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string does not contain any of the specified substrings with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, StringComparison comparison,
        params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring, comparison))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string does not contain any of the specified substrings from a list with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>True if the string does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, IList<string?> substrings,
        StringComparison comparison)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring != null && value.Contains(substring, comparison))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T>? value, params T?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null && value.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a collection does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T>? value, IList<T?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null && value.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a non-generic collection does not contain any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this IEnumerable? value, params object?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                foreach (var collectionItem in value)
                {
                    if (Equals(collectionItem, item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a non-generic collection does not contain any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this IEnumerable? value, IList<object?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                foreach (var collectionItem in value)
                {
                    if (Equals(collectionItem, item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a ReadOnlySpan does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ReadOnlySpan<T> value, params T[]? items)
    {
        if (items is null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            for (var i = 0; i < value.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(value[i], item))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a ReadOnlySpan does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items)
    {
        if (items is null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a Span does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this Span<T> value, params T?[] items)
    {
        if (items is null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a Span does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this Span<T> value, IList<T?> items)
    {
        if (items is null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value[i], item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a Memory does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this Memory<T> value, params T?[] items)
    {
        if (items is null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value.Span[i], item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a Memory does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory does not contain any of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this Memory<T> value, IList<T?> items)
    {
        if (items is null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(value.Span[i], item))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a StringBuilder does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the StringBuilder does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this StringBuilder? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring != null && str.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a StringBuilder does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the StringBuilder does not contain any of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this StringBuilder? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring != null && str.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Validates that a string does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        if (value.CheckDoesNotContainAny(substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that a string does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, IList<string?> substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContainAny(substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that a string does not contain any of the specified substrings with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, StringComparison comparison,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        if (value.CheckDoesNotContainAny(comparison, substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("comparison", comparison),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a string does not contain any of the specified substrings from a list with a specific string
    ///     comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, IList<string?> substrings,
        StringComparison comparison, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContainAny(substrings, comparison))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings),
            ("comparison", comparison)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this IEnumerable<T>? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a collection does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this IEnumerable<T>? value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a non-generic collection does not contain any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this IEnumerable? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params object?[] items)
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a non-generic collection does not contain any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this IEnumerable? value, IList<object?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a ReadOnlySpan does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this ReadOnlySpan<T> value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items) where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a ReadOnlySpan does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a Span does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this Span<T> value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
        where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a Span does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this Span<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a Memory does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this Memory<T> value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
        where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a Memory does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this Memory<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        if (value.CheckDoesNotContainAny(items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a StringBuilder does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this StringBuilder? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        if (value.CheckDoesNotContainAny(substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that a StringBuilder does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the validation passed.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this StringBuilder? value, IList<string?> substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckDoesNotContainAny(substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            validationFailureMessage, parameterName, blackboard, contextList);
    }

    /// <summary>
    ///     Ensures that a string does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    public static string? EnsureDoesNotContainAny(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, substrings);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    public static string? EnsureDoesNotContainAny(this string? value, IList<string?> substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContainAny(substrings, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain any of the specified substrings with a specific string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    public static string? EnsureDoesNotContainAny(this string? value, StringComparison comparison,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        var result = value.ValidateDoesNotContainAny(comparison, blackboard, validationFailureMessage, parameterName, substrings);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain any of the specified substrings from a list with a specific string
    ///     comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    public static string? EnsureDoesNotContainAny(this string? value, IList<string?> substrings,
        StringComparison comparison, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContainAny(substrings, comparison, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the items.</exception>
    public static IEnumerable<T>? EnsureDoesNotContainAny<T>(this IEnumerable<T>? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, items);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the items.</exception>
    public static IEnumerable<T>? EnsureDoesNotContainAny<T>(this IEnumerable<T>? value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContainAny(items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a non-generic collection does not contain any of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the items.</exception>
    public static IEnumerable? EnsureDoesNotContainAny(this IEnumerable? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params object?[] items)
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, items);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a non-generic collection does not contain any of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the items.</exception>
    public static IEnumerable? EnsureDoesNotContainAny(this IEnumerable? value, IList<object?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContainAny(items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a ReadOnlySpan does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains any of the items.</exception>
    public static ReadOnlySpan<T> EnsureDoesNotContainAny<T>(this ReadOnlySpan<T> value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, items);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a ReadOnlySpan does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains any of the items.</exception>
    public static ReadOnlySpan<T> EnsureDoesNotContainAny<T>(this ReadOnlySpan<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a Span does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains any of the items.</exception>
    public static Span<T> EnsureDoesNotContainAny<T>(this Span<T> value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, items);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a Span does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains any of the items.</exception>
    public static Span<T> EnsureDoesNotContainAny<T>(this Span<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a Memory does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original memory if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains any of the items.</exception>
    public static Memory<T> EnsureDoesNotContainAny<T>(this Memory<T> value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T?[] items)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, items);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a Memory does not contain any of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it does not contain any of the items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains any of the items.</exception>
    public static Memory<T> EnsureDoesNotContainAny<T>(this Memory<T> value, IList<T?> items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = value.ValidateDoesNotContainAny(items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a StringBuilder does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original StringBuilder if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder contains any of the substrings.</exception>
    public static StringBuilder? EnsureDoesNotContainAny(this StringBuilder? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params string?[] substrings)
    {
        var result = value.ValidateDoesNotContainAny(blackboard, validationFailureMessage, parameterName, substrings);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a StringBuilder does not contain any of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original StringBuilder if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder contains any of the substrings.</exception>
    public static StringBuilder? EnsureDoesNotContainAny(this StringBuilder? value, IList<string?> substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateDoesNotContainAny(substrings, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
