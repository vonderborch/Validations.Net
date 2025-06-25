using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is not empty.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotEmptyAttribute<T>() : ValidationAttribute("IsNotEmpty")
{
    /// <summary>
    /// Validates whether the provided value is not empty. The method checks various types such as strings and collections.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance on which the validation is being performed.</param>
    /// <returns>True if the value is valid and not empty; otherwise, false.</returns>
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
    /// Validates the value of a field or property to ensure it is not empty.
    /// The method supports various types such as strings and generic collections.
    /// </summary>
    /// <param name="value">The value of the field or property to validate.</param>
    /// <param name="instance">The instance containing the field or property being validated.</param>
    /// <param name="propertyName">The name of the field or property being validated.</param>
    /// <param name="blackboard">An optional blackboard object that provides additional context for validation.</param>
    /// <returns>A ValidationResult indicating whether the validation was successful or not.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ((string?)value).ValidateIsNotEmpty(propertyName, blackboard),
            string str => str.ValidateIsNotEmpty(propertyName, blackboard),
            ICollection<T> collection => collection.ValidateIsNotEmpty(propertyName, blackboard),
            _ => new ValidationResult(ValidationException.CreateFromTypeMisMatch<T>("IsEmpty", propertyName, value, blackboard))
        };
    }
}
