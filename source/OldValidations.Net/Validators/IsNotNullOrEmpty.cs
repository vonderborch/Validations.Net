using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is not null or empty.
/// </summary>
public static class IsNotNullOrEmpty
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotNullOrEmpty";

    /// <summary>
    ///     The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not be null or empty";

    /// <summary>
    ///     Checks if the string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     Checks if the collection is not null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this IEnumerable? value)
    {
        if (value is null)
        {
            return false;
        }

        if (value is ICollection collection)
        {
            return collection.Count > 0;
        }

        foreach (var _ in value)
        {
            return true; // Found at least one element, so not empty
        }

        return false; // No elements found, so empty
    }

    /// <summary>
    ///     Checks if the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <returns>True if the span is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this ReadOnlySpan<T> value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <returns>True if the span is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this Span<T> value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the memory is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <returns>True if the memory is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this Memory<T> value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the StringBuilder is not null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <returns>True if the StringBuilder is not null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this StringBuilder? value)
    {
        return value is not null && value.Length > 0;
    }

    /// <summary>
    ///     Checks if the collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not null or empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this T? value) where T : ICollection
    {
        return value is not null && value.Count > 0;
    }

    /// <summary>
    ///     Ensures that the string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null or empty.</exception>
    public static string? EnsureIsNotNullOrEmpty(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the collection is not null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null or empty.</exception>
    public static IEnumerable? EnsureIsNotNullOrEmpty(this IEnumerable? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static ReadOnlySpan<T> EnsureIsNotNullOrEmpty<T>(this ReadOnlySpan<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static Span<T> EnsureIsNotNullOrEmpty<T>(this Span<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the memory is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static Memory<T> EnsureIsNotNullOrEmpty<T>(this Memory<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the StringBuilder is not null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null or empty.</exception>
    public static StringBuilder? EnsureIsNotNullOrEmpty(this StringBuilder? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not null or empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null or empty.</exception>
    public static T? EnsureIsNotNullOrEmpty<T>(this T? value, string? parameterName = null,
        IBlackboard? blackboard = null) where T : ICollection
    {
        var result = value.ValidateIsNotNullOrEmpty(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is not null or empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty(this IEnumerable? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this ReadOnlySpan<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"ReadOnlySpan<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this Span<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"Span<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the memory is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the memory elements.</typeparam>
    /// <param name="value">The memory to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this Memory<T> value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"Memory<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the StringBuilder is not null or empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty(this StringBuilder? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value?.ToString())]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this T? value, string? parameterName = null,
        IBlackboard? blackboard = null) where T : ICollection
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
