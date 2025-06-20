using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a numeric value does not equal another value within a specified tolerance.
/// </summary>
/// <typeparam name="T">The type of the value to validate, must implement INumber{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotEqualsWithToleranceAttribute<T>(T compareTo, T tolerance)
    : ValidationAttribute("IsNotEqualsWithTolerance") where T : INumber<T>
{
    /// <summary>
    ///     Gets the value to compare against.
    /// </summary>
    public T CompareTo { get; } = compareTo;

    /// <summary>
    ///     Gets the tolerance value for the comparison.
    /// </summary>
    public T Tolerance { get; } = tolerance;

    /// <summary>
    ///     Checks if the provided value does not equal the comparison value within the specified tolerance.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value does not equal the comparison value within the tolerance, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return typedValue.ConvertedValue!.CheckIsNotEqualsWithTolerance(this.CompareTo, this.Tolerance);
    }

    /// <summary>
    /// Validates whether the given value does not match the comparison value within a specified tolerance.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard instance to be used during validation.</param>
    /// <returns>The result of the validation, encapsulated in a <see cref="ValidationResult"/>.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return typedValue.ConvertedValue!.ValidateIsEqualsWithTolerance(this.CompareTo, this.Tolerance, propertyName, blackboard);
    }
}
