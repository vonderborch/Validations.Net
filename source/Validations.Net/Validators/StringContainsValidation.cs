using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string contains a specified substring or substrings.
/// </summary>
public static class StringContainsValidation
{
    /// <summary>
    /// Determines whether the specified string contains the given substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns><c>true</c> if the string contains the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DoesContain(string value, string substring)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
        
        return value.Contains(substring);
    }
    
    /// <summary>
    /// Determines whether the specified string contains the given substring, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DoesContain(string value, string substring, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
        
        return value.Contains(substring, comparisonType);
    }
    
    /// <summary>
    /// Determines whether the specified string contains all of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesContain(string value, string[] substrings)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        for (var i = 0; i < substrings.Length; i++)
        {
            substrings[i].ValidateIsNotNullOrWhiteSpace($"{nameof(substrings)}[{i}]");
            if (!value.Contains(substrings[i]))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains all of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesContain(string value, string[] substrings, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        for (var i = 0; i < substrings.Length; i++)
        {
            substrings[i].ValidateIsNotNullOrWhiteSpace($"{nameof(substrings)}[{i}]");
            if (!value.Contains(substrings[i], comparisonType))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains all of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesContain(string value, IEnumerable<string> substrings)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        foreach (var substring in substrings)
        {
            substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
            if (!value.Contains(substring))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains all of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesContain(string value, IEnumerable<string> substrings, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        foreach (var substring in substrings)
        {
            substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
            if (!value.Contains(substring, comparisonType))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains the given substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns><c>true</c> if the string contains the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, string substring)
    {
        return DoesContain(value, substring);
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains the given substring, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, string substring, StringComparison comparisonType)
    {
        return DoesContain(value, substring, comparisonType);
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains all of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, string[] substrings)
    {
        return DoesContain(value, substrings);
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains all of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, string[] substrings, StringComparison comparisonType)
    {
        return DoesContain(value, substrings, comparisonType);
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains all of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, IEnumerable<string> substrings)
    {
        return DoesContain(value, substrings);
    }

    /// <summary>
    /// Extension method that determines whether the specified string contains all of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string contains all of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesContain(this string value, IEnumerable<string> substrings, StringComparison comparisonType)
    {
        return DoesContain(value, substrings, comparisonType);
    }

    /// <summary>
    /// Validates that the specified string contains the given substring. Throws a <see cref="ValidationException"/> if the string does not contain the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(string value, string substring, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substring))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substring}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring }
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string contains the given substring, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(this string value, string substring, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substring, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substring}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string contains all of the given substrings. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(this string value, string[] substrings, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substrings))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string contains all of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(this string value, string[] substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substrings, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string contains all of the given substrings. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(this string value, IEnumerable<string> substrings, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substrings))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string contains all of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesContainValidation(this string value, IEnumerable<string> substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesContain(substrings, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified string contains the given substring. Throws a <see cref="ValidationException"/> if the string does not contain the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, string substring, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substring, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string contains the given substring, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, string substring, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substring, comparisonType, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string contains all of the given substrings. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, string[] substrings, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substrings, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string contains all of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, string[] substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substrings, comparisonType, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string contains all of the given substrings. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, IEnumerable<string> substrings, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substrings, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string contains all of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string does not contain all of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it contains all of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain all of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesContain(this string value, IEnumerable<string> substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesContainValidation(value, substrings, comparisonType, variableName, blackboard);
    }
}
