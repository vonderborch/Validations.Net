using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value equals a specified comparison value.
/// </summary>
/// <typeparam name="T">The type of the value to compare, must implement IComparable{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsEqualsAttribute<T> : ValidationAttribute where T : IComparable<T>
{
    /// <summary>
    ///     Attribute that validates if a value equals a specified comparison value.
    /// </summary>
    /// <param name="compareTo">The value to compare to.</param>
    public ValidateIsEqualsAttribute(T compareTo) : base("IsEquals")
    {
        CompareTo = compareTo;
        Comparer = null;
    }
    
    /// <summary>
    ///     Attribute that validates if a value equals a specified comparison value.
    /// </summary>
    /// <param name="compareTo">The value to compare to.</param>
    /// <param name="comparer">The equality comparer used to compare values.</param>
    public ValidateIsEqualsAttribute(T compareTo, IEqualityComparer<T> comparer) : base("IsEquals")
    {
        CompareTo = compareTo;
        Comparer = comparer;
    }
    
    /// <summary>
    ///     Gets the equality comparer used to compare values.
    /// </summary>
    public IEqualityComparer<T>? Comparer { get; }

    /// <summary>
    ///     Gets the value to compare against.
    /// </summary>
    public T CompareTo { get; }

    /// <summary>
    /// Checks whether the provided value meets the validation criteria implemented by the validation attribute.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The instance of the object containing the value.</param>
    /// <returns>Returns true if the validation criteria are met; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.Comparer is null)
        {
            return typedValue.ConvertedValue!.CheckIsEquals(this.CompareTo);
        }
        return typedValue.ConvertedValue!.CheckIsEquals(this.CompareTo, this.Comparer);
    }

    /// <summary>
    /// Validates the given value using the specified rules and parameters.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance of the class containing the property to be validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard instance for additional validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the outcome of validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        if (this.Comparer is null)
        {
            return typedValue.ConvertedValue!.ValidateIsEquals(this.CompareTo, propertyName, blackboard);
        }
        return typedValue.ConvertedValue!.ValidateIsEquals(this.CompareTo, this.Comparer, propertyName, blackboard);
    }
}
