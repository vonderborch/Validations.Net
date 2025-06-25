using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are one of a specified set of values.
/// </summary>
public static class IsOneOf
{
    /// <summary>
    ///     Checks if a value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The set of valid values.</param>
    /// <returns>True if the value is one of the specified options; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, params T[] options)
    {
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if a value is one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The collection of valid values.</param>
    /// <returns>True if the value is one of the specified options; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, ICollection<T> options)
    {
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Ensures that a value is one of the specified options, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <param name="options">The set of valid values.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not one of the specified options.</exception>
    public static T? EnsureIsOneOf<T>(this T? value, string propertyName, IBlackboard? blackboard = null,
        params T[] options)
    {
        ValidationResult result = value.ValidateIsOneOf(propertyName, blackboard, options);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a value is one of the specified options, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The collection of valid values.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not one of the specified options.</exception>
    public static T? EnsureIsOneOf<T>(this T? value, ICollection<T> options, string propertyName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsOneOf(options, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is one of the specified options.
    ///     If the value is not one of the specified options, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The set of valid values.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsOneOf<T>(this T? value, string variableName, IBlackboard? blackboard = null,
        params T[] options)
    {
        if (!value.CheckIsOneOf(options))
        {
            ValidationResult result = new(
                new ValidationException("IsOneOf", variableName,
                    $"{variableName} must be one of the specified values.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "options", options }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that a value is one of the specified options.
    ///     If the value is not one of the specified options, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The collection of valid values.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsOneOf<T>(this T? value, ICollection<T> options, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsOneOf(options))
        {
            ValidationResult result = new(
                new ValidationException("IsOneOf", variableName,
                    $"{variableName} must be one of the specified values.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "options", options }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
