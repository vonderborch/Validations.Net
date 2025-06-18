using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string does not contain any of the specified substrings or characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAnyAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAnyAttribute class with a collection of substrings.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="subStrings">The substrings to search for.</param>
    public ValidateDoesNotContainAnyAttribute(StringComparison comparison, params string[] subStrings) : base(
        "DoesNotContainAny")
    {
        this.Comparison = comparison;
        this.Substrings = subStrings;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAnyAttribute class with a collection of characters.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    public ValidateDoesNotContainAnyAttribute(StringComparison comparison, params char[] characters) : base(
        "DoesNotContainAny")
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
    ///     Checks if the provided value does not contain any of the specified substrings or characters.
    /// </summary>
    /// <param name="value">The value to check. Must be a string.</param>
    /// <returns>True if the value does not contain any of the specified substrings or characters, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string.</exception>
    public override bool Check(object? value)
    {
        var str = GetCorrectType<string>(value, nameof(value));
        if (this.Characters is null)
        {
            return str.CheckDoesNotContainAny(this.Substrings!, this.Comparison);
        }

        return str.CheckDoesNotContainAny(this.Characters, this.Comparison);
    }

    /// <summary>
    ///     Validates if the provided value does not contain any of the specified substrings or characters and throws a
    ///     ValidationException if it does.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value contains any of the specified substrings or characters, or
    ///     when the value is not a string.
    /// </exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        var str = GetCorrectType<string>(value, nameof(propertyName));
        if (this.Characters is null)
        {
            str.ValidateDoesNotContainAny(this.Substrings!, propertyName, this.Comparison, blackboard);
        }
        else
        {
            str.ValidateDoesNotContainAny(this.Characters, propertyName, this.Comparison, blackboard);
        }
    }
}
