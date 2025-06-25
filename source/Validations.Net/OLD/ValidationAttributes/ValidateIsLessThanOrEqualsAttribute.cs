using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is less than or equal to a specified comparison value.
/// </summary>
/// <typeparam name="T">The type of the value to compare, must implement IComparable{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsLessThanOrEqualsAttribute<T>(T compareTo)
    : ValidationAttribute("IsLessThanOrEquals") where T : IComparable<T>
{
    /// <summary>
    ///     Gets the value to compare against.
    /// </summary>
    public T CompareTo { get; } = compareTo;

    /// <summary>
    ///     Checks if the provided value is less than or equal to the comparison value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is less than or equal to the comparison value, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return typedValue.ConvertedValue!.CheckIsLessThanOrEquals(this.CompareTo);
    }

    /// <summary>
    /// Validates whether the provided value is less than or equal to the specified comparison value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The object instance that contains the property being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional additional context for the validation process.</param>
    /// <returns>A ValidationResult indicating the result of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName, IBlackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return typedValue.ConvertedValue!.ValidateIsLessThanOrEquals(this.CompareTo, propertyName, blackboard);
    }
}
