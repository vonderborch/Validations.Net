using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating the length of a value.
/// </summary>
public static class IsNotLength
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotLength";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not have the specified length";

    /// <summary>
    ///     Checks if the string has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the string length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this string? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        if (value is null)
        {
            return false;
        }

        return !CheckLength(value.Length, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the collection has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the collection length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this IEnumerable? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        if (value is null)
        {
            return false;
        }

        if (value is ICollection collection)
        {
            return CheckLength(collection.Count, expectedLength, mode);
        }

        // For non-ICollection IEnumerable, we need to count manually
        var count = 0;
        foreach (var _ in value)
        {
            count++;
            // Early exit for LessThan and LessThanOrEqual modes
            if (mode == LengthCheckMode.LessThan && count >= expectedLength)
            {
                return false;
            }

            if (mode == LengthCheckMode.LessThanOrEqual && count > expectedLength)
            {
                return false;
            }
        }

        return CheckLength(count, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the span length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this ReadOnlySpan<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        return !CheckLength(value.Length, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the span length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this Span<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        return !CheckLength(value.Length, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the memory has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the memory length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this Memory<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        return !CheckLength(value.Length, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the StringBuilder has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the StringBuilder length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this StringBuilder? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength)
    {
        if (value is null)
        {
            return false;
        }

        return !CheckLength(value.Length, expectedLength, mode);
    }

    /// <summary>
    ///     Checks if the collection has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <returns>True if the collection length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this T? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength) where T : ICollection
    {
        if (value is null)
        {
            return false;
        }

        return CheckLength(value.Count, expectedLength, mode);
    }

    /// <summary>
    ///     Helper method to check if the actual length matches the expected length according to the mode.
    /// </summary>
    /// <param name="actualLength">The actual length.</param>
    /// <param name="expectedLength">The expected length.</param>
    /// <param name="mode">The mode for checking the length.</param>
    /// <returns>True if the length matches the criteria, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckLength(int actualLength, int expectedLength, LengthCheckMode mode)
    {
        return mode switch
        {
            LengthCheckMode.ExactLength => actualLength == expectedLength,
            LengthCheckMode.LessThan => actualLength < expectedLength,
            LengthCheckMode.LessThanOrEqual => actualLength <= expectedLength,
            LengthCheckMode.GreaterThan => actualLength > expectedLength,
            LengthCheckMode.GreaterThanOrEqual => actualLength >= expectedLength,
            _ => false
        };
    }

    /// <summary>
    ///     Ensures that the string has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotLength(this string? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Ensures that the collection has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the collection length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotLength(this IEnumerable? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Ensures that the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotLength<T>(this ReadOnlySpan<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Ensures that the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotLength<T>(this Span<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Ensures that the memory has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the memory length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsLength<T>(this Memory<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Ensures that the StringBuilder has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the StringBuilder length does not match the criteria.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotLength(this StringBuilder? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotLength(expectedLength, mode, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Helper method to get the length of a collection.
    /// </summary>
    /// <param name="value">The collection to get the length of.</param>
    /// <returns>The length of the collection, or null if the collection is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int? GetCollectionLength(IEnumerable? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is ICollection collection)
        {
            return collection.Count;
        }

        var count = 0;
        foreach (var _ in value)
        {
            count++;
        }

        return count;
    }

    /// <summary>
    ///     Helper method to generate validation failure messages.
    /// </summary>
    /// <param name="typeName">The name of the type being validated.</param>
    /// <param name="actualLength">The actual length.</param>
    /// <param name="expectedLength">The expected length.</param>
    /// <param name="mode">The mode for checking the length.</param>
    /// <returns>The validation failure message.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetLengthValidationMessage(string typeName, int? actualLength, int expectedLength,
        LengthCheckMode mode)
    {
        var operatorText = mode switch
        {
            LengthCheckMode.ExactLength => "exactly",
            LengthCheckMode.LessThan => "less than",
            LengthCheckMode.LessThanOrEqual => "less than or equal to",
            LengthCheckMode.GreaterThan => "greater than",
            LengthCheckMode.GreaterThanOrEqual => "greater than or equal to",
            _ => "exactly"
        };

        var actualLengthText = actualLength?.ToString() ?? "null";
        return $"{typeName} length must be {operatorText} {expectedLength}. Actual length: {actualLengthText}";
    }

    /// <summary>
    ///     Validates if the string has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength(this string? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("string", value?.Length, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [("value", value), ("actualLength", value?.Length), ("expectedLength", expectedLength), ("mode", mode)]
        );
    }

    /// <summary>
    ///     Validates if the collection has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the collection length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength(this IEnumerable? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var actualLength = GetCollectionLength(value);
        var message = GetLengthValidationMessage("collection", actualLength, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [("value", value), ("actualLength", actualLength), ("expectedLength", expectedLength), ("mode", mode)]
        );
    }

    /// <summary>
    ///     Validates if the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the span length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength<T>(this ReadOnlySpan<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("ReadOnlySpan", value.Length, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [
                ("value", value.ToString()), ("actualLength", value.Length), ("expectedLength", expectedLength),
                ("mode", mode)
            ]
        );
    }

    /// <summary>
    ///     Validates if the span has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the span length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength<T>(this Span<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("Span", value.Length, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [
                ("value", value.ToString()), ("actualLength", value.Length), ("expectedLength", expectedLength),
                ("mode", mode)
            ]
        );
    }

    /// <summary>
    ///     Validates if the memory has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the memory length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength<T>(this Memory<T> value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("Memory", value.Length, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [
                ("value", value.ToString()), ("actualLength", value.Length), ("expectedLength", expectedLength),
                ("mode", mode)
            ]
        );
    }

    /// <summary>
    ///     Validates if the StringBuilder has the specified length according to the check mode.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the StringBuilder length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength(this StringBuilder? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("StringBuilder", value?.Length, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [("value", value), ("actualLength", value?.Length), ("expectedLength", expectedLength), ("mode", mode)]
        );
    }

    /// <summary>
    ///     Validates if the collection has the specified length according to the check mode.
    /// </summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="expectedLength">The expected length to compare against.</param>
    /// <param name="mode">The mode for checking the length (default: ExactLength).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the collection length matches the criteria.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotLength<T>(this T? value, int expectedLength,
        LengthCheckMode mode = LengthCheckMode.ExactLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : ICollection
    {
        if (!CheckIsNotLength(value, expectedLength, mode))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var message = GetLengthValidationMessage("collection", value?.Count, expectedLength, mode);
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [("value", value), ("actualLength", value?.Count), ("expectedLength", expectedLength), ("mode", mode)]
        );
    }
}
