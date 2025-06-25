using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a numeric value is within a specified range.
/// </summary>
/// <typeparam name="T">The type of the value to validate, must implement INumber{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsInRangeAttribute<T> : ValidationAttribute where T : INumber<T>
{
    /// <summary>
    ///     Initializes a new instance of the ValidateIsInRangeAttribute class.
    /// </summary>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minIsInclusive">Whether the minimum value is inclusive in the range. Default is true.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is inclusive in the range. Default is false.</param>
    /// <exception cref="ValidationException">Thrown when min is greater than max.</exception>
    public ValidateIsInRangeAttribute(T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) :
        base("IsInRange")
    {
        min.ValidateIsLessThanOrEquals(max, nameof(min));
        max.ValidateIsGreaterThanOrEquals(min, nameof(max));

        this.Min = min;
        this.Max = max;
        this.MinIsInclusive = minIsInclusive;
        this.MaxIsInclusive = maxIsInclusive;
    }

    /// <summary>
    ///     Gets the maximum value of the range.
    /// </summary>
    public T Max { get; }

    /// <summary>
    ///     Gets whether the maximum value is inclusive in the range.
    /// </summary>
    public bool MaxIsInclusive { get; }

    /// <summary>
    ///     Gets the minimum value of the range.
    /// </summary>
    public T Min { get; }

    /// <summary>
    ///     Gets whether the minimum value is inclusive in the range.
    /// </summary>
    public bool MinIsInclusive { get; }

    /// <summary>
    ///     Checks if the provided value is within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is within the range, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return typedValue.ConvertedValue!.CheckIsInRange(this.Min, this.Max, this.MinIsInclusive, this.MaxIsInclusive);
    }

    /// <summary>
    /// Validates the input value against the defined range constraints for this attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The parent object instance containing the property or field being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">Optional blackboard context for additional validation requirements.</param>
    /// <returns>A ValidationResult indicating whether the validation succeeded or failed.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return typedValue.ConvertedValue!.ValidateIsInRange(this.Min, this.Max, propertyName, this.MinIsInclusive, this.MaxIsInclusive, blackboard);
    }
}
