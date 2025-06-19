using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are not one of a specified set of values.
/// </summary>
public static class IsNotWithin
{
    /// <summary>
    ///     Checks if a value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The set of invalid values.</param>
    /// <returns>True if the value is not one of the specified options; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWithin<T>(this T? value, params T[] options)
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
    public static bool CheckIsNotWithin<T>(this T? value, ICollection<T> options)
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
    ///     Validates that a value is not one of the specified options, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <param name="options">The set of invalid values.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T ValidateIsNotWithin<T>(this T? value, string propertyName, Blackboard? blackboard = null,
        params T[] options)
    {
        if (!value.CheckIsNotWithin(options))
        {
            throw new ValidationException("IsNotWithin", propertyName,
                $"{propertyName} must not be one of the specified values.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "options", options }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a value is not one of the specified options, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="options">The collection of invalid values.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T ValidateIsNotWithin<T>(this T? value, ICollection<T> options, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotWithin(options))
        {
            throw new ValidationException("IsNotWithin", propertyName,
                $"{propertyName} must not be one of the specified values.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "options", options }
                });
        }

        return value!;
    }
}
