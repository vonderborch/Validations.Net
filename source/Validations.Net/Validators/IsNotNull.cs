using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that values are not null.
/// </summary>
public static class IsNotNull
{
    /// <summary>
    /// Checks if a value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not null; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNull<T>(this T? value)
    {
        return value is not null;
    }
    
    /// <summary>
    /// Validates that a value is not null, throwing a <see cref="ValidationException"/> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null.</exception>
    public static T ValidateIsNotNull<T>(this T? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNull())
        {
            throw new ValidationException("IsNotNull", propertyName, $"{propertyName} must not be null.", blackboard, new Dictionary<string, object?> { { "value", value } });
        }
        
        return value!;
    }
}
