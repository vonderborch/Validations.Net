using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value meets a predicate.
/// </summary>
public static class AgainstPredicate
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "AgainstPredicate";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must meet the predicate";

    /// <summary>
    /// Checks if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <returns>True if the value meets the predicate, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T? value, Func<T?, bool> predicate)
    {
        var result = predicate(value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T? value, string predicateName, string? predicateGroup = null)
    {
        var result = predicate(value);
        return result;
    }

    /// <summary>
    /// Ensures that the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it meets the predicate, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not meet the predicate.</exception>
    public static T? EnsureAgainstPredicate<T>(this T? value, Func<T?, bool> predicate,
        string? parameterName = null, IBlackboard? blackboard = null)
    {
        ValidationResult validationResult = value.ValidateAgainstPredicate(predicate, parameterName, blackboard);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
        return value;
    }

    /// <summary>
    /// Validates if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <param name="variableName">The name of the variable to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateAgainstPredicate<T>(this T? value, Func<T?, bool> predicate,
        string? variableName = null, IBlackboard? blackboard = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, variableName, blackboard, [("value", value)] );
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
