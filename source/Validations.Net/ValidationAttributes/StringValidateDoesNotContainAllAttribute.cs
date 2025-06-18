using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a string does not contain all of the specified substrings or characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAllAttribute : ValidationAttribute
{
    /// <summary>
    /// Gets the collection of substrings to search for.
    /// </summary>
    public ICollection<string>? Substrings { get; init; } = null;

    /// <summary>
    /// Gets the collection of characters to search for.
    /// </summary>
    public ICollection<char>? Characters { get; init; } = null;

    /// <summary>
    /// Gets the string comparison type to use.
    /// </summary>
    public StringComparison Comparison { get; }

    /// <summary>
    /// Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of substrings.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="subStrings">The substrings to search for.</param>
    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params string[] subStrings) : base("DoesNotContainAll")
    {
        Comparison = comparison;
        Substrings = subStrings;
    }

    /// <summary>
    /// Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of characters.
    /// </summary>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="characters">The characters to search for.</param>
    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params char[] characters) : base("DoesNotContainAll")
    {
        Comparison = comparison;
        Characters = characters;
    }

    /// <summary>
    /// Checks if the provided value does not contain all of the specified substrings or characters.
    /// </summary>
    /// <param name="value">The value to check. Must be a string.</param>
    /// <returns>True if the value does not contain all of the specified substrings or characters, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string.</exception>
    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        if (Characters is null)
        {
            return str.CheckDoesNotContainAll(Substrings!, Comparison);
        }
        else
        {
            return str.CheckDoesNotContainAll(Characters, Comparison);
        }
    }

    /// <summary>
    /// Validates if the provided value does not contain all of the specified substrings or characters and throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value contains all of the specified substrings or characters, or when the value is not a string.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(propertyName));
        if (Characters is null)
        { 
            str.ValidateDoesNotContainAll(Substrings!, propertyName, Comparison, blackboard);
        }
        else
        { 
            str.ValidateDoesNotContainAll(Characters, propertyName, Comparison, blackboard);
        }
    }
}
