using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is not empty.
/// </summary>
public static class IsNotEmpty
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotEmpty";

    /// <summary>
    ///     The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not be empty";

    /// <summary>
    ///     Checks if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this T? value) where T : ICollection
    {
        return value is not null && value.Count > 0;
    }

    /// <summary>
    ///     Checks if the string is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     Checks if the collection is not empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty(this IEnumerable? value)
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
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Checks if the span is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the span elements.</typeparam>
    /// <param name="value">The span to check.</param>
    /// <returns>True if the span is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this ReadOnlySpan<T> value)
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
    public static bool CheckIsNotEmpty<T>(this Span<T> value)
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
    public static bool CheckIsNotEmpty<T>(this Memory<T> value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    ///     Checks if the StringBuilder is not empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <returns>True if the StringBuilder is not empty, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty(this StringBuilder? value)
    {
        return value is not null && value.Length > 0;
    }

    /// <summary>
    ///     Ensures that the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static T? EnsureIsNotEmpty<T>(this T? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : ICollection
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static string? EnsureIsNotEmpty(this string? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the collection is not empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static IEnumerable? EnsureIsNotEmpty(this IEnumerable? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static ReadOnlySpan<T> EnsureIsNotEmpty<T>(this ReadOnlySpan<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static Span<T> EnsureIsNotEmpty<T>(this Span<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static Memory<T> EnsureIsNotEmpty<T>(this Memory<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the StringBuilder is not empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>The value if it is not empty, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is empty.</exception>
    public static StringBuilder? EnsureIsNotEmpty(this StringBuilder? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotEmpty(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the string is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty(this string? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is not empty.
    /// </summary>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty(this IEnumerable? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty<T>(this ReadOnlySpan<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty<T>(this Span<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
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
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty<T>(this Memory<T> value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", $"Memory<{typeof(T).Name}>[{value.Length}]")]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the StringBuilder is not empty.
    /// </summary>
    /// <param name="value">The StringBuilder to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty(this StringBuilder? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value?.ToString())]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the collection to check.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEmpty<T>(this T? value, IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : ICollection
    {
        if (!value.CheckIsNotEmpty())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
