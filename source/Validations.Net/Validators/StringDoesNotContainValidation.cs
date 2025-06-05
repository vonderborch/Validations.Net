using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string does not contain a specified substring or substrings.
/// </summary>
public static class StringDoesNotContainValidation
{
    /// <summary>
    /// Determines whether the specified string does not contain the given substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns><c>true</c> if the string does not contain the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DoesNotContain(string value, string substring)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
        
        return !value.Contains(substring);
    }
    
    /// <summary>
    /// Determines whether the specified string does not contain the given substring, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DoesNotContain(string value, string substring, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
        
        return !value.Contains(substring, comparisonType);
    }
    
    /// <summary>
    /// Determines whether the specified string does not contain any of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesNotContain(string value, string[] substrings)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        for (var i = 0; i < substrings.Length; i++)
        {
            substrings[i].ValidateIsNotNullOrWhiteSpace($"{nameof(substrings)}[{i}]");
            if (value.Contains(substrings[i]))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string does not contain any of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesNotContain(string value, string[] substrings, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        for (var i = 0; i < substrings.Length; i++)
        {
            substrings[i].ValidateIsNotNullOrWhiteSpace($"{nameof(substrings)}[{i}]");
            if (value.Contains(substrings[i], comparisonType))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string does not contain any of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesNotContain(string value, IEnumerable<string> substrings)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        foreach (var substring in substrings)
        {
            substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
            if (value.Contains(substring))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Determines whether the specified string does not contain any of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool DoesNotContain(string value, IEnumerable<string> substrings, StringComparison comparisonType)
    {
        value.ValidateIsNotNullOrWhiteSpace(nameof(value));
        
        foreach (var substring in substrings)
        {
            substring.ValidateIsNotNullOrWhiteSpace(nameof(substring));
            if (value.Contains(substring, comparisonType))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain the given substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <returns><c>true</c> if the string does not contain the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, string substring)
    {
        return DoesNotContain(value, substring);
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain the given substring, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain the substring; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, string substring, StringComparison comparisonType)
    {
        return DoesNotContain(value, substring, comparisonType);
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain any of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, string[] substrings)
    {
        return DoesNotContain(value, substrings);
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain any of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, string[] substrings, StringComparison comparisonType)
    {
        return DoesNotContain(value, substrings, comparisonType);
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain any of the given substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, IEnumerable<string> substrings)
    {
        return DoesNotContain(value, substrings);
    }

    /// <summary>
    /// Extension method that checks if the specified string does not contain any of the given substrings, using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <returns><c>true</c> if the string does not contain any of the substrings; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    public static bool CheckDoesNotContain(this string value, IEnumerable<string> substrings, StringComparison comparisonType)
    {
        return DoesNotContain(value, substrings, comparisonType);
    }

    /// <summary>
    /// Validates that the specified string does not contain the given substring. Throws a <see cref="ValidationException"/> if the string contains the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(string value, string substring, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substring))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substring}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring }
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string does not contain the given substring, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(this string value, string substring, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substring, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substring}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string does not contain any of the given substrings. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(this string value, string[] substrings, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substrings))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string does not contain any of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(this string value, string[] substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substrings, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string does not contain any of the given substrings. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(this string value, IEnumerable<string> substrings, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substrings))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified string does not contain any of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string DoesNotContainValidation(this string value, IEnumerable<string> substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substrings, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} contains {substrings}.", "DoesNotContain", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "substrings", substrings },
                { "comparisonType", comparisonType },
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain the given substring. Throws a <see cref="ValidationException"/> if the string contains the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, string substring, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substring, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain the given substring, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains the substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain the substring.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, string substring, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substring, comparisonType, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain any of the given substrings. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, string[] substrings, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substrings, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain any of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The array of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, string[] substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substrings, comparisonType, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain any of the given substrings. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, IEnumerable<string> substrings, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substrings, variableName, blackboard);
    }

    /// <summary>
    /// Extension method that validates the specified string does not contain any of the given substrings, using the specified comparison type. Throws a <see cref="ValidationException"/> if the string contains any of the substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substrings">The enumerable of substrings to look for.</param>
    /// <param name="comparisonType">The type of comparison to use.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it does not contain any of the substrings.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains any of the substrings.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, IEnumerable<string> substrings, StringComparison comparisonType, string variableName, object? blackboard = null)
    {
        return DoesNotContainValidation(value, substrings, comparisonType, variableName, blackboard);
    }
}
