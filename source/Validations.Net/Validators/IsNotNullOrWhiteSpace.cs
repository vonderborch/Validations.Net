using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a string is not null or whitespace.
/// </summary>
public static class IsNotNullOrWhiteSpace
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotNullOrWhiteSpace";

    /// <summary>
    ///     The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not be null or whitespace";

    /// <summary>
    ///     Checks if the string is not null or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null or whitespace, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrWhiteSpace(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Ensures that the string is not null or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not null or whitespace, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null or whitespace.</exception>
    public static string? EnsureIsNotNullOrWhiteSpace(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotNullOrWhiteSpace(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the string is not null or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrWhiteSpace(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrWhiteSpace())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
