using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotWhiteSpace
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotWhiteSpace";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not be whitespace";

    /// <summary>
    ///     Checks if the value is whitespace.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is whitespace, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Ensures that the value is whitespace.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is whitespace, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not whitespace.</exception>
    public static string? EnsureIsNotWhiteSpace(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        var result = value.ValidateIsNotWhiteSpace(parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates if the value is whitespace.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotWhiteSpace(this string? value, string? parameterName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
