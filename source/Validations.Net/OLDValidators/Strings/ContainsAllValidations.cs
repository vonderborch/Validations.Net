using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating if a string contains all specified elements (strings or characters).
/// </summary>
public static class ContainsAllValidations
{
    /// <summary>
    ///     Checks if the string contains all specified strings in the enumerable collection.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of strings to look for.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <returns>true if the string contains all specified items; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value, IEnumerable<string> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        return value.CheckContainsAll(items.ToList(), stringComparisonType);
    }

    /// <summary>
    ///     Checks if the string contains all specified string parameters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="items">The strings to look for.</param>
    /// <returns>true if the string contains all specified items; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value,
        StringComparison stringComparisonType = StringComparison.Ordinal, params string[] items)
    {
        return value.CheckContainsAll(items, stringComparisonType);
    }

    /// <summary>
    ///     Checks if the string contains all specified strings in the collection.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of strings to look for.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <returns>true if the string contains all specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the value or items are null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value, ICollection<string> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (!value.Contains(item, stringComparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if the string contains all specified characters in the enumerable collection.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of characters to look for.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <returns>true if the string contains all specified characters; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value, IEnumerable<char> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        return value.CheckContainsAll(items.ToList(), stringComparisonType);
    }

    /// <summary>
    ///     Checks if the string contains all specified character parameters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="items">The characters to look for.</param>
    /// <returns>true if the string contains all specified characters; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value,
        StringComparison stringComparisonType = StringComparison.Ordinal, params char[] items)
    {
        return value.CheckContainsAll(items, stringComparisonType);
    }

    /// <summary>
    ///     Checks if the string contains all specified characters in the collection.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of characters to look for.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <returns>true if the string contains all specified characters; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the value or items are null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll(this string? value, ICollection<char> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (!value.Contains(item, stringComparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Validates that the string contains all specified strings in the enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of strings to look for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, IEnumerable<string> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateContainsAll(items.ToList(), variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the string contains all specified string parameters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <param name="items">The strings to look for.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, StringComparison stringComparisonType,
        string variableName, object? blackboard = null, params string[] items)
    {
        return value.ValidateContainsAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the string contains all specified strings in the collection.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of strings to look for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, ICollection<string> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckContainsAll(items, stringComparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must contain all specified items.",
                "ContainsAll", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the string contains all specified characters in the enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of characters to look for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, IEnumerable<char> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateContainsAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the string contains all specified character parameters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <param name="items">The characters to look for.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, StringComparison stringComparisonType,
        string variableName, object? blackboard = null, params char[] items)
    {
        return value.ValidateContainsAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that the string contains all specified characters in the collection.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of characters to look for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use for matching.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string doesn't contain all specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateContainsAll(this string? value, ICollection<char> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckContainsAll(items, stringComparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must contain all specified characters.",
                "ContainsAll", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }
}
