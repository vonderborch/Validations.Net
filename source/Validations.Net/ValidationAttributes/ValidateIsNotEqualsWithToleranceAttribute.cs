using System.Numerics;
using SimpleBlackboard.Net;
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
    /// <returns>True if the value does not equal the comparison value within the tolerance, false otherwise.</returns>
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotEqualsWithTolerance(this.CompareTo, this.Tolerance);
    }

    /// <summary>
    ///     Validates if the provided value does not equal the comparison value within the specified tolerance and throws a
    ///     ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value equals the comparison value within the tolerance.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        value.ValidateIsNotNull(propertyName, blackboard);
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotEqualsWithTolerance(this.CompareTo, this.Tolerance, propertyName, blackboard);
    }
}
