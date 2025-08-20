using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value is greater than another value.
/// </summary>
public static class IsGreaterThan
{
    /// <summary>
    /// The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsGreaterThan";

    /// <summary>
    /// The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must be greater than the specified value";

    /// <summary>
    /// Checks if the value is greater than the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns>True if the value is greater than the specified value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThan<T>(this T value, T other) where T : IComparable<T>
    {
        return value.CompareTo(other) > 0;
    }

    /// <summary>
    /// Ensures that the value is greater than the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is greater than the specified value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not greater than the specified value.</exception>
    public static T EnsureIsGreaterThan<T>(this T value, T other, string? parameterName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        ValidationResult result = value.ValidateIsGreaterThan(other, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates if the value is greater than the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsGreaterThan<T>(this T value, T other, string? parameterName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThan(other))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("other", other)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
