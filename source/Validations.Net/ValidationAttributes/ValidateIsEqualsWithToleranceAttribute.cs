using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a numeric value equals another value within a specified tolerance.
/// </summary>
/// <typeparam name="T">The type of the value to validate, must implement INumber{T}.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsEqualsWithToleranceAttribute<T>(T compareTo, T tolerance)
    : ValidationAttribute("IsEqualsWithTolerance") where T : INumber<T>
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
    /// Checks if the provided value satisfies the validation criteria by comparing it to a target value with a specified tolerance.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="instance">The instance containing the field or property being validated, or null if not applicable.</param>
    /// <returns>True if the value meets the validation criteria; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return typedValue.ConvertedValue!.CheckIsEqualsWithTolerance(this.CompareTo, this.Tolerance);
    }

    /// <summary>
    /// Validates whether the given value matches the comparison value within a specified tolerance.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard instance to be used during validation.</param>
    /// <returns>The result of the validation, encapsulated in a <see cref="ValidationResult"/>.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return typedValue.ConvertedValue!.ValidateIsEqualsWithTolerance(this.CompareTo, this.Tolerance, propertyName, blackboard);
    }
}
