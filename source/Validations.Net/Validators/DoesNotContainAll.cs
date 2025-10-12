using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a value does not contain all of a specified list of items.
/// </summary>
public static class DoesNotContainAll
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotContainAll";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain all specified items";

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
        return !DoesContainAll.CheckDoesContainAll(value, substrings);
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
        return !DoesContainAll.CheckDoesContainAll(value, comparisonType, substrings);
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
        return !DoesContainAll.CheckDoesContainAll(value, substrings);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified substrings from a list using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The list of substrings to search for.</param>
    /// <returns>True if the string does not contain all of the substrings; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, StringComparison comparisonType,
        IList<string?> substrings)
    {
        return !DoesContainAll.CheckDoesContainAll(value, comparisonType, substrings);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <returns>True if the string does not contain all of the characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<char> characters)
    {
        return !DoesContainAll.CheckDoesContainAll(value, characters);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <returns>True if the string does not contain all of the characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters)
    {
        return !DoesContainAll.CheckDoesContainAll(value, comparisonType, characters);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <returns>True if the collection does not contain items matching all predicates; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T>? value, ICollection<Func<T, bool>> predicates)
    {
        return !DoesContainAll.CheckDoesContainAll(value, predicates);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, items);
    }

    /// <summary>
    ///     Checks if a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The items to search for.</param>
    /// <returns>True if the memory does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ReadOnlyMemory<T> value, params T?[] items) where T : IEquatable<T>
    {
        return !DoesContainAll.CheckDoesContainAll(value, items);
    }

    /// <summary>
    ///     Checks if a memory does not contain all of the specified items from a list.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="items">The list of items to search for.</param>
    /// <returns>True if the memory does not contain all of the items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ReadOnlyMemory<T> value, IList<T?> items) where T : IEquatable<T>
    {
        return !DoesContainAll.CheckDoesContainAll(value, items);
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
        return !DoesContainAll.CheckDoesContainAll(value, substrings);
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
        return !DoesContainAll.CheckDoesContainAll(value, substrings);
    }

    #endregion

    #region Validate Methods (Old API Pattern)

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

    #region Validate Methods

    /// <summary>
    ///     Validates if a string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll(this string? value, string[] substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string does not contain all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll(this string? value, StringComparison comparisonType,
        string[] substrings, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesNotContainAll(value, comparisonType, substrings))
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll(this string? value, ICollection<char> characters,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesNotContainAll(value, characters))
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string does not contain all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesNotContainAll(value, comparisonType, characters))
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this IEnumerable<T>? value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a collection does not contain all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this IEnumerable<T>? value,
        ICollection<Func<T, bool>> predicates, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckDoesNotContainAll(value, predicates))
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this ReadOnlySpan<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this Span<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesNotContainAll((ReadOnlySpan<T>)value, items, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this Memory<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesNotContainAll(value.Span, items, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this ReadOnlyMemory<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        return ValidateDoesNotContainAll(value.Span, items, blackboard, parameterName);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain all of the specified substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified substrings.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAll(this string? value, string[] substrings,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, substrings, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain all of the specified substrings using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="substrings">The substrings to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain all of the specified substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified substrings.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAll(this string? value, StringComparison comparisonType,
        string[] substrings, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, comparisonType, substrings, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain all of the specified characters.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified characters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAll(this string? value, ICollection<char> characters,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, characters, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string does not contain all of the specified characters using the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it does not contain all of the specified characters.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified characters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAll(this string? value, StringComparison comparisonType,
        ICollection<char> characters, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, comparisonType, characters, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesNotContainAll<T>(this IEnumerable<T>? value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a collection does not contain all items that match the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="predicates">The predicates to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original collection if it does not contain items matching all of the specified predicates.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains items matching all of the specified predicates.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesNotContainAll<T>(this IEnumerable<T>? value,
        ICollection<Func<T, bool>> predicates, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateDoesNotContainAll(value, predicates, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> EnsureDoesNotContainAll<T>(this ReadOnlySpan<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesNotContainAll(value, items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a span does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original span if it does not contain all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the span contains all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> EnsureDoesNotContainAll<T>(this Span<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesNotContainAll(value, items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it does not contain all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> EnsureDoesNotContainAll<T>(this Memory<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesNotContainAll(value, items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a memory does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="items">The items to search for.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original memory if it does not contain all of the specified items.</returns>
    /// <exception cref="ValidationException">Thrown when the memory contains all of the specified items.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<T> EnsureDoesNotContainAll<T>(this ReadOnlyMemory<T> value, T[] items,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IEquatable<T>
    {
        var result = ValidateDoesNotContainAll(value, items, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion

    #region Ensure Methods (Old API Pattern)

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
    /// <param name="value">The StringBuilder to check.</param>
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
