using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are not one of a specified set of values.
/// </summary>
public static class IsNotOneOf
{
    /// <summary>
    ///     Checks if a value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The set of invalid values.</param>
    /// <returns>True if the value is not one of the specified options; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, params T[] options)
    {
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The collection of invalid values.</param>
    /// <returns>True if the value is not one of the specified options; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, ICollection<T> options)
    {
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Ensures that a value is not one of the specified options, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <param name="options">The set of invalid values.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T? EnsureIsNotOneOf<T>(this T? value, string propertyName, Blackboard? blackboard = null,
        params T[] options)
    {
        ValidationResult result = value.ValidateIsNotOneOf(propertyName, blackboard, options);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a value is not one of the specified options, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The collection of invalid values.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T? EnsureIsNotOneOf<T>(this T? value, ICollection<T> options, string propertyName,
        Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotOneOf(options, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is not one of the specified options.
    ///     If the value is one of the specified options, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The set of invalid values.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, string variableName,
        Blackboard? blackboard = null,
        params T[] options)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            ValidationResult result = new(
                new ValidationException("IsNotOneOf", variableName,
                    $"{variableName} must not be one of the specified values.",
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
    ///     Validates that a value is not one of the specified options.
    ///     If the value is one of the specified options, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The collection of invalid values.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, ICollection<T> options, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            ValidationResult result = new(
                new ValidationException("IsNotOneOf", variableName,
                    $"{variableName} must not be one of the specified values.",
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
