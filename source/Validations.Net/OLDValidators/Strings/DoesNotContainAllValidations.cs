using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating that strings do not contain all specified items or characters.
/// </summary>
public static class DoesNotContainAllValidations
{
    /// <summary>
    ///     Checks if a string does not contain all of the specified items.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of items to check for.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified items; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, IEnumerable<string> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        return value.CheckDoesNotContainAll(items.ToList(), stringComparisonType);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified string items using a params array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <param name="items">The array of string items to check for.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified items; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value,
        StringComparison stringComparisonType = StringComparison.Ordinal, params string[] items)
    {
        return value.CheckDoesNotContainAll(items, stringComparisonType);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified items in a collection.
    ///     This is the main implementation that other overloads delegate to.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of items to check for.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified items; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<string> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (value.Contains(item, stringComparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of characters to check for.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified characters; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, IEnumerable<char> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        return value.CheckDoesNotContainAll(items.ToList(), stringComparisonType);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified characters using a params array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <param name="items">The array of characters to check for.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified characters; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value,
        StringComparison stringComparisonType = StringComparison.Ordinal, params char[] items)
    {
        return value.CheckDoesNotContainAll(items, stringComparisonType);
    }

    /// <summary>
    ///     Checks if a string does not contain all of the specified characters in a collection.
    ///     This is the main implementation that other character-based overloads delegate to.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="items">The collection of characters to check for.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <returns><c>true</c> if the string does not contain all of the specified characters; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<char> items,
        StringComparison stringComparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (value.Contains(item, stringComparisonType))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified items.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, IEnumerable<string> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateDoesNotContainAll(items.ToList(), variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified string items using a params array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <param name="items">The array of string items to check for.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, StringComparison stringComparisonType,
        string variableName, object? blackboard = null, params string[] items)
    {
        return value.ValidateDoesNotContainAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified items in a collection.
    ///     This is the main implementation that other overloads delegate to.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for items.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, ICollection<string> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(items, stringComparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.",
                "DoesNotContainAll", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of characters to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, IEnumerable<char> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateDoesNotContainAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified characters using a params array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <param name="items">The array of characters to check for.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, StringComparison stringComparisonType,
        string variableName, object? blackboard = null, params char[] items)
    {
        return value.ValidateDoesNotContainAll(items, variableName, stringComparisonType, blackboard);
    }

    /// <summary>
    ///     Validates that a string does not contain all of the specified characters in a collection.
    ///     This is the main implementation that other character-based overloads delegate to.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="items">The collection of characters to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="stringComparisonType">The string comparison type to use when checking for characters.</param>
    /// <param name="blackboard">Additional context data to include in the exception.</param>
    /// <returns>The validated string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains all of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAll(this string? value, ICollection<char> items, string variableName,
        StringComparison stringComparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(items, stringComparisonType))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain any of the specified characters.", "DoesNotContainAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "value", value },
                    { "items", items }
                });
        }

        return value!;
    }
}
