using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string does not contain all of the specified substrings or characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAllAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of substrings.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="subStrings">The substrings to search for.</param>
    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params string[] subStrings) : base(
        "DoesNotContainAll")
    {
        this.Comparison = comparison;
        this.Substrings = subStrings;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of characters.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params char[] characters) : base(
        "DoesNotContainAll")
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
    /// Checks if the specified value does not contain all defined substrings or characters based on the given criteria.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance that contains the value being validated.</param>
    /// <returns>
    /// <c>true</c> if the value does not contain all substrings or characters; otherwise, <c>false</c>.
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
            null => typedValue.ConvertedValue.CheckDoesNotContainAll(this.Substrings!, this.Comparison),
            _ => typedValue.ConvertedValue.CheckDoesNotContainAll(this.Characters, this.Comparison)
        };
    }

    /// <summary>
    /// Validates the specified value for a property against the provided parameters and rules.
    /// </summary>
    /// <param name="value">The value of the property to be validated.</param>
    /// <param name="instance">The instance of the class that contains the property being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional instance of <see cref="IBlackboard"/> for additional validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> that represents the outcome of the validation.</returns>
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
            null => typedValue.ConvertedValue.ValidateDoesNotContainAll(this.Substrings!, propertyName, this.Comparison, blackboard),
            _ => typedValue.ConvertedValue.ValidateDoesNotContainAll(this.Characters, propertyName, this.Comparison, blackboard)
        };
    }
}
