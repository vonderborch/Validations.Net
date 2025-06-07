using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides custom validation functionality allowing for user-defined validation logic.
/// </summary>
public static class PredicateValidations
{
    /// <summary>
    /// Checks if the specified value satisfies the provided validation predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="predicate">A function that returns true if the value is valid, false otherwise.</param>
    /// <returns>True if the value satisfies the validation predicate, false otherwise.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T value, Func<T, bool> predicate)
    {
        predicate.ValidateIsNotNull(nameof(predicate));
        return predicate(value);
    }
    
    /// <summary>
    /// Validates the specified value using a custom validation predicate. Throws a <see cref="ValidationException"/> if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="predicate">A predicate function that returns true if the value is valid, false otherwise.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context data to include in the validation exception.</param>
    /// <returns>The original value if it passes validation.</returns>
    /// <exception cref="ValidationException">Thrown when the value fails the custom validation.</exception>
    [DebuggerStepThrough]
    public static T ValidateAgainstPredicate<T>(this T value, Func<T, bool> predicate, string variableName, object? blackboard = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            throw new ValidationException(variableName, $"{variableName} failed predicate validation.", "PredicateValidation", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value;
    }
}
