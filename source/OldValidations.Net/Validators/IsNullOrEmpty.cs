using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is null or empty.
/// </summary>
public static class IsNullOrEmpty
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNullOrEmpty";

    /// <summary>
    ///     The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must be null or empty";

    /// <summary>
    ///     Checks if the string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     Checks if the collection is null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this IEnumerable? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is ICollection collection)
        {
            return collection.Count == 0;
        }

        foreach (var _ in value)
        {
            return false; // Found at least one element, so not empty
        }

        return true; // No elements found, so empty
    }

    /// <summary>
    ///     Checks if the span is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <returns>True if the span is empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this ReadOnlySpan<T> value)
    {
        return value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the span is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <returns>True if the span is empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this Span<T> value)
    {
        return value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the memory is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <returns>True if the memory is empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this Memory<T> value)
    {
        return value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the StringBuilder is null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <returns>True if the StringBuilder is null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this StringBuilder? value)
    {
        return value is null || value.Length == 0;
    }

    /// <summary>
    ///     Checks if the collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this T? value) where T : ICollection
    {
        return value is null || value.Count == 0;
    }

    /// <summary>
    ///     Ensures that the string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null or empty.</exception>
    public static string? EnsureIsNullOrEmpty(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the collection is null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null or empty.</exception>
    public static IEnumerable? EnsureIsNullOrEmpty(this IEnumerable? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the span is empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not empty.</exception>
    public static ReadOnlySpan<T> EnsureIsNullOrEmpty<T>(this ReadOnlySpan<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the span is empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not empty.</exception>
    public static Span<T> EnsureIsNullOrEmpty<T>(this Span<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the memory is empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not empty.</exception>
    public static Memory<T> EnsureIsNullOrEmpty<T>(this Memory<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the StringBuilder is null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null or empty.</exception>
    public static StringBuilder? EnsureIsNullOrEmpty(this StringBuilder? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null or empty.</exception>
    public static T? EnsureIsNullOrEmpty<T>(this T? value, string? parameterName = null, IBlackboard? blackboard = null)
        where T : ICollection
    {
        var result = value.ValidateIsNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty(this IEnumerable? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the span is empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty<T>(this ReadOnlySpan<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"ReadOnlySpan<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the span is empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty<T>(this Span<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"Span<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the memory is empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty<T>(this Memory<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"Memory<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the StringBuilder is null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty(this StringBuilder? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value?.ToString())]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNullOrEmpty<T>(this T? value, string? parameterName = null,
        IBlackboard? blackboard = null) where T : ICollection
    {
        if (!value.CheckIsNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
