using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating values against predicates.
/// </summary>
public static class AgainstPredicate
{
    /// <summary>
    ///     Checks whether the specified value satisfies a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value being evaluated.</typeparam>
    /// <param name="value">The value to check against the predicate.</param>
    /// <param name="predicate">A predicate function that defines the validation logic.</param>
    /// <returns><c>true</c> if the value satisfies the predicate; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T value, Func<T, bool> predicate)
    {
        var result = predicate(value);
        return result;
    }

    /// <summary>
    ///     Ensures that the specified value satisfies a given predicate. Throws an exception
    ///     if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate against the predicate.</param>
    /// <param name="predicate">A predicate function that defines the validation logic.</param>
    /// <param name="variableName">The name of the variable being validated, used for exception messages.</param>
    /// <param name="blackboard">An optional blackboard object for storing validation-related information.</param>
    /// <returns>The original value, if it satisfies the predicate.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not satisfy the predicate.</exception>
    public static T EnsureAgainstPredicate<T>(this T value, Func<T, bool> predicate, string variableName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateAgainstPredicate(predicate, variableName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates the given value against a specified predicate.
    ///     If the predicate is not satisfied, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to be validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="predicate">The predicate function used to validate the value.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateAgainstPredicate<T>(this T value, Func<T, bool> predicate,
        string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            ValidationResult result = new(
                new ValidationException("AgainstPredicate", variableName,
                    $"{variableName} failed predicate validation.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
