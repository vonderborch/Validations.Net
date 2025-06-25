using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is greater than a specified comparison value.
/// </summary>
/// <typeparam name="T">The type of the value to compare, must implement IComparable{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsGreaterThanAttribute<T>(T compareTo)
    : ValidationAttribute("IsGreaterThan") where T : IComparable<T>
{
    /// <summary>
    ///     Gets the value to compare against.
    /// </summary>
    public T CompareTo { get; } = compareTo;

    /// <summary>
    ///     Checks if the provided value is greater than the comparison value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is greater than the comparison value, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return typedValue.ConvertedValue!.CheckIsGreaterThan(this.CompareTo);
    }

    /// <summary>
    /// Validates whether the specified value meets the "greater than" condition in relation to the comparison value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context during validation.</param>
    /// <returns>A ValidationResult representing the outcome of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return typedValue.ConvertedValue!.ValidateIsGreaterThan(this.CompareTo, propertyName, blackboard);
    }
}
