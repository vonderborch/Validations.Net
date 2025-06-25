using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class StringDoesNotContain
{
    /// <summary>
    ///     Checks if a string does not contain a specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the string does not contain the substring; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string subString,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !value.Contains(subString, comparison);
    }

    /// <summary>
    ///     Checks if a substring of a string does not contain a specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>
    ///     True if the substring does not contain the specified substring; otherwise, false. Returns false if the string
    ///     is null.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        var stringPart = value.Substring(startIndex, count ?? value.Length - startIndex);
        return !stringPart.Contains(subString, comparison);
    }

    /// <summary>
    ///     Checks if a string does not contain a specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the string does not contain the character; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, char character,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !value.Contains(character, comparison);
    }

    /// <summary>
    ///     Checks if a substring of a string does not contain a specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>
    ///     True if the substring does not contain the specified character; otherwise, false. Returns false if the string
    ///     is null.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        var subString = value.Substring(startIndex, count ?? value.Length - startIndex);
        return !subString.Contains(character, comparison);
    }

    /// <summary>
    ///     Ensures that a string does not contain a specified substring, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified substring.</exception>
    public static string EnsureDoesNotContain(this string? value, string subString, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesNotContain(subString, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a substring of a string does not contain a specified substring, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the substring contains the specified substring.</exception>
    public static string EnsureDoesNotContain(this string? value, string subString, string parameterName,
        int startIndex,
        int? count = null, StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result =
            value.ValidateDoesNotContain(subString, parameterName, startIndex, count, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a string does not contain a specified character, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified character.</exception>
    public static string EnsureDoesNotContain(this string? value, char character, string parameterName,
        StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesNotContain(character, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a substring of a string does not contain a specified character, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the substring contains the specified character.</exception>
    public static string EnsureDoesNotContain(this string? value, char character, string parameterName, int startIndex,
        int? count = null,
        StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null)
    {
        ValidationResult result =
            value.ValidateDoesNotContain(character, parameterName, startIndex, count, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a string does not contain a specified substring.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, string subString, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        if (value.CheckDoesNotContain(subString, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesNotContain", parameterName,
            $"{parameterName} must not contain {subString}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subString", subString }
            }));
    }

    /// <summary>
    ///     Validates that a substring of a string does not contain a specified substring.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, string subString, string parameterName,
        int startIndex,
        int? count = null, StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (value.CheckDoesNotContain(subString, startIndex, count, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesNotContain",
            parameterName,
            $"{parameterName} must not contain \"{subString}\" (in substring from {startIndex} for {count} characters).",
            blackboard,
            new Dictionary<string, object?>
            {
                { "value", value },
                { "subString", subString },
                { "startIndex", startIndex },
                { "count", count }
            }));
    }

    /// <summary>
    ///     Validates that a string does not contain a specified character.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, char character, string parameterName,
        StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null)
    {
        if (value.CheckDoesNotContain(character, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesNotContain", parameterName,
            $"{parameterName} must not contain '{character}'.",
            blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "character", character }
            }));
    }

    /// <summary>
    ///     Validates that a substring of a string does not contain a specified character.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain(this string? value, char character, string parameterName,
        int startIndex,
        int? count = null,
        StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (value.CheckDoesNotContain(character, startIndex, count, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesNotContain",
            parameterName,
            $"{parameterName} must not contain '{character}' (in substring from {startIndex} for {count} characters).",
            blackboard,
            new Dictionary<string, object?>
            {
                { "value", value },
                { "character", character },
                { "startIndex", startIndex },
                { "count", count }
            }));
    }
}
