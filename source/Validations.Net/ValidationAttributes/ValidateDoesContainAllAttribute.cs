using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string contains all of the specified substrings or characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAllAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAllAttribute class with a collection of substrings.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="subStrings">The substrings to search for.</param>
    public ValidateDoesContainAllAttribute(StringComparison comparison, params string[] subStrings) : base(
        "DoesContainAll")
    {
        this.Comparison = comparison;
        this.Substrings = subStrings;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAllAttribute class with a collection of characters.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    public ValidateDoesContainAllAttribute(StringComparison comparison, params char[] characters) : base(
        "DoesContainAll")
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
    /// Determines whether the provided value satisfies the condition of containing all specified substrings or characters,
    /// based on the provided string comparison type.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance of the object containing the value, if applicable.</param>
    /// <returns>
    /// True if the value contains all specified substrings or characters based on the comparison type; otherwise, false.
    /// </returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return this.Characters switch
        {
            null => typedValue.ConvertedValue.CheckDoesContainAll(this.Substrings!, this.Comparison),
            _ => typedValue.ConvertedValue.CheckDoesContainAll(this.Characters, this.Comparison)
        };
    }

    /// <summary>
    /// Validates the specified value against the defined validation logic.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">The blackboard providing additional context for validation, if any.</param>
    /// <returns>A ValidationResult indicating the outcome of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return this.Characters switch
        {
            null => typedValue.ConvertedValue.ValidateDoesContainAll(this.Substrings!, propertyName, this.Comparison, blackboard),
            _ => typedValue.ConvertedValue.ValidateDoesContainAll(this.Characters, propertyName, this.Comparison, blackboard)
        };
    }
}
