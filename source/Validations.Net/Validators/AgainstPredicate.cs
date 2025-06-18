using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating values against predicates.
/// </summary>
public static class AgainstPredicate
{
    /// <summary>
    /// Checks if a value satisfies a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check against the predicate.</param>
    /// <param name="predicate">The predicate function to evaluate the value against.</param>
    /// <returns>True if the value satisfies the predicate; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T value, Func<T, bool> predicate)
    {
        predicate.ValidateIsNotNull(nameof(predicate));
        return predicate(value);
    }
    
    /// <summary>
    /// Validates that a value satisfies a given predicate, throwing a <see cref="ValidationException"/> if it doesn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate against the predicate.</param>
    /// <param name="predicate">The predicate function to evaluate the value against.</param>
    /// <param name="variableName">The name of the variable being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
    /// <exception cref="ValidationException">Thrown when the value fails to satisfy the predicate.</exception>
    public static T ValidateAgainstPredicate<T>(this T value, Func<T, bool> predicate, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            throw new ValidationException("AgainstPredicate", variableName, $"{variableName} failed predicate validation.",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value;
    }
}
