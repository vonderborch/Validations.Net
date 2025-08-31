using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a value contains all of a specified list of items.
/// </summary>
public static class DoesContainAll
{
    private const string ValidatorName = nameof(DoesContainAll);

    #region Check Methods

    /// <summary>
    ///     Checks if a string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return false;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string contains all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the string contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, StringComparison comparisonType,
        params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return false;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring, comparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string contains all of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return false;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string contains all of the specified substrings from a list using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, StringComparison comparisonType,
        IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return false;
        }

        foreach (var substring in substrings)
        {
            if (substring == null || !value.Contains(substring, comparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string contains all of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <returns>True if the string contains all of the characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, ICollection<char> characters)
    {
        if (value == null || characters == null || characters.Count == 0)
        {
            return false;
        }

        foreach (var character in characters)
        {
            if (!value.Contains(character))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string contains all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <returns>True if the string contains all of the characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters)
    {
        if (value == null || characters == null || characters.Count == 0)
        {
            return false;
        }

        foreach (var character in characters)
        {
            if (!value.Contains(character.ToString(), comparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this IEnumerable<T>? value, params T?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return false;
        }

        var collection = value.ToList();
        foreach (var item in items)
        {
            if (!collection.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a collection contains all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this IEnumerable<T>? value, IList<T?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return false;
        }

        var collection = value.ToList();
        foreach (var item in items)
        {
            if (!collection.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a non-generic collection contains all of the specified items.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the collection contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this IEnumerable? value, params object?[] items)
    {
        if (value == null || items == null || items.Length == 0)
        {
            return false;
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a non-generic collection contains all of the specified items from a list.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the collection contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this IEnumerable? value, IList<object?> items)
    {
        if (value == null || items == null || items.Count == 0)
        {
            return false;
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a collection contains all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <returns>True if the collection contains items matching all predicates; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this IEnumerable<T>? value, ICollection<Func<T, bool>> predicates)
    {
        if (value == null || predicates == null || predicates.Count == 0)
        {
            return false;
        }

        var collection = value.ToList();
        foreach (var predicate in predicates)
        {
            if (!collection.Any(predicate))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ReadOnlySpan<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (items == null || items.Length == 0)
        {
            return false;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return false;
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a span contains all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ReadOnlySpan<T> value, IList<T?> items) where T : IEquatable<T>
    {
        if (items == null || items.Count == 0)
        {
            return false;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                return false;
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the span contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this Span<T> value, params T?[] items) where T : IEquatable<T>
    {
        return CheckDoesContainAll((ReadOnlySpan<T>)value, items);
    }

    /// <summary>
    ///     Checks if a span contains all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the span contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this Span<T> value, IList<T?> items) where T : IEquatable<T>
    {
        return CheckDoesContainAll((ReadOnlySpan<T>)value, items);
    }

    /// <summary>
    ///     Checks if a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this Memory<T> value, params T?[] items) where T : IEquatable<T>
    {
        return CheckDoesContainAll(value.Span, items);
    }

    /// <summary>
    ///     Checks if a memory contains all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this Memory<T> value, IList<T?> items) where T : IEquatable<T>
    {
        return CheckDoesContainAll(value.Span, items);
    }

    /// <summary>
    ///     Checks if a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ReadOnlyMemory<T> value, params T?[] items) where T : IEquatable<T>
    {
        return CheckDoesContainAll(value.Span, items);
    }

    /// <summary>
    ///     Checks if a memory contains all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory contains all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ReadOnlyMemory<T> value, IList<T?> items) where T : IEquatable<T>
    {
        return CheckDoesContainAll(value.Span, items);
    }

    /// <summary>
    ///     Checks if a StringBuilder contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>True if the StringBuilder contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this StringBuilder? value, params string?[] substrings)
    {
        if (value == null || substrings == null || substrings.Length == 0)
        {
            return false;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring == null || !str.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a StringBuilder contains all of the specified substrings from a list.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the StringBuilder contains all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this StringBuilder? value, IList<string?> substrings)
    {
        if (value == null || substrings == null || substrings.Count == 0)
        {
            return false;
        }

        var str = value.ToString();
        foreach (var substring in substrings)
        {
            if (substring == null || !str.Contains(substring))
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Validate Methods (Old API Pattern)

    /// <summary>
    ///     Validates if a string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainAll(this string? value, IBlackboard blackboard, string fieldName,
        params string?[] substrings)
    {
        if (CheckDoesContainAll(value, substrings))
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
            $"The field '{fieldName}' does not contain all of the specified substrings.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainAll<T>(this IEnumerable<T>? value, IBlackboard blackboard,
        string fieldName, params T?[] items)
    {
        if (CheckDoesContainAll(value, items))
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
            $"The field '{fieldName}' does not contain all of the specified items.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">The blackboard for context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainAll<T>(this ReadOnlySpan<T> value, IBlackboard blackboard,
        string fieldName, params T?[] items) where T : IEquatable<T>
    {
        if (CheckDoesContainAll(value, items))
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
            $"The field '{fieldName}' does not contain all of the specified items.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll(this string? value, string[] substrings,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, substrings))
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
            "The value must contain all of the specified substrings.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string contains all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll(this string? value, StringComparison comparisonType,
        string[] substrings, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, comparisonType, substrings))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("comparisonType", comparisonType),
            ("substrings", substrings)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must contain all of the specified substrings using {comparisonType} comparison.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string contains all of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll(this string? value, ICollection<char> characters,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, characters))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("characters", characters)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must contain all of the specified characters.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string contains all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, comparisonType, characters))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("comparisonType", comparisonType),
            ("characters", characters)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must contain all of the specified characters using {comparisonType} comparison.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this IEnumerable<T>? value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, items))
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
            "The value must contain all of the specified items.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection contains all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this IEnumerable<T>? value,
        ICollection<Func<T, bool>> predicates, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesContainAll(value, predicates))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("predicates", predicates)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must contain items matching all of the specified predicates.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this ReadOnlySpan<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        if (CheckDoesContainAll(value, items))
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
            "The value must contain all of the specified items.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this Span<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesContainAll((ReadOnlySpan<T>)value, items, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this Memory<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesContainAll(value.Span, items, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this ReadOnlyMemory<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesContainAll(value.Span, items, blackboard, parameterName);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains all of the specified substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the specified substrings.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAll(this string? value, string[] substrings,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, substrings, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string contains all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains all of the specified substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the specified substrings.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAll(this string? value, StringComparison comparisonType,
        string[] substrings, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, comparisonType, substrings, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string contains all of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains all of the specified characters.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the specified characters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAll(this string? value, ICollection<char> characters,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, characters, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string contains all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it contains all of the specified characters.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the specified characters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, comparisonType, characters, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesContainAll<T>(this IEnumerable<T>? value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, items, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection contains all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it contains items matching all of the specified predicates.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain items matching all of the specified predicates.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesContainAll<T>(this IEnumerable<T>? value,
        ICollection<Func<T, bool>> predicates, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesContainAll(value, predicates, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> EnsureDoesContainAll<T>(this ReadOnlySpan<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesContainAll(value, items, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it contains all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> EnsureDoesContainAll<T>(this Span<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesContainAll(value, items, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it contains all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> EnsureDoesContainAll<T>(this Memory<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesContainAll(value, items, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it contains all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<T> EnsureDoesContainAll<T>(this ReadOnlyMemory<T> value, T[] items,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesContainAll(value, items, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion

    #region Ensure Methods (Old API Pattern)

    /// <summary>
    ///     Ensures that a string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original string if it contains all substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all substrings.</exception>
    public static string EnsureDoesContainAll(this string value, params string?[] substrings)
    {
        if (!CheckDoesContainAll(value, substrings))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("substrings", substrings)
            };
            throw ValidationException.Create(ValidatorName,
                "The value does not contain all of the specified substrings.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original collection if it contains all items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all items.</exception>
    public static IEnumerable<T> EnsureDoesContainAll<T>(this IEnumerable<T> value, params T?[] items)
    {
        if (!CheckDoesContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value does not contain all of the specified items.",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it contains all items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain all items.</exception>
    public static ReadOnlySpan<T> EnsureDoesContainAll<T>(this ReadOnlySpan<T> value, params T?[] items)
        where T : IEquatable<T>
    {
        if (!CheckDoesContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value does not contain all of the specified items.",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original span if it contains all items.</returns>
    /// <exception cref="ValidationException">Thrown when the span does not contain all items.</exception>
    public static Span<T> EnsureDoesContainAll<T>(this Span<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (!CheckDoesContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value does not contain all of the specified items.",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>The original memory if it contains all items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory does not contain all items.</exception>
    public static Memory<T> EnsureDoesContainAll<T>(this Memory<T> value, params T?[] items) where T : IEquatable<T>
    {
        if (!CheckDoesContainAll(value, items))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToArray()),
                ("items", items)
            };
            throw ValidationException.Create(ValidatorName, "The value does not contain all of the specified items.",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a StringBuilder contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <returns>The original StringBuilder if it contains all substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the StringBuilder does not contain all substrings.</exception>
    public static StringBuilder EnsureDoesContainAll(this StringBuilder value, params string?[] substrings)
    {
        if (!CheckDoesContainAll(value, substrings))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value.ToString()),
                ("substrings", substrings)
            };
            throw ValidationException.Create(ValidatorName,
                "The value does not contain all of the specified substrings.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
