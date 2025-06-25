using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is null or empty.
/// </summary>
/// <typeparam name="T">The type of the value to validate. Must be a string, array of T, or ICollection{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNullOrEmptyAttribute<T>() : ValidationAttribute("IsNullOrEmpty")
{
    /// <summary>
    ///     Checks if the provided value is null or empty.
    /// </summary>
    /// <param name="value">The value to check. Must be a string, array of T, or ICollection<T>.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is null or empty, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string, array of T, or ICollection<T>.</exception>
    public override bool Check(object? value, object? instance)
    {
        switch (value)
        {
            case null:
                return true;
            case T[] array:
                return array.CheckIsNullOrEmpty();
            case ICollection<T> collection:
                return collection!.CheckIsNullOrEmpty();
            case string str:
                return str.CheckIsNullOrEmpty();
            default:
                throw ValidationException.CreateFromTypeMisMatch<T>(this.ValidatorName, nameof(value), value);
        }
    }

    /// <summary>
    ///     Validates if the provided value is null or empty and throws a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string, array of T, or ICollection<T>.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not null or empty, or when the value is not a string,
    ///     array of T, or ICollection<T>.
    /// </exception>
    public override void Validate(object? value, object? instance, string propertyName, IBlackboard? blackboard = null)
    {
        switch (value)
        {
            case null:
                return;
            case T[] array:
                array.ValidateIsNullOrEmpty(propertyName, blackboard);
                return;
            case ICollection<T> collection:
                collection!.ValidateIsNullOrEmpty(propertyName, blackboard);
                return;
            case string str:
                str.ValidateIsNullOrEmpty(propertyName, blackboard);
                return;
            default:
                throw ValidationException.CreateFromTypeMisMatch<T>(this.ValidatorName, propertyName, value,
                    blackboard);
        }
    }
}
