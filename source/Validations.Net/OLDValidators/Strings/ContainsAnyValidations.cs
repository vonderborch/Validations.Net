using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating that strings contain at least one value from a collection of values.
/// </summary>
public static class ContainsAnyValidations
{
    /// <summary>
    ///     Checks if the specified string contains at least one of the items from the collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The collection of strings to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified items; otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when <paramref name="value" /> or <paramref name="items" /> is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, ICollection<string> items,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var val in items)
        {
            if (value.Contains(val, comparisonType))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if the specified string contains at least one of the items from the params array.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="items">The array of strings to check for. Cannot be null.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified items; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, StringComparison comparisonType = StringComparison.Ordinal,
        params string[] items)
    {
        return value.CheckContainsAny(items, comparisonType);
    }

    /// <summary>
    ///     Checks if the specified string contains at least one of the items from the enumerable collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The enumerable collection of strings to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified items; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, IEnumerable<string> items,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        return value.CheckContainsAny(items.ToList(), comparisonType);
    }

    /// <summary>
    ///     Checks if the specified string contains at least one of the characters from the collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The collection of characters to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified characters; otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when <paramref name="value" /> or <paramref name="items" /> is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, ICollection<char> items,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (value.Contains(item, comparisonType))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if the specified string contains at least one of the characters from the params array.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="items">The array of characters to check for. Cannot be null.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified characters; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, StringComparison comparisonType = StringComparison.Ordinal,
        params char[] items)
    {
        return value.CheckContainsAny(items, comparisonType);
    }

    /// <summary>
    ///     Checks if the specified string contains at least one of the characters from the enumerable collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The enumerable collection of characters to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <returns><c>true</c> if the string contains at least one of the specified characters; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny(this string? value, IEnumerable<char> items,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        return value.CheckContainsAny(items.ToList(), comparisonType);
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the items from the collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The collection of strings to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, ICollection<string> items, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckContainsAny(items, comparisonType))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard,
                new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the items from the params array.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <param name="items">The array of strings to check for. Cannot be null.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null, params string[] items)
    {
        return value.ValidateContainsAny(items, variableName, comparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the items from the enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The enumerable collection of strings to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, IEnumerable<string> items, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateContainsAny(items.ToList(), variableName, comparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the characters from the collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The collection of characters to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, ICollection<char> items, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckContainsAny(items, comparisonType))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain at least one of the specified characters.", "ContainsAny", blackboard,
                new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the characters from the params array.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <param name="items">The array of characters to check for. Cannot be null.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null, params char[] items)
    {
        return value.ValidateContainsAny(items, variableName, comparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the specified string contains at least one of the characters from the enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The enumerable collection of characters to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal" />.</param>
    /// <param name="blackboard">Additional context data for the validation.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAny(this string? value, IEnumerable<char> items, string variableName,
        StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateContainsAny(items.ToList(), variableName, comparisonType, blackboard);
    }
}
