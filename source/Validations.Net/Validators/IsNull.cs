using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that values are null.
/// </summary>
public static class IsNull
{
    /// <summary>
    /// Checks if a value is null.
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
    /// Validates that a value is null, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null.</exception>
    public static T ValidateIsNull<T>(this T? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNull())
        {
            throw new ValidationException("IsNull", propertyName, $"{propertyName} must be null.", blackboard, new Dictionary<string, object?> { { "value", value } });
        }
        
        return value!;
    }
}
