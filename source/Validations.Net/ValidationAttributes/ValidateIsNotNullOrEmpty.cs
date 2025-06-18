using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a value is not null and not empty.
/// </summary>
/// <typeparam name="T">The type of the value to validate. Must be a string, array of T, or ICollection{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotNullOrEmpty<T>() : ValidationAttribute("IsNotNullOrEmpty")
{
    /// <summary>
    /// Checks if the provided value is not null and not empty.
    /// </summary>
    /// <param name="value">The value to check. Must be a string, array of T, or ICollection<T>.</param>
    /// <returns>True if the value is not null and not empty, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string, array of T, or ICollection<T>.</exception>
    public override bool Check(object? value)
    {
        switch (value)
        {
            case null:
                return false;
            case string str:
                return !str.CheckIsNotNullOrEmpty();
            case T[] array:
                return array.CheckIsNotNullOrEmpty();
            case ICollection<T> collection:
                return collection!.CheckIsNotNullOrEmpty();
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>(ValidatorName, nameof(value), value);
        }
    }
    
    /// <summary>
    /// Validates if the provided value is not null and not empty and throws a ValidationException if it is.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string, array of T, or ICollection<T>.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or empty, or when the value is not a string, array of T, or ICollection<T>.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case null:
                value.ValidateIsNotNull(propertyName, blackboard);
                return;
            case string str:
                str.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                return;
            case T[] array:
                 array.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                 return;
            case ICollection<T> collection:
                collection!.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                return;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>(ValidatorName, propertyName, value, blackboard);
        }
    }
}
