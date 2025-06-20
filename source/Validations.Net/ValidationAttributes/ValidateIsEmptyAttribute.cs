using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is empty.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsEmptyAttribute<T>() : ValidationAttribute("IsEmpty")
{
    /// <summary>
    /// Checks whether the specified value satisfies the "IsEmpty" validation condition.
    /// </summary>
    /// <param name="value">
    /// The value to be validated. It can be null, a string, or a collection.
    /// </param>
    /// <param name="instance">
    /// The instance of the object containing the property or field being validated. Can be null.
    /// </param>
    /// <returns>
    /// A boolean value indicating whether the value passes the "IsEmpty" validation.
    /// </returns>
    public override bool Check(object? value, object? instance)
    {
        return value switch
        {
            null => ((string?)value).CheckIsEmpty(),
            string str => str.CheckIsEmpty(),
            ICollection<T> collection => collection.CheckIsEmpty(),
            _ => false
        };
    }

    /// <summary>
    /// Validates the provided value against the "IsEmpty" condition using the specified property name and optional blackboard context.
    /// </summary>
    /// <param name="value">
    /// The value of the property or field to be validated. Can be null, a string, or a collection.
    /// </param>
    /// <param name="instance">
    /// The instance of the object containing the property or field being validated. Can be null.
    /// </param>
    /// <param name="propertyName">
    /// The name of the property or field being validated.
    /// </param>
    /// <param name="blackboard">
    /// An optional blackboard context that may be used during validation. Can be null.
    /// </param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating the result of the validation.
    /// </returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        return value switch
        {
            null => ((string?)value).ValidateIsEmpty(propertyName, blackboard),
            string str => str.ValidateIsEmpty(propertyName, blackboard),
            ICollection<T> collection => collection.ValidateIsEmpty(propertyName, blackboard),
            _ => new ValidationResult(ValidationException.CreateFromTypeMisMatch<T>("IsEmpty", propertyName, value, blackboard))
        };
    }
}
