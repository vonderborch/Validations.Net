using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value does not equal a specified comparison value.
/// </summary>
/// <typeparam name="T">The type of the value to compare, must implement IComparable{T}.</typeparam>
/// <param name="comparer">The equality comparer used to compare values.</param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotEquals<T>(T compareTo, IEqualityComparer<T>? comparer = null)
    : ValidationAttribute("IsNotEquals") where T : IComparable<T>
{
    /// <summary>
    ///     Gets the equality comparer used to compare values.
    /// </summary>
    public IEqualityComparer<T>? Comparer { get; } = comparer;

    /// <summary>
    ///     Gets the value to compare against.
    /// </summary>
    public T CompareTo { get; } = compareTo;

    /// <summary>
    ///     Checks if the provided value does not equal the comparison value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value does not equal the comparison value, false otherwise.</returns>
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        if (this.Comparer is null)
        {
            return typedValue.CheckIsNotEquals(this.CompareTo);
        }

        return typedValue.CheckIsNotEquals(this.CompareTo, this.Comparer);
    }

    /// <summary>
    ///     Validates if the provided value does not equal the comparison value and throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value equals the comparison value.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        if (this.Comparer is null)
        {
            typedValue.ValidateIsNotEquals(this.CompareTo, propertyName, blackboard);
        }
        else
        {
            typedValue.ValidateIsNotEquals(this.CompareTo, this.Comparer, propertyName, blackboard);
        }
    }
}
