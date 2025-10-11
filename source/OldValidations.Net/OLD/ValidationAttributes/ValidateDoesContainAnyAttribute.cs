using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string contains any of the specified substrings or characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAnyAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAnyAttribute class with a collection of substrings.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="subStrings">The substrings to search for.</param>
    public ValidateDoesContainAnyAttribute(StringComparison comparison, params string[] subStrings) : base(
        "DoesContainAny")
    {
        this.Comparison = comparison;
        this.Substrings = subStrings;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAnyAttribute class with a collection of characters.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    public ValidateDoesContainAnyAttribute(StringComparison comparison, params char[] characters) : base(
        "DoesContainAny")
    {
        this.Comparison = comparison;
        this.Characters = characters;
    }

    /// <summary>
    ///     Gets the string comparison type to use.
    /// </summary>
    public StringComparison Comparison { get; }

    /// <summary>
    ///     Gets the collection of characters to search for.
    /// </summary>
    public ICollection<char>? Characters { get; init; }

    /// <summary>
    ///     Gets the collection of substrings to search for.
    /// </summary>
    public ICollection<string>? Substrings { get; init; }

    /// <summary>
    ///     Checks if the provided value contains any of the specified substrings or characters.
    /// </summary>
    /// <param name="value">The value to check. Must be a string.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value contains any of the specified substrings or characters, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string.</exception>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return this.Characters switch
        {
            null => typedValue.ConvertedValue.CheckDoesContainAny(this.Substrings!, this.Comparison),
            _ => typedValue.ConvertedValue.CheckDoesContainAny(this.Characters, this.Comparison)
        };
    }

    /// <summary>
    /// Validates the specified value against the defined validation rules.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard instance used for additional validation context.</param>
    /// <returns>A ValidationResult indicating whether validation was successful.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return this.Characters switch
        {
            null => typedValue.ConvertedValue.ValidateDoesContainAny(this.Substrings!, propertyName, this.Comparison, blackboard),
            _ => typedValue.ConvertedValue.ValidateDoesContainAny(this.Characters, propertyName, this.Comparison, blackboard)
        };
    }
}
