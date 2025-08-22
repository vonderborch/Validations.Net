using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a value does not contain all of a specified list of items.
/// </summary>
public static class DoesNotContainAll
{
    private const string ValidatorName = "DoesNotContainAll";

    #region Check Methods

    /// <summary>
    ///     Checks if a string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, StringComparison comparisonType,
        params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring, comparisonType))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified substrings from a list using the specified string
    ///     comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, StringComparison comparisonType,
        IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring, comparisonType))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T>? value, params T?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null || !value.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a collection does not contain all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T>? value, IList<T?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null || !value.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a non-generic collection does not contain all of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this IEnumerable? value, params object?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            var found = false;
            foreach (var element in value)
            {
                if (Equals(element, item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a non-generic collection does not contain all of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this IEnumerable? value, IList<object?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            var found = false;
            foreach (var element in value)
            {
                if (Equals(element, item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ReadOnlySpan<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a span does not contain all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ReadOnlySpan<T> value, IList<T?> items) where T : IEquatable<T>
    {
        if (items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this Span<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (items == null || items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a span does not contain all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this Span<T> value, IList<T?> items) where T : IEquatable<T>
    {
        if (items == null || items.Count == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this Memory<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (items == null || items.Length == 0)
        {
            return true;
        }

        var span = value.Span;
        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a memory does not contain all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this Memory<T> value, IList<T?> items) where T : IEquatable<T>
    {
        if (items == null || items.Count == 0)
        {
            return true;
        }

        var span = value.Span;
        foreach (var item in items)
        {
            if (item == null)
            {
                return true;
            }

            var found = false;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a StringBuilder does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the StringBuilder does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this StringBuilder? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return true;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring == null || !str.Contains(substring))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a StringBuilder does not contain all of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the StringBuilder does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this StringBuilder? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return true;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring == null || !str.Contains(substring))
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainAll(this string? value, IBlackboard blackboard,
        string fieldName, params string?[] substrings)
    {
        if (CheckDoesNotContainAll(value, substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The field '{fieldName}' contains all of the specified substrings.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainAll<T>(this IEnumerable<T>? value, IBlackboard blackboard,
        string fieldName, params T?[] items)
    {
        if (CheckDoesNotContainAll(value, items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The field '{fieldName}' contains all of the specified items.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainAll<T>(this ReadOnlySpan<T> value, IBlackboard blackboard,
        string fieldName, params T?[] items) where T : IEquatable<T>
    {
        if (CheckDoesNotContainAll(value, items))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value.ToArray()),
            ("items", items)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The field '{fieldName}' contains all of the specified items.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it does not contain all substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all substrings.</exception>
    public static string EnsureDoesNotContainAll(this string value, params string?[] substrings)
    {
        if (!CheckDoesNotContainAll(value, substrings))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("substrings", substrings)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified substrings.", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it does not contain all items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all items.</exception>
    public static IEnumerable<T> EnsureDoesNotContainAll<T>(this IEnumerable<T> value, params T?[] items)
    {
        if (!CheckDoesNotContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified items.", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it does not contain all items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains all items.</exception>
    public static ReadOnlySpan<T> EnsureDoesNotContainAll<T>(this ReadOnlySpan<T> value, params T?[] items)
        where T : IEquatable<T>
    {
        if (!CheckDoesNotContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified items.", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it does not contain all items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains all items.</exception>
    public static Span<T> EnsureDoesNotContainAll<T>(this Span<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (!CheckDoesNotContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified items.", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original memory if it does not contain all items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains all items.</exception>
    public static Memory<T> EnsureDoesNotContainAll<T>(this Memory<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (!CheckDoesNotContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified items.", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a StringBuilder does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original StringBuilder if it does not contain all substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder contains all substrings.</exception>
    public static StringBuilder EnsureDoesNotContainAll(this StringBuilder value, params string?[] substrings)
    {
        if (!CheckDoesNotContainAll(value, substrings))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToString()),
                ("substrings", substrings)
            };
            throw ValidationException.Create(ValidatorName, "The value contains all of the specified substrings.", null,
                null, contextList);
        }

        return value;
    }

    #endregion
}
