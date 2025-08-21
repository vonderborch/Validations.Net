using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value is greater than or equal to another value.
/// </summary>
public static class IsGreaterThanOrEquals
{
    /// <summary>
    /// The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsGreaterThanOrEquals";

    /// <summary>
    /// The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must be greater than or equal to the specified value";

    /// <summary>
    /// Checks if the value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns>True if the value is greater than or equal to the specified value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThanOrEquals<T>(this T value, T other) where T : IComparable<T>
    {
        return value.CompareTo(other) >= 0;
    }

    /// <summary>
    /// Ensures that the value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>The value if it is greater than or equal to the specified value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not greater than or equal to the specified value.</exception>
    public static T EnsureIsGreaterThanOrEquals<T>(this T value, T other, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        ValidationResult result = value.ValidateIsGreaterThanOrEquals(other, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates if the value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsGreaterThanOrEquals<T>(this T value, T other, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThanOrEquals(other))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("other", other)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
