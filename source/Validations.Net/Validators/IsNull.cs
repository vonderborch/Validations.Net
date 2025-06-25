using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are null.
/// </summary>
public static class IsNull
{
    /// <summary>
    ///     Checks if a value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is null; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNull<T>(this T? value)
    {
        return value is null;
    }

    /// <summary>
    ///     Ensures that a value is null, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null.</exception>
    public static T EnsureIsNull<T>(this T? value, string propertyName, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNull(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a value is null.
    ///     If the value is not null, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNull<T>(this T? value, string variableName, IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNull())
        {
            ValidationResult result = new(
                new ValidationException("IsNull", variableName,
                    $"{variableName} must be null.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
