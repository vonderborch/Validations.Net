using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is one of the specified options.
/// </summary>
public static class IsOneOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsOneOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must be one of the specified values";

    /// <summary>
    ///     Checks if the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>True if the value is one of the specified options, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, params T[] options)
    {
        if (value is null || options.Length == 0)
        {
            return false;
        }

        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>True if the value is one of the specified options, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, ICollection<T> options)
    {
        if (value is null || options.Count == 0)
        {
            return false;
        }

        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Ensures that the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>The value if it is one of the specified options, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not one of the specified options.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsOneOf<T>(this T? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T[] options)
    {
        var result = value.ValidateIsOneOf(blackboard, parameterName, options);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>The value if it is one of the specified options, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not one of the specified options.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsOneOf<T>(this T? value, ICollection<T> options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsOneOf(options, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOneOf<T>(this T? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        params T[] options)
    {
        if (!value.CheckIsOneOf(options))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value), ("options", options)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    ///     Validates if the value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOneOf<T>(this T? value, ICollection<T> options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsOneOf(options))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value), ("options", options)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
