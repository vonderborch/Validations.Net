using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Strings;

/// <summary>
/// Provides extension methods for validating that strings do not contain any of the specified items.
/// </summary>
/// <remarks>
/// This class contains methods for both checking conditions (methods prefixed with 'Check') 
/// and validating conditions (methods prefixed with 'Validate'). The validation methods will throw 
/// a <see cref="ValidationException"/> if the validation fails.
/// </remarks>
public static class DoesNotContainAnyValidations
{
    /// <summary>
    /// Checks whether the string contains any of the specified string items.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The collection of strings to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string contains at least one of the specified items; otherwise, false.</returns>
    /// <remarks>Note: The method name suggests it checks if a string does NOT contain any items, but it returns true if it DOES contain any.</remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<string> items, StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var val in items)
        {
            if (value.Contains(val, comparisonType))
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified string items.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The collection of strings to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, ICollection<string> items, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(items, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "items", items }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Checks whether the string contains any of the specified string items using a params array.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="items">The array of strings to check for.</param>
    /// <returns>True if the string contains at least one of the specified items; otherwise, false.</returns>
    /// <remarks>This is a convenience overload that allows passing multiple string parameters directly.</remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, StringComparison comparisonType = StringComparison.Ordinal, params string[] items)
    {
        return value.CheckDoesNotContainAny(items, comparisonType);
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified string items using a params array.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <param name="items">The array of strings to check for.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null, params string[] items)
    {
        return value.ValidateDoesNotContainAny(items, variableName, comparisonType, blackboard);
    }
    
    /// <summary>
    /// Checks whether the string contains any of the specified string items from an enumerable collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The enumerable collection of strings to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string contains at least one of the specified items; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, IEnumerable<string> items, StringComparison comparisonType = StringComparison.Ordinal)
    {
        return value.CheckDoesNotContainAny(items.ToList(), comparisonType);
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified string items from an enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The enumerable collection of strings to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, IEnumerable<string> items, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateDoesNotContainAny(items.ToList(), variableName, comparisonType, blackboard);
    }
    
    /// <summary>
    /// Checks whether the string contains any of the specified characters.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The collection of characters to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string contains at least one of the specified characters; otherwise, false.</returns>
    /// <remarks>Note: The method name suggests it checks if a string does NOT contain any items, but it returns true if it DOES contain any.</remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<char> items, StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        items.ValidateIsNotNull(nameof(items));

        foreach (var item in items)
        {
            if (value.Contains(item, comparisonType))
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified characters.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The collection of characters to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, ICollection<char> items, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(items, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified characters.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "items", items }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Checks whether the string contains any of the specified characters using a params array.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="items">The array of characters to check for.</param>
    /// <returns>True if the string contains at least one of the specified characters; otherwise, false.</returns>
    /// <remarks>This is a convenience overload that allows passing multiple character parameters directly.</remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, StringComparison comparisonType = StringComparison.Ordinal, params char[] items)
    {
        return value.CheckDoesNotContainAny(items, comparisonType);
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified characters using a params array.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <param name="items">The array of characters to check for.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null, params char[] items)
    {
        return value.ValidateDoesNotContainAny(items, variableName, comparisonType, blackboard);
    }
    
    /// <summary>
    /// Checks whether the string contains any of the specified characters from an enumerable collection.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="items">The enumerable collection of characters to check for. Cannot be null.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <returns>True if the string contains at least one of the specified characters; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, IEnumerable<char> items, StringComparison comparisonType = StringComparison.Ordinal)
    {
        return value.CheckDoesNotContainAny(items.ToList(), comparisonType);
    }
    
    /// <summary>
    /// Validates that the string does not contain any of the specified characters from an enumerable collection.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="items">The enumerable collection of characters to check for. Cannot be null.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparisonType">The string comparison type to use.</param>
    /// <param name="blackboard">Optional object providing additional context for the validation.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the string contains any of the specified characters.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContainAny(this string? value, IEnumerable<char> items, string variableName, StringComparison comparisonType = StringComparison.Ordinal, object? blackboard = null)
    {
        return value.ValidateDoesNotContainAny(items.ToList(), variableName, comparisonType, blackboard);
    }
}
